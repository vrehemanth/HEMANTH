using System;
using System.Collections.Generic;

namespace EGI_Backend.Application.DTOs
{
    public class ClaimDocumentDto
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }

    public class ClaimDetailResponseDto
    {
        public Guid Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public string PolicyNo { get; set; } = string.Empty;
        public Guid PolicyAssignmentId { get; set; }
        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public Guid? DependentId { get; set; }
        public string? DependentName { get; set; }
        public string? DependentRelationship { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string ClaimReason { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
        public string? ReviewedByName { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public List<ClaimDocumentDto> Documents { get; set; } = new();
    }
}
