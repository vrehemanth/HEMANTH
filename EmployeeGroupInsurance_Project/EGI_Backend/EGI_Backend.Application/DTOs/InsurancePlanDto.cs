using System;
using System.Collections.Generic;

namespace EGI_Backend.Application.DTOs
{
    public class InsurancePlanDto
    {
        public Guid Id { get; set; }
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public decimal BasePremium { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PlanCoverageDto> Coverages { get; set; } = new List<PlanCoverageDto>();
    }
}
