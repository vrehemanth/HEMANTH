using System;

namespace EGI_Backend.Application.DTOs
{
    public class PlanCoverageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public string CoveredGroup { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
