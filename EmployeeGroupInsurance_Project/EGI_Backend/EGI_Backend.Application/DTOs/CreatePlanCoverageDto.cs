using System;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class CreatePlanCoverageDto
    {
        [Required]
        public string Type { get; set; } = string.Empty; // e.g., "Health", "Life"
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CoverageAmount { get; set; }
        
        [Required]
        public string CoveredGroup { get; set; } = string.Empty; // e.g., "EmployeeOnly"
    }
}
