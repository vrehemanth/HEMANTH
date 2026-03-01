using System;

namespace EGI_Backend.Application.DTOs
{
    public class AgentDashboardSummaryDto
    {
        public int TotalCustomers { get; set; }
        public int ActivePolicies { get; set; }
        public decimal TotalPremiumHandled { get; set; }
        public decimal TotalCommissionEarned { get; set; }
        public int PendingClaimsForCustomers { get; set; }
    }
}
