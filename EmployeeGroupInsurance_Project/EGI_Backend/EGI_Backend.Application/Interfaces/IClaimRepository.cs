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
        Task<bool> IsDuplicateAsync(string submissionToken);

        // Sum of all UTILIZED claims (Approved + Pending) for a specific policy period
        Task<decimal> GetApprovedClaimsTotalAsync(Guid policyAssignmentId, Guid memberId, Guid? dependentId, CoverageType? claimType);

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
 
        // Fraud Check: Check if this specific bill has been submitted before
        Task<bool> IsDuplicateBillAsync(string hospitalName, decimal amount, DateTime billDate, Guid? currentClaimId = null);
        Task<ClaimDocument?> GetDocumentByIdAsync(Guid id);
    }
}
