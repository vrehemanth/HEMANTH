using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Application.DTOs
{
    public class ReviewClaimDto
    {
        [Required]
        public bool IsApproved { get; set; }

        public string? RejectionReason { get; set; }
 
        // Fraud Override (New Feature)
        public bool OverrideFraud { get; set; } = false;
        public string? FraudOverrideReason { get; set; }
    }
}
