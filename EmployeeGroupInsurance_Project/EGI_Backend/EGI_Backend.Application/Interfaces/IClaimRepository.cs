using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClaimRepository
    {
        Task AddAsync(Claim claim);
        Task<Claim?> GetByIdAsync(Guid id);
        Task<List<Claim>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId);
        Task<List<Claim>> GetByMemberIdAsync(Guid memberId);
        Task<List<Claim>> GetPendingClaimsAsync();

        // Sum of all APPROVED claims for a member for a specific claim type (or all if null)
        Task<decimal> GetApprovedClaimsTotalAsync(Guid memberId, Guid? dependentId, CoverageType? claimType);

        Task<List<Claim>> GetAllAsync();
        Task<int> CountAsync();
        Task<int> CountPendingAsync();
        Task<decimal> GetTotalPayoutsAsync();
        Task<int> CountPendingForAgentCustomersAsync(Guid agentId);
        Task<int> CountReviewedByOfficerAsync(Guid officerId);
        Task<int> CountApprovedByOfficerAsync(Guid officerId);
        Task<int> CountRejectedByOfficerAsync(Guid officerId);
        Task<decimal> GetTotalApprovedAmountByOfficerAsync(Guid officerId);
        Task<int> CountPendingForClientAsync(Guid clientId);
        Task<List<Claim>> GetByClientIdAsync(Guid clientId);
        Task<List<Claim>> GetByClientIdsAsync(List<Guid> clientIds);
        Task<double> GetApprovalRateAsync();
        Task<decimal> GetAverageClaimAmountAsync();
        Task<double> GetAverageProcessingTimeDaysAsync();
        Task<List<Claim>> GetReviewedByOfficerAsync(Guid officerId);
        Task<double> GetApprovalRateForClientsAsync(List<Guid> clientIds);
        Task<List<Claim>> GetTopClaimsForClientsAsync(List<Guid> clientIds, int count);
    }
}
