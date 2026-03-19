using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Constants
{
    public static class BusinessRules
    {
        // ─── Organization Thresholds (Headcount: Members + Dependents) ───
        public const int EnterpriseThreshold = 1000;
        public const int LargeThreshold = 250;
        public const int MediumThreshold = 50;

        // ─── Commission Rates (Decimal Form) ───
        public const decimal RateEnterprise = 0.02m;
        public const decimal RateLarge = 0.04m;
        public const decimal RateMedium = 0.06m;
        public const decimal RateSmall = 0.08m;

        // ─── Pricing & Billing ───
        public const decimal AnnualBillingDiscount = 0.05m; // 5% Discount
        public const int InvoiceDueGraceDays = 7;
        public const int ClaimInitialWaitingPeriodDays = 0; // Waiting period removed per user request

        // ─── Helper Methods ─────────────────────────────────────────────
        
        public static BusinessCategory GetCategoryByHeadcount(int headcount)
        {
            return headcount switch
            {
                > EnterpriseThreshold => BusinessCategory.Enterprise,
                > LargeThreshold => BusinessCategory.Large,
                > MediumThreshold => BusinessCategory.Medium,
                _ => BusinessCategory.Small
            };
        }

        public static decimal GetCommissionRate(BusinessCategory category)
        {
            return category switch
            {
                BusinessCategory.Enterprise => RateEnterprise,
                BusinessCategory.Large => RateLarge,
                BusinessCategory.Medium => RateMedium,
                BusinessCategory.Small => RateSmall,
                _ => RateSmall
            };
        }

        public static decimal GetIndustryMultiplier(IndustryType industry)
        {
            return industry switch
            {
                IndustryType.IT => 1.00m,
                IndustryType.Banking => 1.08m,
                IndustryType.Education => 1.12m,
                IndustryType.Others => 1.20m,
                IndustryType.Retail => 1.32m,
                IndustryType.Healthcare => 1.40m,
                IndustryType.Hospitality => 1.44m,
                IndustryType.Logistics => 1.48m,
                IndustryType.Manufacturing => 1.72m,
                IndustryType.Construction => 1.80m,
                IndustryType.OilAndGas => 1.88m,
                IndustryType.Mining => 2.00m,
                _ => 1.20m // Default to 'Others'
            };
        }

        public static decimal GetAgeMultiplier(DateTime dob)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;

            return age switch
            {
                < 18 => 0.7m,
                <= 25 => 0.9m,
                <= 35 => 1.0m,
                <= 45 => 1.2m,
                <= 55 => 1.5m,
                <= 65 => 2.0m,
                _ => 2.5m
            };
        }
    }
}
