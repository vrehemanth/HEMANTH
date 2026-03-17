using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Application.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IInsurancePlanRepository _planRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IUserRepository _userRepo;
        private readonly IDocumentStorageService _storage;
        private readonly IMemberRepository _memberRepo;
        private readonly IDependentRepository _dependentRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly IOCRService _ocrService;
        private readonly IFraudDetectionService _fraudService;
        private readonly IAuditLogRepository _auditRepo;
 
        public ClaimService(
            IClaimRepository claimRepo,
            IPolicyAssignmentRepository policyRepo,
            IInsurancePlanRepository planRepo,
            ICorporateClientRepository clientRepo,
            IUserRepository userRepo,
            IDocumentStorageService storage,
            IMemberRepository memberRepo,
            IDependentRepository dependentRepo,
            IInvoiceRepository invoiceRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            INotificationService notificationService,
            IEmailService emailService,
            IOCRService ocrService,
            IFraudDetectionService fraudService,
            IAuditLogRepository auditRepo)
        {
            _claimRepo = claimRepo;
            _policyRepo = policyRepo;
            _planRepo = planRepo;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
            _storage = storage;
            _memberRepo = memberRepo;
            _dependentRepo = dependentRepo;
            _invoiceRepo = invoiceRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
            _emailService = emailService;
            _ocrService = ocrService;
            _fraudService = fraudService;
            _auditRepo = auditRepo;
        }

        public async Task<string> SubmitClaimAsync(Guid corporateClientUserId, SubmitClaimDto dto)
        {
            if (!string.IsNullOrEmpty(dto.SubmissionToken))
            {
                if (await _claimRepo.IsDuplicateAsync(dto.SubmissionToken))
                    throw new BadRequestException("This claim has already been submitted (Duplicate Token).");
            }

            var member = await _memberRepo.GetByIdWithPolicyAsync(dto.MemberId);
            if (member == null)
                throw new NotFoundException("Member data mismatch. This individual is not recognized.");

            var policy = member.PolicyAssignment;
            if (policy == null || policy.Id != dto.PolicyAssignmentId)
                throw new NotFoundException("Policy not found or member does not belong to this policy.");

            OCRExtractedData? ocrData = null;
            if (dto.Documents.Count > 0)
            {
                try
                {
                    ocrData = await _ocrService.ExtractDataAsync(dto.Documents[0], dto.ClaimType);
                    if (ocrData != null && ocrData.IsSuccess)
                    {
                        if (dto.ClaimType == CoverageType.Life && ocrData.DateOfDeath.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.DateOfDeath.Value;
                        else if (dto.ClaimType == CoverageType.Health && ocrData.BillDate.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.BillDate.Value;
                        else if (dto.ClaimType == (CoverageType)CoverageType.Accident && ocrData.IncidentDate.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.IncidentDate.Value;
                    }
                }
                catch { }
            }

            if (dto.IncidentDate.Date > DateTime.UtcNow.Date)
            {
                throw new BadRequestException("Claim Rejected: Incident date cannot be in the future.");
            }

            if (dto.IncidentDate.Date < policy.StartDate.Date || dto.IncidentDate.Date > policy.EndDate.Date)
            {
                throw new BadRequestException($"Claim Rejected: The incident date ({dto.IncidentDate:dd MMM yyyy}) is outside the policy coverage period ({policy.StartDate:dd MMM yyyy} to {policy.EndDate:dd MMM yyyy}).");
            }

            var submissionDeadline = policy.EndDate.AddDays(30);
            if (DateTime.UtcNow.Date > submissionDeadline.Date)
            {
                throw new BadRequestException($"Submission Blocked: The window for submitting claims for this policy period closed on {submissionDeadline:dd MMM yyyy}.");
            }

            if (policy.Status != PolicyStatus.Active && policy.Status != PolicyStatus.Expired)
                throw new ForbiddenException("Claims can only be submitted for active or recently expired policies.");

            Dependent? dependent = null;
            if (dto.DependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dto.DependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");
            }

            if (!member.Status)
                throw new ForbiddenException("Claim Submission Blocked: This member account is currently inactive.");

            if (dependent != null && !dependent.IsActive)
                throw new ForbiddenException("Claim Submission Blocked: Attempting to claim for an inactive dependent.");

            var client = await _clientRepo.GetByUserIdAsync(corporateClientUserId);
            if (client == null || policy.CorporateClientId != client.Id)
                throw new ForbiddenException("You are not authorized to submit claims for this policy.");

            var invoices = await _invoiceRepo.GetByPolicyAssignmentIdAsync(dto.PolicyAssignmentId);
            var overdueInvoices = invoices
                .Where(i => i.Status != InvoiceStatus.Paid && !i.InvoiceNo.EndsWith("-ADJ"))
                .Where(i => (DateTime.UtcNow.Date - i.DueDate.Date).Days >= 15)
                .ToList();

            if (overdueInvoices.Any())
            {
                var oldestDate = overdueInvoices.Min(i => i.DueDate);
                throw new BadRequestException($"Claim Rejected: Multiple invoices have been overdue since {oldestDate.ToShortDateString()}. Coverage is currently suspended.");
            }

            var plan = await _planRepo.GetByIdAsync(policy.InsurancePlanId);
            if (plan == null)
                throw new NotFoundException("Insurance plan not found.");

            var matchingCoverage = plan.Coverages.FirstOrDefault(c => c.Type == dto.ClaimType);
            if (matchingCoverage == null)
                throw new BadRequestException($"This plan does not cover '{dto.ClaimType}' claims.");

            if (dependent != null)
            {
                bool isParent = dependent.Relationship == RelationshipType.Father || dependent.Relationship == RelationshipType.Mother;
                bool coverageIncludesParents = matchingCoverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents;
                if (isParent && !coverageIncludesParents)
                    throw new BadRequestException($"The plan does not cover parents under '{dto.ClaimType}'.");

                if (!isParent && matchingCoverage.CoveredGroup == CoveredGroup.EmployeeOnly)
                    throw new BadRequestException($"The plan covers '{dto.ClaimType}' for Employee only.");
            }

            decimal coverageLimit = matchingCoverage.CoverageAmount;
            decimal utilizedAmount = await _claimRepo.GetApprovedClaimsTotalAsync(dto.PolicyAssignmentId, dto.MemberId, dto.DependentId, dto.ClaimType);
            decimal availableBalance = coverageLimit - utilizedAmount;

            if (dto.ClaimType == CoverageType.Life)
            {
                dto.ClaimAmount = coverageLimit;
            }

            if (dto.ClaimAmount <= 0)
            {
                throw new BadRequestException("Claim Rejected: Invalid claim amount.");
            }

            if (dto.ClaimAmount > availableBalance)
            {
                throw new BadRequestException($"Insufficient Coverage: Your remaining balance for {dto.ClaimType} is ₹{availableBalance:N2}.");
            }

            if (dto.Documents.Count != dto.DocumentTypes.Count)
                throw new BadRequestException("Each document must have a corresponding document type.");

            if (dto.Documents.Count == 0)
                throw new BadRequestException("At least one supporting document is required.");

            var claim = new Claim
            {
                Id = Guid.NewGuid(),
                ClaimNumber = $"CLM-{DateTime.UtcNow.Year}-{dto.ClaimAmount.ToString().GetHashCode():X4}-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}",
                PolicyAssignmentId = dto.PolicyAssignmentId,
                MemberId = dto.MemberId,
                DependentId = dto.DependentId,
                ClaimType = dto.ClaimType,
                ClaimAmount = dto.ClaimAmount,
                ClaimReason = dto.ClaimReason,
                IncidentDate = dto.IncidentDate,
                ClaimDate = DateTime.UtcNow,
                SubmissionToken = dto.SubmissionToken,
                Status = ClaimStatus.Pending
            };

            if (ocrData != null && ocrData.IsSuccess)
            {
                claim.ExtractedHospitalName = ocrData.HospitalName;
                claim.ExtractedBillDate = ocrData.BillDate;
                claim.ExtractedBillAmount = ocrData.BillAmount;
                claim.ExtractedDateOfDeath = ocrData.DateOfDeath;
                claim.ExtractedCauseOfDeath = ocrData.CauseOfDeath;
                claim.ExtractedFirNumber = ocrData.FirNumber;
                claim.ExtractedPoliceStation = ocrData.PoliceStation;
                claim.ExtractedIncidentDate = ocrData.IncidentDate;
                claim.ExtractedDiagnosis = ocrData.Diagnosis;

                var fraudResult = await _fraudService.AnalyzeClaimAsync(claim, ocrData);
                claim.FraudScore = fraudResult.Score;
                claim.FraudAnalysis = fraudResult.Analysis;
                claim.IsSuspectedFraud = fraudResult.IsFlagged;

                if (claim.IsSuspectedFraud)
                {
                    claim.RequiresAdminApproval = true;
                }
            }
 
            if (claim.ClaimAmount > 500000)
            {
                claim.RequiresAdminApproval = true;
            }

            var uploadedFilePaths = new List<string>();
            try
            {
                for (int i = 0; i < dto.Documents.Count; i++)
                {
                    var file = dto.Documents[i];
                    var filePath = await _storage.UploadAsync(file);
                    uploadedFilePaths.Add(filePath);

                    claim.Documents.Add(new ClaimDocument
                    {
                        Id = Guid.NewGuid(),
                        ClaimId = claim.Id,
                        DocumentType = dto.DocumentTypes[i],
                        FileName = file.FileName,
                        FilePath = filePath,
                        UploadedAt = DateTime.UtcNow
                    });
                }

                member.LastClaimAt = DateTime.UtcNow;
                await _memberRepo.UpdateAsync(member);

                await _claimRepo.AddAsync(claim);
                await _unitOfWork.SaveChangesAsync();

                await _notificationService.CreateNotificationAsync(corporateClientUserId, "Claim Submitted", $"Claim {claim.ClaimNumber} for {claim.ClaimType} has been submitted successfully.", "Success");

                // Notify Agent and Admins
                try
                {
                    if (policy.AgentId != Guid.Empty)
                    {
                        await _notificationService.CreateNotificationAsync(policy.AgentId, "New Claim Received", $"A new {claim.ClaimType} claim {claim.ClaimNumber} has been submitted for policy {policy.PolicyNo}.", "Info");
                    }

                    var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                    foreach (var admin in admins)
                    {
                        await _notificationService.CreateNotificationAsync(admin.Id, "Claim Submitted", $"A new {claim.ClaimType} claim {claim.ClaimNumber} was submitted for policy {policy.PolicyNo}.", "Info");
                    }
                }
                catch { }

                return $"Claim submitted successfully. Claim Number: {claim.ClaimNumber}.";
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                foreach (var path in uploadedFilePaths)
                {
                    try { await _storage.DeleteAsync(path); } catch { }
                }
                throw new BadRequestException("Multiple submission requests detected. Please wait a moment and try again.");
            }
            catch (Exception)
            {
                foreach (var path in uploadedFilePaths)
                {
                    try { await _storage.DeleteAsync(path); } catch { }
                }
                throw;
            }
        }

        public async Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto)
        {
            var user = await _userRepo.GetByIdAsync(claimsOfficerUserId);
            if (user == null) throw new UnauthorizedException("Reviewer not found.");

            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            if (claim.Status != ClaimStatus.Pending && claim.Status != ClaimStatus.InReview && claim.Status != ClaimStatus.PendingAdminApproval)
                throw new BadRequestException("Claim has already been finalized.");

            if (claim.Status == ClaimStatus.InReview && claim.ReviewedBy != claimsOfficerUserId && user.Role != UserRole.Admin)
            {
                throw new ForbiddenException("This claim is currently being reviewed by another officer.");
            }

            if (claim.Status == ClaimStatus.Pending || claim.Status == ClaimStatus.InReview)
            {
                claim.ReviewedBy = claimsOfficerUserId;
                claim.ReviewedAt = DateTime.UtcNow;
                claim.Status = ClaimStatus.InReview;
            }

            if (dto.IsApproved)
            {
                if (claim.FraudScore >= 80 && !dto.OverrideFraud && !claim.IsFraudOverridden)
                {
                    throw new BadRequestException("CRITICAL RISK: High-confidence fraud indicators detected. Override required.");
                }

                if ((claim.FraudScore >= 40 || (claim.IsSuspectedFraud && dto.OverrideFraud)) && user.Role != UserRole.Admin)
                {
                    claim.RequiresAdminApproval = true; 
                }

                if (dto.OverrideFraud && string.IsNullOrWhiteSpace(dto.FraudOverrideReason))
                {
                    throw new BadRequestException("An override justification is required.");
                }

                if (dto.OverrideFraud)
                {
                    claim.IsFraudOverridden = true;
                    claim.FraudOverriddenBy = claimsOfficerUserId;
                    claim.FraudOverrideReason = dto.FraudOverrideReason;
                }
            }

            if (dto.IsApproved)
            {
                if (claim.RequiresAdminApproval && claim.Status != ClaimStatus.PendingAdminApproval && user.Role != UserRole.Admin)
                {
                    claim.Status = ClaimStatus.PendingAdminApproval;
                    claim.ReviewedBy = claimsOfficerUserId;
                    claim.ReviewedAt = DateTime.UtcNow;
                    
                    await _unitOfWork.SaveChangesAsync();
                    
                    // Notify Admins for Executive Approval
                    try
                    {
                        var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                        foreach (var admin in admins)
                        {
                            await _notificationService.CreateNotificationAsync(admin.Id, "Executive Approval Required", $"Claim {claim.ClaimNumber} requires your final review due to high value or risk score.", "Warning");
                        }
                    }
                    catch { }
                    
                    return;
                }

                if (claim.Status == ClaimStatus.PendingAdminApproval)
                {
                    if (user.Role != UserRole.Admin)
                    {
                        throw new ForbiddenException("This high-value claim requires final sign-off from an Administrator.");
                    }
                    claim.AdminApprovedBy = claimsOfficerUserId; 
                    claim.AdminApprovedAt = DateTime.UtcNow;
                }

                claim.Status = ClaimStatus.Approved;
                claim.RejectionReason = null;

                await AdjustMemberCoverageAsync(claim);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dto.RejectionReason))
                    throw new BadRequestException("Rejection reason is required.");

                claim.Status = ClaimStatus.Rejected;
                claim.RejectionReason = dto.RejectionReason;
            }

            await _unitOfWork.SaveChangesAsync();

            // Notification logic (Simplified for restoration)
            try
            {
                var policy = await _policyRepo.GetByIdWithDetailsAsync(claim.PolicyAssignmentId);
                if (policy != null)
                {
                    string title = $"Claim {claim.Status}";
                    string msgBase = $"Claim {claim.ClaimNumber} has been {claim.Status}.";
                    await _notificationService.CreateNotificationAsync(policy.CorporateClientId, title, msgBase, "Info");
                    if (policy.AgentId != Guid.Empty)
                        await _notificationService.CreateNotificationAsync(policy.AgentId, title, msgBase, "Info");
                }
            }
            catch { }
        }

        public async Task<List<ClaimResponseDto>> GetClaimsByPolicyAsync(Guid policyAssignmentId, Guid callerUserId, string role)
        {
            var policy = await _policyRepo.GetByIdWithDetailsAsync(policyAssignmentId);
            if (policy == null) throw new NotFoundException("Policy not found.");

            bool isAuthorized = IsHighAuthority(role) || 
                              (role?.Equals("Customer", StringComparison.OrdinalIgnoreCase) == true && policy.CorporateClient?.UserId == callerUserId) ||
                              (role?.Equals("Agent", StringComparison.OrdinalIgnoreCase) == true && policy.AgentId == callerUserId);

            if (!isAuthorized) throw new ForbiddenException("Security Access Denied.");

            var claims = await _claimRepo.GetByPolicyAssignmentIdAsync(policyAssignmentId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimResponseDto>> GetPendingClaimsAsync()
        {
            var claims = await _claimRepo.GetPendingClaimsAsync();
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimDetailResponseDto>> GetClaimsByMemberAsync(Guid memberId, Guid callerUserId, string role)
        {
            var member = await _memberRepo.GetByIdAsync(memberId);
            if (member == null) throw new NotFoundException("Member not found.");

            var claims = await _claimRepo.GetByMemberIdAsync(memberId);
            return _mapper.Map<List<ClaimDetailResponseDto>>(claims);
        }

        public async Task<ClaimDetailResponseDto> GetClaimDetailAsync(Guid claimId, Guid callerUserId, string role)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            return _mapper.Map<ClaimDetailResponseDto>(claim);
        }

        public async Task<CoverageSummaryDto> GetCoverageSummaryAsync(Guid memberId, Guid? dependentId)
        {
            var member = await _memberRepo.GetByIdWithPolicyAsync(memberId);
            if (member == null)
                throw new NotFoundException("Member not found.");

            var policy = member.PolicyAssignment;
            if (policy == null)
                throw new NotFoundException("Policy assignment not found for this member.");

            var plan = policy.InsurancePlan;
            if (plan == null)
                throw new NotFoundException("Insurance plan not found.");

            // Validate dependent belongs to member (if provided)
            Dependent? dependent = null;
            if (dependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");
            }

            var summary = new CoverageSummaryDto
            {
                MemberId = member.Id,
                MemberName = member.FullName,
                EmployeeCode = member.EmployeeCode,
                DependentId = dependentId,
                DependentName = dependent?.FullName,
                PolicyNo = policy.PolicyNo
            };

            // For each coverage in the plan, calculate remaining balance
            foreach (var coverage in plan.Coverages)
            {
                // Skip coverages that don't apply to this dependent relationship
                if (dependent != null)
                {
                    bool isParent = dependent.Relationship == RelationshipType.Father || dependent.Relationship == RelationshipType.Mother;
                    bool coverageIncludesParents = coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents;
                    bool coverageIncludesFamily = coverage.CoveredGroup == CoveredGroup.EmployeeAndFamily || coverageIncludesParents;

                    if (isParent && !coverageIncludesParents) continue;
                    if (!isParent && coverage.CoveredGroup == CoveredGroup.EmployeeOnly) continue;
                }

                decimal claimed = await _claimRepo.GetApprovedClaimsTotalAsync(policy.Id, memberId, dependentId, coverage.Type);
                decimal remaining = coverage.CoverageAmount - claimed;

                summary.CoverageBreakdown.Add(new CoverageItemDto
                {
                    ClaimType = coverage.Type.ToString(),
                    TotalCoverage = coverage.CoverageAmount,
                    TotalClaimed = claimed,
                    Remaining = Math.Max(0, remaining)
                });
            }

            // Aggregate totals using the Insurance Plan as the master limit
            summary.TotalLimitAllowed = plan.Coverages.Sum(c => c.CoverageAmount);
            summary.TotalAmountUtilized = await _claimRepo.GetApprovedClaimsTotalAsync(policy.Id, memberId, dependentId, null);
            summary.RemainingBalance = Math.Max(0, summary.TotalLimitAllowed - summary.TotalAmountUtilized);

            return summary;
        }

        public async Task<List<ClaimResponseDto>> GetClaimsReviewedByOfficerAsync(Guid officerId)
        {
            var claims = await _claimRepo.GetReviewedByOfficerAsync(officerId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task TakeClaimAsync(Guid officerId, Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");
            claim.Status = ClaimStatus.InReview;
            claim.ReviewedBy = officerId;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReleaseClaimAsync(Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim != null && claim.Status == ClaimStatus.InReview)
            {
                claim.Status = ClaimStatus.Pending;
                claim.ReviewedBy = null;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task AdjustMemberCoverageAsync(Claim claim)
        {
            if (claim.ClaimType == CoverageType.Life)
            {
                if (claim.DependentId.HasValue)
                {
                    var dependent = await _dependentRepo.GetByIdAsync(claim.DependentId.Value);
                    if (dependent != null)
                    {
                        dependent.IsActive = false;
                        await _dependentRepo.UpdateAsync(dependent);
                    }
                }
                else
                {
                    var member = await _memberRepo.GetByIdAsync(claim.MemberId);
                    if (member != null)
                    {
                        member.Status = false;
                        await _memberRepo.UpdateAsync(member);
                    }
                }
            }
        }

        public async Task<(byte[] content, string contentType, string fileName)> GetSecureDocumentAsync(Guid userId, string role, Guid documentId)
        {
            var claimDoc = await _claimRepo.GetDocumentByIdAsync(documentId);
            string? filePath = null;
            string? fileName = null;
            bool hasAccess = IsHighAuthority(role);

            if (claimDoc != null)
            {
                if (!hasAccess && role != null && role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    // Fix: Compare userId (User table PK) with CorporateClient.UserId
                    if (claimDoc.Claim.PolicyAssignment != null && 
                        (claimDoc.Claim.PolicyAssignment.CorporateClient != null && claimDoc.Claim.PolicyAssignment.CorporateClient.UserId == userId))
                    {
                        hasAccess = true;
                    }
                }
                else if (!hasAccess && role != null && role.Equals("Agent", StringComparison.OrdinalIgnoreCase))
                {
                    if (claimDoc.Claim.PolicyAssignment.AgentId == userId)
                    {
                        hasAccess = true;
                    }
                }
                filePath = claimDoc.FilePath;
                fileName = claimDoc.FileName;
            }
            else
            {
                var clientDoc = await _clientRepo.GetDocumentByIdAsync(documentId);
                if (clientDoc == null) throw new NotFoundException("Document not found.");

                if (!hasAccess && role != null && role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    // Fix: Compare userId (User table PK) with CorporateClient.UserId
                    if (clientDoc.CorporateClient != null && clientDoc.CorporateClient.UserId == userId)
                    {
                        hasAccess = true;
                    }
                }
                
                filePath = clientDoc.FilePath;
                fileName = clientDoc.FileName;
            }

            if (!hasAccess) throw new ForbiddenException("Security Access Denied.");

            string finalPath = filePath;
            if (!System.IO.File.Exists(finalPath))
            {
                var fileNameOnly = System.IO.Path.GetFileName(filePath);
                var uploadsFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                var localPath = System.IO.Path.Combine(uploadsFolder, fileNameOnly);
                if (System.IO.File.Exists(localPath)) finalPath = localPath;
                else throw new NotFoundException("Physical file not found.");
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(finalPath);
            var extension = System.IO.Path.GetExtension(finalPath).ToLowerInvariant();
            string contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            return (bytes, contentType, fileName);
        }

        private bool IsHighAuthority(string? role)
        {
            if (string.IsNullOrEmpty(role)) return false;
            var norm = role.Trim().Replace(" ", "").Replace("_", "").ToLowerInvariant();
            return norm == "admin" || norm == "claimsofficer" || norm == "officer";
        }
    }
}
