using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class Claim
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ClaimNumber { get; set; } = string.Empty;

        public Guid PolicyAssignmentId { get; set; }
        public PolicyAssignment PolicyAssignment { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        // Null if this is a Member's own claim (not a Dependent's)
        public Guid? DependentId { get; set; }
        public Dependent? Dependent { get; set; }

        // Reuse CoverageType as ClaimType (Health, Life, Accident, CriticalIllness)
        public CoverageType ClaimType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ClaimAmount { get; set; }

        [Required]
        public string ClaimReason { get; set; } = string.Empty;

        public DateTime IncidentDate { get; set; }
        public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

        // Anti-duplicate protection token
        public string? SubmissionToken { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        // ClaimsOfficer who reviewed this claim
        public Guid? ReviewedBy { get; set; }
        public User? ReviewedByUser { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string? RejectionReason { get; set; }
        
        // --- New Features Properties ---
        public bool IsAutoApproved { get; set; } = false;
        public bool RequiresAdminApproval { get; set; } = false;
        
        public Guid? AdminApprovedBy { get; set; }
        public User? AdminApprovedByUser { get; set; }
        public DateTime? AdminApprovedAt { get; set; }

        // OCR Extracted Data (Pre-fill fields)
        public string? ExtractedHospitalName { get; set; }
        public DateTime? ExtractedBillDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtractedBillAmount { get; set; }

        public DateTime? ExtractedDateOfDeath { get; set; }
        public string? ExtractedCauseOfDeath { get; set; }
        public string? ExtractedFirNumber { get; set; }
        public string? ExtractedPoliceStation { get; set; }
        public DateTime? ExtractedIncidentDate { get; set; }
        public string? ExtractedDiagnosis { get; set; }
 
        // Fraud Detection (New Feature)
        public int FraudScore { get; set; } = 0; // 0-100
        public string? FraudAnalysis { get; set; } // Detailed reasons
        public bool IsSuspectedFraud { get; set; } = false;
 
        // Fraud Override Tracking
        public bool IsFraudOverridden { get; set; } = false;
        public Guid? FraudOverriddenBy { get; set; }
        public string? FraudOverrideReason { get; set; }
 
        // Navigation property for supporting documents
        public ICollection<ClaimDocument> Documents { get; set; } = new List<ClaimDocument>();
    }
}
