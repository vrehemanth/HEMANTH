using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Application.DTOs
{
    public class UpdateInsurancePlanDto
    {
        [Required]
        public string PlanName { get; set; } = string.Empty;
        [Required]
        public decimal BasePremium { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }

        public List<CreatePlanCoverageDto> Coverages { get; set; } = new List<CreatePlanCoverageDto>();
    }
}
