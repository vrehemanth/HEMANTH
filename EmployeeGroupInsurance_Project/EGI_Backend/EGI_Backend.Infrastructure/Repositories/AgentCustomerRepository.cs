using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class AgentCustomerRepository : IAgentCustomerRepository
    {
        private readonly EGIDbContext _context;

        public AgentCustomerRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AgentCustomer agentCustomer)
        {
            await _context.AgentCustomers.AddAsync(agentCustomer);
        }

        public async Task<int> GetCustomerCountForAgentAsync(Guid agentId)
        {
            return await _context.AgentCustomers.CountAsync(ac => ac.AgentId == agentId);
        }

        public async Task<bool> HasAssignedAgentAsync(Guid clientId)
        {
            return await _context.AgentCustomers.AnyAsync(ac => ac.CorporateClientId == clientId);
        }

        public async Task<AgentCustomer?> GetByCorporateClientIdAsync(Guid clientId)
        {
            return await _context.AgentCustomers.FirstOrDefaultAsync(ac => ac.CorporateClientId == clientId);
        }

        public async Task<List<AgentCustomer>> GetByAgentIdWithDetailsAsync(Guid agentId)
        {
            return await _context.AgentCustomers
                .Include(ac => ac.CorporateClient)
                .Where(ac => ac.AgentId == agentId)
                .ToListAsync();
        }
    }
}
