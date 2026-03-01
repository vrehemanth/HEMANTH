using System;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClaimService
    {
        Task<string> SubmitClaimAsync(Guid corporateClientUserId, SubmitClaimDto dto);
        Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto);
        Task<List<ClaimResponseDto>> GetClaimsByPolicyAsync(Guid policyAssignmentId);
        Task<List<ClaimResponseDto>> GetPendingClaimsAsync();
        Task<List<ClaimDetailResponseDto>> GetClaimsByMemberAsync(Guid memberId);
        Task<ClaimDetailResponseDto> GetClaimDetailAsync(Guid claimId);

        // Remaining coverage per claim type for a member or dependent
        Task<CoverageSummaryDto> GetCoverageSummaryAsync(Guid memberId, Guid? dependentId);
    }
}
