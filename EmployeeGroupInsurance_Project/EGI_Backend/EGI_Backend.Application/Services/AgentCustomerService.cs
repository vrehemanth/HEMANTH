using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System.IO;
using System.Linq;

public class AgentCustomerService : IAgentCustomerService
{
    private readonly IUserRepository _userRepo;
    private readonly ICorporateClientRepository _clientRepo;
    private readonly ICorporateDocumentRepository _docRepo;
    private readonly IAgentCustomerRepository _agentCustomerRepo;
    private readonly IUnitOfWork _unitOfWork;
    public AgentCustomerService(
        IUserRepository userRepo, 
        ICorporateClientRepository clientRepo,
        ICorporateDocumentRepository docRepo, 
        IAgentCustomerRepository agentCustomerRepo,
        IUnitOfWork unitOfWork)
    {
        _userRepo = userRepo;
        _clientRepo = clientRepo;
        _docRepo = docRepo;
        _agentCustomerRepo = agentCustomerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateCustomerByAgentAsync(Guid agentId, AgentCreateCustomerDto dto)
    {
        var existingUser = await _userRepo.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new ConflictException("Email already exists.");
        
        var tempPassword = Guid.NewGuid().ToString().Substring(0, 8); // Mocking or real temp pass
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            Name = dto.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword),
            Role = UserRole.Customer,
            Status = UserStatus.Inactive,
            MustChangePassword = true
        };

        await _userRepo.AddAsync(user);

        var corporateClient = new CorporateClient
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CompanyName = dto.CompanyName,
            Address = dto.Address,
            Status = VerificationStatus.Draft
        };

        await _clientRepo.AddAsync(corporateClient);

        // 3. Create mapping between Agent and Customer
        var agentCustomer = new AgentCustomer
        {
            Id = Guid.NewGuid(),
            AgentId = agentId,
            CorporateClientId = corporateClient.Id,
            AssignedAt = DateTime.UtcNow
        };
        await _agentCustomerRepo.AddAsync(agentCustomer);

        // Save parent records first
        await _unitOfWork.SaveChangesAsync();

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        foreach (var file in dto.Documents)
        {
            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var document = new CorporateClientDocument
            {
                Id = Guid.NewGuid(),
                CorporateClientId = corporateClient.Id,
                DocumentType = DocumentType.PAN,
                FileName = uniqueFileName,
                FilePath = filePath
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
        int minLoad = int.MaxValue;

        foreach (var currentAgent in agents)
        {
            int load = await _agentCustomerRepo.GetCustomerCountForAgentAsync(currentAgent.Id);
            if (load < minLoad)
            {
                minLoad = load;
                selectedAgentId = currentAgent.Id;
            }
        }

        if (selectedAgentId.HasValue)
        {
            var newAssignment = new AgentCustomer
            {
                Id = Guid.NewGuid(),
                AgentId = selectedAgentId.Value,
                CorporateClientId = corporateClientId,
                AssignedAt = DateTime.UtcNow
            };
            await _agentCustomerRepo.AddAsync(newAssignment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}