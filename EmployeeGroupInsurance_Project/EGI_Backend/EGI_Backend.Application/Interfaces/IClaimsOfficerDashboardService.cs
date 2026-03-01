using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClaimsOfficerDashboardService
    {
        Task<ClaimsOfficerDashboardSummaryDto> GetSummaryAsync(Guid officerId);
    }
}
