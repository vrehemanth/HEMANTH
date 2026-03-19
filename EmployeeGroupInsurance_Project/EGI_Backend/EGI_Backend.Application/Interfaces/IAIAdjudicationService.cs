using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAIAdjudicationService
    {
        /// <summary>
        /// Analyzes a claim using LLM (Groq Llama-3.3-70b) to provide adjudication recommendations.
        /// </summary>
        Task<AIAdjudicationResult> AdjudicateClaimAsync(Claim claim, string dischargeSummaryText);

        /// <summary>
        /// Provides a plain-English explanation for a premium adjustment during policy endorsement.
        /// </summary>
        Task<string> GetEndorsementExplanationAsync(
            decimal adjustment, 
            int remainingDays, 
            string type, 
            string description, 
            string billingFrequency, 
            decimal recurringChange, 
            decimal nextRecurringTotal);

        /// <summary>
        /// Translates a raw rejection reason into a helpful, empathetic explanation with actionable appeal steps for the member.
        /// </summary>
        Task<string> GetClaimRejectionExplanationAsync(string rawReason, string claimType, decimal amount);

        /// <summary>
        /// Analyzes KYC/KYB documents (like PAN, GST, Regeistration) using Vision AI for an uploaded client.
        /// </summary>
        Task<(string Analysis, int ConfidenceScore)> AnalyzeKYBDocumentsAsync(CorporateClient client, EGI_Backend.Domain.Entities.CorporateClientDocument document);
    }

    public class AIAdjudicationResult
    {
        public bool IsApprovedRecommendation { get; set; }
        public int ConfidenceScore { get; set; } // 0-100
        public string Reasoning { get; set; } = string.Empty;
        public string AnalysisDetails { get; set; } = string.Empty;
    }
}
