using System;

namespace EGI_Backend.Application.DTOs
{
    public class EndorsementPreviewDto
    {
        public decimal PremiumAdjustment { get; set; }
        public int RemainingDaysToNextCycle { get; set; }
        public string AIExplanation { get; set; } = string.Empty;
        
        public decimal NewRecurringPremium { get; set; } // Monthly if monthly, annual if annual
        public decimal PremiumChange { get; set; } // The delta in recurring payment
        public string BillingFrequency { get; set; } = string.Empty;
    }
}
