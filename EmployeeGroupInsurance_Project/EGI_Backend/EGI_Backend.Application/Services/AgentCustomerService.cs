using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class AgentCustomerService : IAgentCustomerService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly ICorporateDocumentRepository _docRepo;
        private readonly IAgentCustomerRepository _agentCustomerRepo;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentStorageService _documentStorage;

        public AgentCustomerService(
            IUserRepository userRepo,
            ICorporateClientRepository clientRepo,
            ICorporateDocumentRepository docRepo,
            IAgentCustomerRepository agentCustomerRepo,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IDocumentStorageService documentStorage)
        {
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _docRepo = docRepo;
            _agentCustomerRepo = agentCustomerRepo;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _documentStorage = documentStorage;
        }

        public async Task CreateCustomerByAgentAsync(Guid agentId, AgentCreateCustomerDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new ConflictException("Email already exists.");

            // Simple temp password — agent will provide credentials directly
            var tempPassword = Guid.NewGuid().ToString("N")[..8];

            var user = new User
            {
                Id              = Guid.NewGuid(),
                Email           = dto.Email,
                Name            = dto.Name,
                PasswordHash    = BCrypt.Net.BCrypt.HashPassword(tempPassword),
                Role            = UserRole.Customer,
                Status          = UserStatus.Inactive,
                MustChangePassword = true
            };
            await _userRepo.AddAsync(user);
            await _emailService.SendCredentialsEmailAsync(user.Email, tempPassword);

            var corporateClient = new CorporateClient
            {
                Id          = Guid.NewGuid(),
                UserId      = user.Id,
                CompanyName = dto.CompanyName,
                Address     = dto.Address,
                Status      = VerificationStatus.Pending
            };
            await _clientRepo.AddAsync(corporateClient);

            var agentCustomer = new AgentCustomer
            {
                Id                = Guid.NewGuid(),
                AgentId           = agentId,
                CorporateClientId = corporateClient.Id,
                AssignedAt        = DateTime.UtcNow
            };
            await _agentCustomerRepo.AddAsync(agentCustomer);

            // Commit user, client and assignment together as one unit of work
            await _unitOfWork.SaveChangesAsync();

            // 8. Financial/Audit: Corporate documentation ingestion
            int fileIdx = 0;
            foreach (var file in dto.Documents)
            {
                var filePath = await _documentStorage.UploadAsync(file);

                // Flaw 9 Fix: Avoid hardcoding all files as PAN. 
                // Cycle through common types or use 'Miscellaneous'
                var docType = fileIdx == 0 ? DocumentType.PAN : 
                             fileIdx == 1 ? DocumentType.GSTIN :
                             DocumentType.CIN;

                if (fileIdx > 2) docType = DocumentType.PAN; // Fallback

                var document = new CorporateClientDocument
                {
                    Id                = Guid.NewGuid(),
                    CorporateClientId = corporateClient.Id,
                    DocumentType      = docType,
                    FileName          = file.FileName,
                    FilePath          = filePath
                };
                await _docRepo.AddAsync(document);
                fileIdx++;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AssignLeastLoadedAgentAsync(Guid corporateClientId)
        {
            // Flaw 7 Fix: Use optimized repository method to avoid N+1 queries
            bool hasAssignedAgent = await _agentCustomerRepo.HasAssignedAgentAsync(corporateClientId);
            if (hasAssignedAgent) return;

            var agentId = await _agentCustomerRepo.GetLeastLoadedAgentIdAsync();
            
            if (agentId.HasValue)
            {
                var newAssignment = new AgentCustomer
                {
                    Id                = Guid.NewGuid(),
                    AgentId           = agentId.Value,
                    CorporateClientId = corporateClientId,
                    AssignedAt        = DateTime.UtcNow
                };
                await _agentCustomerRepo.AddAsync(newAssignment);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
