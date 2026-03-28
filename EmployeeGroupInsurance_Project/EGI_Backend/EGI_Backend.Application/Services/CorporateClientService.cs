using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Utilities;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System.Security.Claims;

namespace EGI_Backend.Application.Services
{
public class CorporateClientService : ICorporateClientService
{
    private readonly ICorporateClientRepository _clientRepo;
    private readonly ICorporateDocumentRepository _docRepo;
    private readonly IUserRepository _userRepo;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentStorageService _storage;
    private readonly IMapper _mapper;
    private readonly IAgentCustomerService _agentCustomerService;
    private readonly INotificationService _notificationService;
    private readonly IAIAdjudicationService _aiAdjudicationService;

    public CorporateClientService(
        ICorporateClientRepository clientRepo,
        ICorporateDocumentRepository docRepo,
        IUserRepository userRepo,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        IDocumentStorageService storage,
        IMapper mapper,
        IAgentCustomerService agentCustomerService,
        INotificationService notificationService,
        IAIAdjudicationService aiAdjudicationService)
    {
        _clientRepo = clientRepo;
        _docRepo = docRepo;
        _userRepo = userRepo;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _storage = storage;
        _mapper = mapper;
        _agentCustomerService = agentCustomerService;
        _notificationService = notificationService;
        _aiAdjudicationService = aiAdjudicationService;
    }

    public async Task CreateProfileAsync(Guid userId, CreateCorporateProfileDto dto)
    {
        var existing = await _clientRepo.GetByUserIdAsync(userId);

        if (existing != null)
        {
            if (existing.Status == VerificationStatus.Approved || existing.Status == VerificationStatus.Pending)
                throw new ConflictException("Profile is already active or under review.");

            existing.CompanyName = dto.CompanyName;
            existing.Address = dto.Address;
            existing.Phone = dto.Phone;
            existing.IndustryType = dto.IndustryType;
            // Keep status as Draft or Rejected until documents are uploaded via UploadDocumentAsync
        }
        else
        {
            var client = new CorporateClient
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                Phone = dto.Phone,
                IndustryType = dto.IndustryType,
                Status = VerificationStatus.Draft
            };
            await _clientRepo.AddAsync(client);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<string> UploadDocumentAsync(Guid userId, UploadCorporateDocumentDto dto)
    {
        var client = await _clientRepo.GetByUserIdAsync(userId);

        if (client == null)
            throw new NotFoundException("Corporate client not found.");

        if (client.IsBlocked)
            throw new BadRequestException("You have exceeded the maximum re-submission attempts. Please contact customer care.");

        if (client.Status == VerificationStatus.Approved)
            throw new BadRequestException("Your account is already approved");
        if (client.Status == VerificationStatus.Pending)
            throw new BadRequestException("Your previous documents are still under review. Please wait for admin approval.");

        if (client.Status == VerificationStatus.Rejected)
        {
            client.Status = VerificationStatus.Pending;
            client.RejectionReason = null;
            client.ReviewedBy = Guid.Empty;
            client.ReviewedAt = null;
            
            // CLEAN SLATE: Remove all old rejected documents
            var oldDocs = await _docRepo.GetByClientIdAsync(client.Id);
            foreach (var oldDoc in oldDocs)
            {
                await _docRepo.DeleteAsync(oldDoc.Id);
            }

            // RESET AI Analysis for resubmission so we only judge the new data
            client.KybAiAnalysis = null;
            client.KybAiConfidenceScore = 0;
        }
        if (dto.IndustryType.HasValue)
        {
            client.IndustryType = dto.IndustryType.Value;
        }

        var filePath = await _storage.UploadAsync(dto.File);

        var document = new CorporateClientDocument
        {
            Id = Guid.NewGuid(),
            CorporateClientId = client.Id,
            DocumentType = dto.DocumentType,
            FileName = dto.File.FileName,
            FilePath = filePath
        };

        await _docRepo.AddAsync(document);

        // Run KYB Document Intelligence
        var (aiAnalysis, score) = await _aiAdjudicationService.AnalyzeKYBDocumentsAsync(client, document);

        if (string.IsNullOrEmpty(client.KybAiAnalysis))
        {
            client.KybAiAnalysis = $"[DOCUMENT: {document.DocumentType}]\n{aiAnalysis}";
            client.KybAiConfidenceScore = Math.Max(1, score); // Ensure at least 1 for UI visibility
        }
        else
        {
            client.KybAiAnalysis += $"\n\n[DOCUMENT: {document.DocumentType}]\n{aiAnalysis}";
            // For subsequent documents, we can either average or take the minimum/maximum.
            // Let's take the minimum score to be more conservative, ensuring all documents meet a certain threshold.
            client.KybAiConfidenceScore = Math.Min(client.KybAiConfidenceScore, score);
            if (client.KybAiConfidenceScore < 1) client.KybAiConfidenceScore = 1;
        }

        // REAL FIX: Move from Draft to Pending only after at least one document is uploaded!
        if (client.Status == VerificationStatus.Draft)
        {
            client.Status = VerificationStatus.Pending;
        }

        await _unitOfWork.SaveChangesAsync();

        // Notify Admins
        var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
        foreach (var admin in admins)
        {
            await _notificationService.CreateNotificationAsync(admin.Id, "Document Uploaded", $"New verification document {dto.DocumentType} uploaded by {client.CompanyName}.", "Info");
        }

        return "Document uploaded successfully. Waiting for admin approval.";
    }

    public async Task<List<CorporateClientResponseDto>> GetPendingClientsAsync()
    {
        var clients = await _clientRepo.GetPendingAsync();
        return _mapper.Map<List<CorporateClientResponseDto>>(clients);
    }

    public async Task ReviewClientAsync(Guid clientId, Guid adminId, ReviewCorporateClientDto dto)
    {
        var client = await _clientRepo.GetByIdAsync(clientId);

        if (client == null)
            throw new NotFoundException("Corporate client not found.");

        if (client.Status != VerificationStatus.Pending)
            throw new BadRequestException("Client already reviewed.");

        var user = await _userRepo.GetByIdAsync(client.UserId);

        if (user == null)
            throw new NotFoundException("Associated user not found.");

        if (dto.IndustryType.HasValue)
        {
            client.IndustryType = dto.IndustryType.Value;
        }

        if (dto.IsApproved)
        {
            client.Status = VerificationStatus.Approved;
            client.ReSubmissionCount = 0; // reset on approval
            client.RejectionReason = null;

            user.Status = UserStatus.Active;

            if (user.MustChangePassword)
            {
                var tempPassword = PasswordHelper.GenerateTemporaryPassword();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                await _emailService.SendCredentialsEmailAsync(user.Email, tempPassword);
            }

            // AUTO-ASSIGNMENT LOGIC: Find the Least Loaded Agent ONLY if no agent is currently assigned
            await _agentCustomerService.AssignLeastLoadedAgentAsync(client.Id);

            // Notify Customer
            await _notificationService.CreateNotificationAsync(client.UserId, "Profile Approved", "Your corporate profile has been approved. You can now purchase policies.", "Success");
        }
        else
        {
            client.Status = VerificationStatus.Rejected;
            client.RejectionReason = dto.RejectionReason;
            client.ReSubmissionCount++; // Increment count on rejection
            user.Status = UserStatus.Active; // Ensure they can login to resubmit

            if (client.ReSubmissionCount >= 4)
            {
                client.IsBlocked = true;
                user.Status = UserStatus.Inactive; // Ensure blocked users can't login easily
                await _emailService.SendBlockNotificationEmailAsync(user.Email);

                // Notify Customer
                await _notificationService.CreateNotificationAsync(client.UserId, "Account Blocked", "Your account has been permanently blocked due to multiple verification failures.", "Error");
            }
            else
            {
                await _emailService.SendRejectionEmailAsync(
                    user.Email,
                    dto.RejectionReason ?? "Documents did not meet verification requirements."
                );

                // Notify Customer
                await _notificationService.CreateNotificationAsync(client.UserId, "Profile Rejected", $"Your profile was rejected. Reason: {dto.RejectionReason}", "Warning");
            }
            
            await _unitOfWork.SaveChangesAsync();
        }

        client.ReviewedBy = adminId;
        client.ReviewedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    }
}
 // end namespace EGI_Backend.Application.Services
