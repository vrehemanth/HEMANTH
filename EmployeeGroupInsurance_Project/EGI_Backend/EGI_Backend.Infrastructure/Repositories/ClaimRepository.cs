using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly EGIDbContext _context;

        public ClaimRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Claim claim)
        {
            await _context.Claims.AddAsync(claim);
        }

        public async Task<Claim?> GetByIdAsync(Guid id)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Include(c => c.Documents)       // Include supporting documents
                .Include(c => c.ReviewedByUser)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Claim>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Where(c => c.PolicyAssignmentId == policyAssignmentId)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<List<Claim>> GetByMemberIdAsync(Guid memberId)
        {
            return await _context.Claims
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Include(c => c.Documents)
                .Where(c => c.MemberId == memberId)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<List<Claim>> GetPendingClaimsAsync()
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderBy(c => c.ClaimDate)
                .ToListAsync();
        }

        // Cumulative approved claims for a specific member/dependent + claim type
        public async Task<decimal> GetApprovedClaimsTotalAsync(Guid memberId, Guid? dependentId, CoverageType claimType)
        {
            return await _context.Claims
                .Where(c =>
                    c.MemberId == memberId &&
                    c.DependentId == dependentId &&
                    c.ClaimType == claimType &&
                    c.Status == ClaimStatus.Approved)
                .SumAsync(c => c.ClaimAmount);
        }

        public async Task<List<Claim>> GetAllAsync()
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                    .ThenInclude(pa => pa.CorporateClient)
                .Include(c => c.ReviewedByUser)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Claims.CountAsync();
        }

        public async Task<int> CountPendingAsync()
        {
            return await _context.Claims.CountAsync(c => c.Status == ClaimStatus.Pending);
        }

        public async Task<decimal> GetTotalPayoutsAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == ClaimStatus.Approved)
                .SumAsync(c => c.ClaimAmount);
        }

        public async Task<int> CountPendingForAgentCustomersAsync(Guid agentId)
        {
            return await _context.Claims
                .Include(c => c.PolicyAssignment)
                .CountAsync(c => c.PolicyAssignment.AgentId == agentId && c.Status == ClaimStatus.Pending);
        }

        public async Task<int> CountReviewedByOfficerAsync(Guid officerId)
        {
            return await _context.Claims.CountAsync(c => c.ReviewedBy == officerId);
        }

        public async Task<decimal> GetTotalApprovedAmountByOfficerAsync(Guid officerId)
        {
            return await _context.Claims
                .Where(c => c.ReviewedBy == officerId && c.Status == ClaimStatus.Approved)
                .SumAsync(c => c.ClaimAmount);
        }

        public async Task<int> CountPendingForClientAsync(Guid clientId)
        {
            return await _context.Claims
                .Include(c => c.PolicyAssignment)
                .CountAsync(c => c.PolicyAssignment.CorporateClientId == clientId && c.Status == ClaimStatus.Pending);
        }

        public async Task<List<Claim>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Where(c => c.PolicyAssignment.CorporateClientId == clientId)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }
    }
}
