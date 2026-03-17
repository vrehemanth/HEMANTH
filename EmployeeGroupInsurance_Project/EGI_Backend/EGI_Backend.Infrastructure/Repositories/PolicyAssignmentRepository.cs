using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class PolicyAssignmentRepository : IPolicyAssignmentRepository
    {
        private readonly EGIDbContext _context;

        public PolicyAssignmentRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PolicyAssignment policyAssignment)
        {
            await _context.PolicyAssignments.AddAsync(policyAssignment);
        }

        public async Task<PolicyAssignment?> GetByIdAsync(Guid id)
        {
            return await _context.PolicyAssignments.FindAsync(id);
        }

        public async Task<PolicyAssignment?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.PolicyAssignments
                .Include(pa => pa.CorporateClient)
                .Include(pa => pa.InsurancePlan)
                    .ThenInclude(ip => ip.Coverages)
                .Include(pa => pa.Agent)
                .Include(pa => pa.Members)
                    .ThenInclude(m => m.Dependents)
                .FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public async Task UpdateAsync(PolicyAssignment policyAssignment)
        {
            _context.PolicyAssignments.Update(policyAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PolicyAssignment>> GetAllWithDetailsAsync()
        {
            return await _context.PolicyAssignments
                .Include(pa => pa.CorporateClient)
                .Include(pa => pa.InsurancePlan)
                .Include(pa => pa.Agent)
                .Include(pa => pa.Members)
                .OrderByDescending(pa => pa.Id)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.PolicyAssignments.CountAsync();
        }

        public async Task<List<PolicyAssignment>> GetByAgentIdAsync(Guid agentId)
        {
            return await _context.PolicyAssignments
                .AsNoTracking()
                .Include(pa => pa.CorporateClient)
                .Include(pa => pa.InsurancePlan)
                .Where(pa => pa.AgentId == agentId)
                .OrderByDescending(pa => pa.CreatedAt)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalPremiumForAgentAsync(Guid agentId)
        {
            return await _context.PolicyAssignments
                .Where(pa => pa.AgentId == agentId && pa.Status == PolicyStatus.Active)
                .SumAsync(pa => pa.AnnualPremium);
        }

        public async Task<decimal> GetTotalCommissionForAgentAsync(Guid agentId)
        {
            return await _context.PolicyAssignments
                .Where(pa => pa.AgentId == agentId && pa.Status == PolicyStatus.Active)
                .SumAsync(pa => pa.CommissionAmount);
        }

        public async Task<Dictionary<Guid, decimal>> GetTotalCommissionsForAgentsAsync(List<Guid> agentIds)
        {
            return await _context.PolicyAssignments
                .Where(pa => agentIds.Contains(pa.AgentId) && pa.Status == PolicyStatus.Active)
                .GroupBy(pa => pa.AgentId)
                .Select(g => new { AgentId = g.Key, Total = g.Sum(pa => pa.CommissionAmount) })
                .ToDictionaryAsync(x => x.AgentId, x => x.Total);
        }

        public async Task<decimal> GetTotalCommissionAsync()
        {
            return await _context.PolicyAssignments
                .Where(pa => pa.Status == PolicyStatus.Active)
                .SumAsync(pa => pa.CommissionAmount);
        }

        public async Task<int> CountActiveForAgentAsync(Guid agentId)
        {
            return await _context.PolicyAssignments
                .CountAsync(pa => pa.AgentId == agentId && pa.Status == PolicyStatus.Active);
        }

        public async Task<List<PolicyAssignment>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.PolicyAssignments
                .AsNoTracking()
                .Include(pa => pa.InsurancePlan)
                .Include(pa => pa.CorporateClient)
                .Where(pa => pa.CorporateClientId == clientId)
                .OrderByDescending(pa => pa.StartDate)
                .ToListAsync();
        }

        public async Task<int> CountByClientIdAsync(Guid clientId)
        {
            return await _context.PolicyAssignments
                .CountAsync(pa => pa.CorporateClientId == clientId && pa.Status == PolicyStatus.Active);
        }

        public async Task<Dictionary<Guid, int>> GetPolicyCountsForAgentsAsync(List<Guid> agentIds)
        {
            return await _context.PolicyAssignments
                .Where(pa => agentIds.Contains(pa.AgentId))
                .GroupBy(pa => pa.AgentId)
                .Select(g => new { AgentId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.AgentId, x => x.Count);
        }
    }
}
