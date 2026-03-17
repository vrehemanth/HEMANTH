using System;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Interfaces
{
    public interface IPolicyAssignmentService
    {
        Task<string> ProcessMembersExcelAsync(UploadMembersDto dto);
        Task<RenewalQuoteResponseDto> GetRenewalQuoteAsync(Guid policyId, Guid corporateClientUserId, int years, BillingFrequency frequency);
        Task<string> RenewPolicyAsync(Guid policyId, Guid corporateClientUserId, int years, BillingFrequency frequency);
        Task UpdatePolicyStatusesAsync(Guid clientId);
        Task<bool> TogglePolicyStatusAsync(Guid policyId);
    }
}
