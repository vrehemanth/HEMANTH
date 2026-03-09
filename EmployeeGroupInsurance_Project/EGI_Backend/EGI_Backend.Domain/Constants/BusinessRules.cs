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
        public const int ClaimInitialWaitingPeriodDays = 30; // Standard Fraud Prevention

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
    }
}
