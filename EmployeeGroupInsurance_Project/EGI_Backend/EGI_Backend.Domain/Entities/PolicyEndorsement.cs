using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class PolicyEndorsement
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PolicyAssignmentId { get; set; }
        public PolicyAssignment PolicyAssignment { get; set; }

        public EndorsementType Type { get; set; }

        public string Description { get; set; } = string.Empty;

        // Stores the details of the member/dependent being added or removed as a JSON string
        // so that the agent can review exactly what is being endorsed before applying it.
        public string EndorsementData { get; set; } = string.Empty; 

        public EndorsementStatus Status { get; set; } = EndorsementStatus.Pending;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PremiumAdjustment { get; set; } = 0m;

        public Guid RequestedByUserId { get; set; }
        public User RequestedByUser { get; set; }

        public Guid? ReviewedByUserId { get; set; }
        public User? ReviewedByUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; set; }
    }
}
