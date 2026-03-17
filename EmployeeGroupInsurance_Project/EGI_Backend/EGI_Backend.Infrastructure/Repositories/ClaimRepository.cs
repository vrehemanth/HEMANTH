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
                    .ThenInclude(pa => pa.CorporateClient)
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
                .Where(c => c.Status == ClaimStatus.Pending || c.Status == ClaimStatus.InReview)
                .OrderBy(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateAsync(string submissionToken)
        {
            if (string.IsNullOrEmpty(submissionToken)) return false;
            return await _context.Claims.AnyAsync(c => c.SubmissionToken == submissionToken);
        }

        // Cumulative utilized claims (Approved, Pending, or InReview) within a specific policy period
        public async Task<decimal> GetApprovedClaimsTotalAsync(Guid policyAssignmentId, Guid memberId, Guid? dependentId, CoverageType? claimType)
        {
            var query = _context.Claims.AsQueryable();
 
            // We count Approved AND all pending states to prevent race condition "draining"
            var activeStatuses = new[] { ClaimStatus.Approved, ClaimStatus.Pending, ClaimStatus.InReview, ClaimStatus.PendingAdminApproval };
 
            query = query.Where(c => 
                c.PolicyAssignmentId == policyAssignmentId &&
                c.MemberId == memberId && 
                c.DependentId == dependentId && 
                activeStatuses.Contains(c.Status));
 
            if (claimType.HasValue)
            {
                query = query.Where(c => c.ClaimType == claimType.Value);
            }
 
            return await query.SumAsync(c => (decimal?)c.ClaimAmount) ?? 0m;
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

        public async Task<int> CountApprovedByOfficerAsync(Guid officerId)
        {
            return await _context.Claims.CountAsync(c => c.ReviewedBy == officerId && c.Status == ClaimStatus.Approved);
        }

        public async Task<int> CountRejectedByOfficerAsync(Guid officerId)
        {
            return await _context.Claims.CountAsync(c => c.ReviewedBy == officerId && c.Status == ClaimStatus.Rejected);
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
                .AsNoTracking()
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Include(c => c.Documents)
                .Where(c => c.PolicyAssignment.CorporateClientId == clientId)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<List<Claim>> GetByClientIdsAsync(List<Guid> clientIds)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Include(c => c.Documents)
                .Where(c => c.PolicyAssignment != null && clientIds.Contains(c.PolicyAssignment.CorporateClientId))
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<double> GetApprovalRateAsync()
        {
            var stats = await _context.Claims
                .Where(c => c.Status == ClaimStatus.Approved || c.Status == ClaimStatus.Rejected)
                .GroupBy(c => 1)
                .Select(g => new
                {
                    Total = g.Count(),
                    Approved = g.Count(c => c.Status == ClaimStatus.Approved)
                })
                .OrderBy(x => 1)
                .FirstOrDefaultAsync();

            if (stats == null || stats.Total == 0) return 0;

            return Math.Round((double)stats.Approved / stats.Total * 100, 1);
        }

        public async Task<decimal> GetAverageClaimAmountAsync()
        {
            return await _context.Claims.AverageAsync(c => (decimal?)c.ClaimAmount) ?? 0m;
        }

        public async Task<double> GetAverageProcessingTimeDaysAsync()
        {
            var processed = await _context.Claims
                .Where(c => c.Status != ClaimStatus.Pending && c.ReviewedAt.HasValue)
                .Select(c => (c.ReviewedAt.Value - c.ClaimDate).TotalDays)
                .ToListAsync();

            if (!processed.Any()) return 0;
            return Math.Round(processed.Average(), 1);
        }

        public async Task<List<Claim>> GetReviewedByOfficerAsync(Guid officerId)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Where(c => c.ReviewedBy == officerId)
                .OrderByDescending(c => c.ReviewedAt)
                .ToListAsync();
        }

        public async Task<double> GetApprovalRateForClientsAsync(List<Guid> clientIds)
        {
            var stats = await _context.Claims
                .Where(c => c.PolicyAssignment != null && clientIds.Contains(c.PolicyAssignment.CorporateClientId))
                .Where(c => c.Status == ClaimStatus.Approved || c.Status == ClaimStatus.Rejected)
                .GroupBy(c => 1)
                .Select(g => new {
                    Total = g.Count(),
                    Approved = g.Count(c => c.Status == ClaimStatus.Approved)
                })
                .OrderBy(x => 1)
                .FirstOrDefaultAsync();

            if (stats == null || stats.Total == 0) return 0;
            return Math.Round((double)stats.Approved / stats.Total * 100, 1);
        }

        public async Task<List<Claim>> GetTopClaimsForClientsAsync(List<Guid> clientIds, int count)
        {
            return await _context.Claims
                .Include(c => c.Member)
                .Include(c => c.Dependent)
                .Include(c => c.PolicyAssignment)
                .Include(c => c.Documents)
                .Where(c => c.PolicyAssignment != null && clientIds.Contains(c.PolicyAssignment.CorporateClientId))
                .OrderByDescending(c => c.ClaimDate)
                .Take(count)
                .ToListAsync();
        }
 
        public async Task<bool> IsDuplicateBillAsync(string hospitalName, decimal amount, DateTime billDate, Guid? currentClaimId = null)
        {
            if (string.IsNullOrEmpty(hospitalName) || amount <= 0) return false;
 
            var normalizedName = hospitalName.Trim().ToUpper();

            return await _context.Claims
                .AnyAsync(c => c.ExtractedHospitalName != null && 
                               c.ExtractedHospitalName.Trim().ToUpper() == normalizedName && 
                               c.ExtractedBillAmount == amount && 
                               c.ExtractedBillDate == billDate &&
                               c.Id != currentClaimId);
        }
        public async Task<ClaimDocument?> GetDocumentByIdAsync(Guid id)
        {
            return await _context.ClaimDocuments
                .Include(d => d.Claim)
                    .ThenInclude(c => c.PolicyAssignment)
                        .ThenInclude(pa => pa.CorporateClient)
                .Include(d => d.Claim)
                    .ThenInclude(c => c.Member)
                        .ThenInclude(m => m.CorporateClient)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
