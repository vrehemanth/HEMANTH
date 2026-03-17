using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class RenewalQuoteRequestDto
    {
        public int Years { get; set; } = 1;
        public BillingFrequency BillingFrequency { get; set; } = BillingFrequency.Annual;
    }

    public class RenewalQuoteResponseDto
    {
        public decimal NewAnnualRate { get; set; } // The base annual rate for the term
        public decimal NewMonthlyPremium { get; set; } // If they choose monthly
        public decimal NewTotalPremium { get; set; } // For the selected term/years
        public int Years { get; set; }
        public BillingFrequency SelectedFrequency { get; set; }
        public string Note { get; set; } = string.Empty;
    }

    public class ConfirmRenewalDto
    {
        public int Years { get; set; } = 1;
        public BillingFrequency BillingFrequency { get; set; } = BillingFrequency.Annual;
    }
}
