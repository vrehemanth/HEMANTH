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

        public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        // ClaimsOfficer who reviewed this claim
        public Guid? ReviewedBy { get; set; }
        public User? ReviewedByUser { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string? RejectionReason { get; set; }

        // Navigation property for supporting documents
        public ICollection<ClaimDocument> Documents { get; set; } = new List<ClaimDocument>();
    }
}
