using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly EGIDbContext _context;

        public MemberRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdWithPolicyAsync(Guid memberId)
        {
            return await _context.Members
                .Include(m => m.PolicyAssignment)
                    .ThenInclude(pa => pa.InsurancePlan)
                        .ThenInclude(ip => ip.Coverages)
                .Include(m => m.Dependents)
                .FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task<Member?> GetByIdAsync(Guid memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            // SaveChangesAsync omitted — IUnitOfWork owns the transaction boundary
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            // SaveChangesAsync omitted — IUnitOfWork owns the transaction boundary
        }

        public async Task<List<Member>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId)
        {
            return await _context.Members
                .Include(m => m.Dependents)
                .Where(m => m.PolicyAssignmentId == policyAssignmentId)
                .ToListAsync();
        }

        public async Task<int> CountByClientIdAsync(Guid clientId)
        {
            return await _context.Members
                .CountAsync(m => m.PolicyAssignment.CorporateClientId == clientId && m.Status);
        }

        public async Task<List<Member>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Members
                .Include(m => m.Dependents)
                .Include(m => m.PolicyAssignment)
                .Where(m => m.PolicyAssignment.CorporateClientId == clientId)
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }
    }
}
