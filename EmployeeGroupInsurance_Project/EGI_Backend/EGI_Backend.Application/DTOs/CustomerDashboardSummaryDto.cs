using System;

namespace EGI_Backend.Application.DTOs
{
    public class CustomerDashboardSummaryDto
    {
        public int TotalPolicies { get; set; }
        public int TotalMembers { get; set; }
        public int PendingClaims { get; set; }
        public int UnpaidInvoices { get; set; }
        public decimal TotalPremiumDue { get; set; }
    }
}
