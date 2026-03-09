using EGI_Backend.Domain.Enums;
using System;

namespace EGI_Backend.Application.DTOs
{
    public class SubmitEndorsementDto
    {
        public Guid PolicyAssignmentId { get; set; }
        public EndorsementType Type { get; set; }
        public string Description { get; set; } = string.Empty;

        // Use object here so Swagger allows direct JSON entry instead of escaped string!
        public object EndorsementData { get; set; } = null!;
    }

    public class ReviewEndorsementDto
    {
        public EndorsementStatus Status { get; set; }
    }

    public class EndorsementResponseDto
    {
        public Guid Id { get; set; }
        public Guid PolicyAssignmentId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EndorsementData { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal PremiumAdjustment { get; set; }
        public decimal CommissionAdjustment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}
