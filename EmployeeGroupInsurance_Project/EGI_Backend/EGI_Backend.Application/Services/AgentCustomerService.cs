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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentStorageService _documentStorage;

        public AgentCustomerService(
            IUserRepository userRepo,
            ICorporateClientRepository clientRepo,
            ICorporateDocumentRepository docRepo,
            IAgentCustomerRepository agentCustomerRepo,
            IUnitOfWork unitOfWork,
            IDocumentStorageService documentStorage)
        {
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _docRepo = docRepo;
            _agentCustomerRepo = agentCustomerRepo;
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

            // Upload documents via IDocumentStorageService (Clean Architecture: no direct filesystem access)
            foreach (var file in dto.Documents)
            {
                var filePath = await _documentStorage.UploadAsync(file);

                var document = new CorporateClientDocument
                {
                    Id                = Guid.NewGuid(),
                    CorporateClientId = corporateClient.Id,
                    DocumentType      = DocumentType.PAN,
                    FileName          = file.FileName,
                    FilePath          = filePath
                };
                await _docRepo.AddAsync(document);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AssignLeastLoadedAgentAsync(Guid corporateClientId)
        {
            bool hasAssignedAgent = await _agentCustomerRepo.HasAssignedAgentAsync(corporateClientId);
            if (hasAssignedAgent) return;

            var agents = await _userRepo.GetActiveAgentsAsync();
            if (!agents.Any()) return;

            Guid? selectedAgentId = null;
            int   minLoad         = int.MaxValue;

            foreach (var currentAgent in agents)
            {
                int load = await _agentCustomerRepo.GetCustomerCountForAgentAsync(currentAgent.Id);
                if (load < minLoad)
                {
                    minLoad         = load;
                    selectedAgentId = currentAgent.Id;
                }
            }

            if (selectedAgentId.HasValue)
            {
                var newAssignment = new AgentCustomer
                {
                    Id                = Guid.NewGuid(),
                    AgentId           = selectedAgentId.Value,
                    CorporateClientId = corporateClientId,
                    AssignedAt        = DateTime.UtcNow
                };
                await _agentCustomerRepo.AddAsync(newAssignment);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
