using System;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class PlanCoverage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Foreign Key to the Insurance Plan
        public Guid InsurancePlanId { get; set; }
        public InsurancePlan InsurancePlan { get; set; } = null!;

        public CoverageType Type { get; set; }
        public decimal CoverageAmount { get; set; }
        
        // Determines who is eligible for this specific coverage amount
        public CoveredGroup CoveredGroup { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
