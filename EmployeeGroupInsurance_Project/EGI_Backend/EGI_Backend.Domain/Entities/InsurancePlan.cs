using System;
using System.Collections.Generic;

namespace EGI_Backend.Domain.Entities
{
    public class InsurancePlan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal BasePremium { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public bool HasHealthCheckup { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for coverages
        public ICollection<PlanCoverage> Coverages { get; set; } = new List<PlanCoverage>();
    }
}
