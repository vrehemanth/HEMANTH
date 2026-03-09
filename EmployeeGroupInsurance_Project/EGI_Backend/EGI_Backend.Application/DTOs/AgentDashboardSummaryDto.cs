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
        
        // New requested fields
        public int PoliciesSoldThisMonth { get; set; }
        public decimal PendingPremium { get; set; }
        
        public List<AgentRecentClaimDto> RecentClaims { get; set; } = new();
        public decimal ProjectedCommission { get; set; }
        public double ClaimApprovalRate { get; set; }
        public List<CustomerPendingPremiumDto> CustomerPendingPremiums { get; set; } = new();
    }

    public class CustomerPendingPremiumDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    public class AgentRecentClaimDto
    {
        public Guid Id { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<ClaimDocumentDto> Documents { get; set; } = new();
    }
}
