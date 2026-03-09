using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface ICustomerDashboardService
    {
        Task<CorporateClientResponseDto> GetProfileAsync(Guid userId);
        Task<CustomerDashboardSummaryDto> GetSummaryAsync(Guid userId);
        Task<CustomerDashboardOverviewDto> GetOverviewAsync(Guid userId);
        Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid userId);
        Task<List<MemberResponseDto>> GetMyMembersAsync(Guid userId);
        Task<List<ClaimResponseDto>> GetMyClaimsAsync(Guid userId);
        Task<List<InvoiceResponseDto>> GetMyInvoicesAsync(Guid userId);
        Task<List<EndorsementResponseDto>> GetMyEndorsementsAsync(Guid userId);
    }
}
