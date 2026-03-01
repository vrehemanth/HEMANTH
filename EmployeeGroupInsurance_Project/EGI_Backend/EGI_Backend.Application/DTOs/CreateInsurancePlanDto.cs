using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Application.DTOs
{
    public class CreateInsurancePlanDto
    {
        [Required]
        public string PlanCode { get; set; } = string.Empty;
        [Required]
        public string PlanName { get; set; } = string.Empty;
        [Required]
        public decimal BasePremium { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        public List<CreatePlanCoverageDto> Coverages { get; set; } = new List<CreatePlanCoverageDto>();
    }
}
