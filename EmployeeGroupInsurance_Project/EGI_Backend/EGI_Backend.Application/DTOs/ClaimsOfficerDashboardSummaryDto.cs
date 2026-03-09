using System;

namespace EGI_Backend.Application.DTOs
{
    public class ClaimsOfficerDashboardSummaryDto
    {
        public int TotalPendingClaimsInSystem { get; set; }
        public int ClaimsReviewedByMe { get; set; }
        public decimal ApprovedAmountByMe { get; set; }
        
        // New analytics fields
        public int PendingClaimsCount { get; set; }
        public int ApprovedClaimsCount { get; set; }
        public int RejectedClaimsCount { get; set; }
        public double AverageProcessingTimeDays { get; set; }
    }
}
