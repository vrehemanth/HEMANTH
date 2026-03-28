using System;
using System.Collections.Generic;

namespace EGI_Backend.Application.DTOs
{
    public class MemberSearchResultDto
    {
        public Guid MemberId { get; set; }
        public Guid? DependentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // e.g., "Primary", "Dependent (Spouse)"
        public string? PolicyNo { get; set; }
        public Guid PolicyAssignmentId { get; set; }
        public List<DependentResponseDto> Dependents { get; set; } = new();
    }
}
