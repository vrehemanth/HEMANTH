using System.Collections.Generic;

namespace EGI_Backend.Application.DTOs
{
    public class CustomerDashboardOverviewDto
    {
        public CustomerDashboardSummaryDto Summary { get; set; } = new();
        public List<PolicyAssignmentResponseDto> RecentPolicies { get; set; } = new();
        public List<ClaimResponseDto> RecentClaims { get; set; } = new();
        public List<InvoiceResponseDto> RecentInvoices { get; set; } = new();
        public List<EndorsementResponseDto> RecentEndorsements { get; set; } = new();
    }
}
