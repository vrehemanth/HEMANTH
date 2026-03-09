using System;

namespace EGI_Backend.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int AgentCount { get; set; }
        public int CustomerCount { get; set; }
        public int ClaimsOfficerCount { get; set; }
        public int PendingClientsCount { get; set; }
        public int TotalClaimsCount { get; set; }
        public int PendingClaimsCount { get; set; }
        public int TotalPoliciesCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalPayouts { get; set; }
        public decimal TotalCommissionPayouts { get; set; }
        public decimal NetProfit => TotalRevenue - TotalPayouts - TotalCommissionPayouts;
        
        // Extended Analytical Data
        public int RecentActivitiesCount { get; set; }
        public double ClaimApprovalRate { get; set; }
        public decimal AverageClaimAmount { get; set; }
        public List<RecentAuditLogDto> TopRecentLogs { get; set; } = new();
        public List<TopAgentDto> TopAgents { get; set; } = new();
    }

    public class TopAgentDto
    {
        public Guid AgentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalCommission { get; set; }
        public int PolicyCount { get; set; }
    }

    public class RecentAuditLogDto
    {
        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
