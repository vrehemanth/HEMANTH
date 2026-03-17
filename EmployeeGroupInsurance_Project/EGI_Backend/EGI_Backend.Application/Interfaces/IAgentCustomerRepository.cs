using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAgentCustomerRepository
    {
        Task AddAsync(AgentCustomer agentCustomer);
        Task<int> GetCustomerCountForAgentAsync(Guid agentId);
        Task<bool> HasAssignedAgentAsync(Guid clientId);
        Task<AgentCustomer?> GetByCorporateClientIdAsync(Guid clientId);
        Task<List<AgentCustomer>> GetByAgentIdWithDetailsAsync(Guid agentId);
        Task<Guid?> GetLeastLoadedAgentIdAsync();
    }
}
