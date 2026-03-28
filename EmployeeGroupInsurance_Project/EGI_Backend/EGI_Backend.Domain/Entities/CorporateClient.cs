using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Domain.Entities
{
    public class CorporateClient
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public BusinessCategory BusinessCategory { get; set; } = BusinessCategory.Small;
        public IndustryType IndustryType { get; set; } = IndustryType.Others;
        public VerificationStatus Status { get; set; } = VerificationStatus.Draft;
        public Guid ReviewedBy { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? RejectionReason { get; set; }
        public int ReSubmissionCount { get; set; } = 0;
        public string? Phone { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string? KybAiAnalysis { get; set; }
        public int KybAiConfidenceScore { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastHealthCheckupDate { get; set; }
        public Guid? HealthCheckupHospitalId { get; set; }
        public int HealthCheckupActualMemberCount { get; set; } = 0;
        public int HealthCheckupActualDependentCount { get; set; } = 0;
        public bool IsHealthCheckupClaimPending { get; set; } = false;
        public DateTime? HealthCheckupVerifiedAt { get; set; }

        public ICollection<CorporateClientDocument> Documents { get; set; } = new List<CorporateClientDocument>();
    }
}
