using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IPolicyAssignmentRepository
    {
        Task AddAsync(PolicyAssignment policyAssignment);
        Task<PolicyAssignment?> GetByIdAsync(Guid id);
        Task<PolicyAssignment?> GetByIdWithDetailsAsync(Guid id);
        Task UpdateAsync(PolicyAssignment policyAssignment);
        Task<List<PolicyAssignment>> GetAllWithDetailsAsync();
        Task<int> CountAsync();
        Task<List<PolicyAssignment>> GetByAgentIdAsync(Guid agentId);
        Task<decimal> GetTotalPremiumForAgentAsync(Guid agentId);
        Task<decimal> GetTotalCommissionForAgentAsync(Guid agentId);
        Task<Dictionary<Guid, decimal>> GetTotalCommissionsForAgentsAsync(List<Guid> agentIds);
        Task<decimal> GetTotalCommissionAsync();
        Task<int> CountActiveForAgentAsync(Guid agentId);
        Task<List<PolicyAssignment>> GetByClientIdAsync(Guid clientId);
        Task<int> CountByClientIdAsync(Guid clientId);
        Task<Dictionary<Guid, int>> GetPolicyCountsForAgentsAsync(List<Guid> agentIds);
    }
}
