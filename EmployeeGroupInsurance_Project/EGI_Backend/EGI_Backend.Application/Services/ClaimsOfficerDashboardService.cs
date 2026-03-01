using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class ClaimsOfficerDashboardService : IClaimsOfficerDashboardService
    {
        private readonly IClaimRepository _claimRepo;

        public ClaimsOfficerDashboardService(IClaimRepository claimRepo)
        {
            _claimRepo = claimRepo;
        }

        public async Task<ClaimsOfficerDashboardSummaryDto> GetSummaryAsync(Guid officerId)
        {
            return new ClaimsOfficerDashboardSummaryDto
            {
                TotalPendingClaimsInSystem = await _claimRepo.CountPendingAsync(),
                ClaimsReviewedByMe = await _claimRepo.CountReviewedByOfficerAsync(officerId),
                ApprovedAmountByMe = await _claimRepo.GetTotalApprovedAmountByOfficerAsync(officerId)
            };
        }
    }
}
