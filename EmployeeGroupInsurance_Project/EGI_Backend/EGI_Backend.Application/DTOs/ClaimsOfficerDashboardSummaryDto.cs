using System;

namespace EGI_Backend.Application.DTOs
{
    public class ClaimsOfficerDashboardSummaryDto
    {
        public int TotalPendingClaimsInSystem { get; set; }
        public int ClaimsReviewedByMe { get; set; }
        public decimal ApprovedAmountByMe { get; set; }
    }
}
