using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClaimService
    {
        Task<string> SubmitClaimAsync(Guid corporateClientUserId, SubmitClaimDto dto);
        Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto);
        Task<List<ClaimResponseDto>> GetClaimsByPolicyAsync(Guid policyAssignmentId, Guid callerUserId, string role);
        Task<List<ClaimResponseDto>> GetPendingClaimsAsync();
        Task<List<ClaimDetailResponseDto>> GetClaimsByMemberAsync(Guid memberId, Guid callerUserId, string role);
        Task<ClaimDetailResponseDto> GetClaimDetailAsync(Guid claimId, Guid callerUserId, string role);

        // Remaining coverage per claim type for a member or dependent
        Task<CoverageSummaryDto> GetCoverageSummaryAsync(Guid memberId, Guid? dependentId);
        Task<List<ClaimResponseDto>> GetClaimsReviewedByOfficerAsync(Guid officerId);
        Task TakeClaimAsync(Guid officerId, Guid claimId);
        Task ReleaseClaimAsync(Guid claimId);
        Task<(byte[] content, string contentType, string fileName)> GetSecureDocumentAsync(Guid userId, string role, Guid documentId);
    }
}
