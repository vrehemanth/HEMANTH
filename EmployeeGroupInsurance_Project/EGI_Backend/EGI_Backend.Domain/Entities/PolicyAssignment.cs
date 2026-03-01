using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class PolicyAssignment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string PolicyNo { get; set; } = string.Empty;

        public Guid CorporateClientId { get; set; }
        public CorporateClient CorporateClient { get; set; }

        public Guid InsurancePlanId { get; set; }
        public InsurancePlan InsurancePlan { get; set; }

        public Guid AgentId { get; set; }
        public User Agent { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PolicyStatus Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualPremium { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPremium { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CommissionAmount { get; set; } = 0m;
        
        public BillingFrequency BillingFrequency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
