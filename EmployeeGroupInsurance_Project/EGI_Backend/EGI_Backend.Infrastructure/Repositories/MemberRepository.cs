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
                .Include(m => m.Dependents.Where(d => d.IsActive))
                .Where(m => m.PolicyAssignmentId == policyAssignmentId && m.Status)
                .ToListAsync();
        }

        public async Task<int> CountByClientIdAsync(Guid clientId)
        {
            return await _context.Members
                .CountAsync(m => m.PolicyAssignment.CorporateClientId == clientId && m.Status);
        }

        public async Task<int> CountActiveAsync()
        {
            return await _context.Members.CountAsync(m => m.Status);
        }

        public async Task<int> CountActiveDependentsAsync()
        {
            return await _context.Dependents.CountAsync(d => d.IsActive && d.Member.Status);
        }

        public async Task<List<Member>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Members
                .Include(m => m.Dependents)
                .Include(m => m.PolicyAssignment)
                .Where(m => m.CorporateClientId == clientId)
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }

        public async Task<Member?> GetByEmployeeCodeAndClientAsync(string employeeCode, Guid clientId)
        {
            return await _context.Members
                .Include(m => m.Dependents)
                .FirstOrDefaultAsync(m => m.EmployeeCode == employeeCode && m.CorporateClientId == clientId);
        }

        public async Task<Member?> SearchAsync(string identifier)
        {
            var lowerId = identifier.ToLower().Trim();
            // Strict check: partial ID search only if >= 8 chars to avoid accidental hits on short hex strings
            bool isFullSearch = lowerId.Length >= 8;

            return await _context.Members
                .Include(m => m.Dependents)
                .Include(m => m.PolicyAssignment)
                .FirstOrDefaultAsync(m => 
                    m.Status && (
                    m.EmployeeCode.ToLower() == lowerId || 
                    (isFullSearch && m.Id.ToString().ToLower().StartsWith(lowerId)) ||
                    (isFullSearch && m.Id.ToString().ToLower().Replace("-", "").StartsWith(lowerId.Replace("-", ""))))
                );
        }

        public async Task<Dependent?> SearchDependentAsync(string identifier)
        {
            var lowerId = identifier.ToLower().Trim();
            bool isFullSearch = lowerId.Length >= 8;

            return await _context.Dependents
                .Include(d => d.Member)
                    .ThenInclude(m => m.PolicyAssignment)
                .FirstOrDefaultAsync(d => 
                    d.IsActive && d.Member.Status && (
                    (isFullSearch && d.Id.ToString().ToLower().StartsWith(lowerId)) ||
                    (isFullSearch && d.Id.ToString().ToLower().Replace("-", "").StartsWith(lowerId.Replace("-", ""))))
                );
        }
    }
}
