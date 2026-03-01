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
    }
}
