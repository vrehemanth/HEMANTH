using System;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class ClaimResponseDto
    {
        public Guid Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public Guid PolicyAssignmentId { get; set; }
        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public Guid? DependentId { get; set; }
        public string? DependentName { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string ClaimReason { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public DateTime ClaimDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? ReviewedByName { get; set; }
        public string? InReviewByOfficerName { get; set; }
        public bool IsAutoApproved { get; set; }
        public bool RequiresAdminApproval { get; set; }

        public string? ExtractedHospitalName { get; set; }
        public DateTime? ExtractedBillDate { get; set; }
        public decimal? ExtractedBillAmount { get; set; }
        public DateTime? ExtractedDateOfDeath { get; set; }
        public string? ExtractedCauseOfDeath { get; set; }
        public string? ExtractedFirNumber { get; set; }
        public string? ExtractedPoliceStation { get; set; }
        public DateTime? ExtractedIncidentDate { get; set; }
        public string? ExtractedDiagnosis { get; set; }
 
        public int FraudScore { get; set; }
        public bool IsSuspectedFraud { get; set; }

        public int AIConfidenceScore { get; set; }
        public string? AIAdjudicationReasoning { get; set; }
        public bool IsAIApproved { get; set; }
    }
}
