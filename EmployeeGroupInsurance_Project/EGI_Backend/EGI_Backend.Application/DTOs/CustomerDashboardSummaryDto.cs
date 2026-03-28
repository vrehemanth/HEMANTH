using System;

namespace EGI_Backend.Application.DTOs
{
    public class CustomerDashboardSummaryDto
    {
        public int TotalPolicies { get; set; }
        public int TotalMembers { get; set; }
        public int TotalDependents { get; set; }
        public int PendingClaims { get; set; }
        public int UnpaidInvoices { get; set; }
        public int TotalEndorsements { get; set; }
        public decimal TotalPremiumDue { get; set; }
        public decimal TotalPendingCredit { get; set; }
    }
}
