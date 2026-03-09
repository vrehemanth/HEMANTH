using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
        Task<List<CorporateClientResponseDto>> GetPendingClientsAsync();
        Task<bool> ReviewClientAsync(Guid id, Guid adminId, ReviewCorporateClientDto dto);
        Task<List<PolicyAssignmentResponseDto>> GetAllPolicyAssignmentsAsync();
        Task<List<ClaimResponseDto>> GetAllClaimsAsync();
        Task<List<UserResponseDto>> GetAllStaffAsync(string role);
        Task<bool> ToggleUserStatusAsync(Guid userId);
        Task<List<CorporateClientResponseDto>> GetAllClientsAsync();
        Task<List<AuditLogResponseDto>> GetAuditLogsAsync(string? userId = null, string? entityName = null);

    }
}
