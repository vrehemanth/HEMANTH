using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class CorporateClientRepository : ICorporateClientRepository
    {
        private readonly EGIDbContext _context;

        public CorporateClientRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<CorporateClient?> GetByUserIdAsync(Guid userId)
        => await _context.CorporateClients
            .Include(c => c.User)
            .Include(c => c.Documents)
            .FirstOrDefaultAsync(c => c.UserId == userId);


        public async Task AddAsync(CorporateClient client)
            => await _context.CorporateClients.AddAsync(client);

        public async Task<List<CorporateClient>> GetPendingAsync()
            => await _context.CorporateClients
                .Include(c => c.User)
                .Include(c => c.Documents)
                .Where(c => c.Status == VerificationStatus.Pending)
                .ToListAsync();

        public async Task<int> CountPendingAsync()
            => await _context.CorporateClients.CountAsync(c => c.Status == VerificationStatus.Pending);

        public async Task<List<CorporateClient>> GetAllAsync()
            => await _context.CorporateClients
                .Include(c => c.User)
                .Include(c => c.Documents)
                .ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<CorporateClient?> GetByIdAsync(Guid id)
            => await _context.CorporateClients
                .Include(c => c.User)
                .Include(c => c.Documents)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<int> GetTotalLiveMembersCountAsync(Guid clientId)
        {
            var activePolicies = await _context.PolicyAssignments
                .Where(p => p.CorporateClientId == clientId && p.Status == PolicyStatus.Active)
                .Include(p => p.Members)
                    .ThenInclude(m => m.Dependents)
                .ToListAsync();

            int count = 0;
            foreach (var policy in activePolicies)
            {
                count += policy.Members.Count;
                count += policy.Members.Sum(m => m.Dependents.Count);
            }
            return count;
        }
    }
}
