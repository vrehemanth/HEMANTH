using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClaimsOfficerDashboardService
    {
        Task<ClaimsOfficerDashboardSummaryDto> GetSummaryAsync(Guid officerId);
        Task<List<CorporateClientResponseDto>> GetPendingHealthCheckupsAsync();
        Task<bool> UpdateHealthCheckupActualsAsync(Guid corporateClientId, int memberCount, int dependentCount);
    }
}
