using System;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class CoverageItemDto
    {
        public string ClaimType { get; set; } = string.Empty;
        public decimal TotalCoverage { get; set; }
        public decimal TotalClaimed { get; set; }
        public decimal Remaining { get; set; }
    }

    public class CoverageSummaryDto
    {
        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public Guid? DependentId { get; set; }
        public string? DependentName { get; set; }
        public string PolicyNo { get; set; } = string.Empty;
        public List<CoverageItemDto> CoverageBreakdown { get; set; } = new();
    }
}
