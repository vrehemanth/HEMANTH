using System;

namespace EGI_Backend.Application.DTOs
{
    public class PolicyAssignmentResponseDto
    {
        public Guid Id { get; set; }
        public string PolicyNo { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal AnnualPremium { get; set; }
        public decimal CommissionAmount { get; set; }
        public Guid InsurancePlanId { get; set; }
        public string BillingFrequency { get; set; } = string.Empty;
        public int BillingDay { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool CanRenew { get; set; }
    }
}
