using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAgentDashboardService
    {
        Task<AgentDashboardSummaryDto> GetSummaryAsync(Guid agentId);
        Task<List<CorporateClientResponseDto>> GetMyCustomersAsync(Guid agentId);
        Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid agentId);
        Task<List<AuditLogResponseDto>> GetCommissionLogsAsync(Guid agentId);
    }
}
