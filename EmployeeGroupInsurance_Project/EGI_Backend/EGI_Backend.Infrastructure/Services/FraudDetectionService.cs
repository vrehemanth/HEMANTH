using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Services
{
    public class FraudDetectionService : IFraudDetectionService
    {
        private readonly IClaimRepository _claimRepo;

        private static readonly string[] SuspiciousKeywords = { "duplicate", "specimen", "copy", "void", "sample", "test", "forged" };
        private static readonly string[] BlacklistedHospitals = { "Naughty Medical Center", "Scam Healthcare", "Fraud Clinic" };

        public FraudDetectionService(IClaimRepository claimRepo)
        {
            _claimRepo = claimRepo;
        }

        public async Task<FraudResult> AnalyzeClaimAsync(EGI_Backend.Domain.Entities.Claim claim, OCRExtractedData ocrData)
        {
            int score = 0;
            var reasons = new List<string>();

            // 0. OCR Quality Enforcement
            if (!ocrData.IsSuccess)
            {
                // Health claims require readable bills; general failure gets +35
                int penalty = (claim.ClaimType == CoverageType.Health) ? 35 : 15;
                score += penalty;
                reasons.Add("OCR Processing Failed: AI cannot verify document authenticity. High risk of obscured details.");
            }

            // 1. Amount Mismatch Check
            if (ocrData.BillAmount.HasValue && Math.Abs(claim.ClaimAmount - ocrData.BillAmount.Value) > 0.01m)
            {
                score += 40;
                reasons.Add($"Amount Mismatch: Requested ₹{claim.ClaimAmount:N2}, but OCR strictly found ₹{ocrData.BillAmount.Value:N2} on document.");
            }
            else if (!ocrData.BillAmount.HasValue && claim.ClaimType == CoverageType.Health)
            {
                // If it's a health bill but amount is missing, it's a major red flag
                score += 25;
                reasons.Add("Missing Financial Data: Document recognized as bill, but total amount is obscured or unreadable.");
            }

            // 2. Incident Date Mismatch Check
            if (claim.ClaimType == CoverageType.Health && ocrData.BillDate.HasValue)
            {
                if (Math.Abs((claim.IncidentDate.Date - ocrData.BillDate.Value.Date).TotalDays) > 2)
                {
                    score += 25;
                    reasons.Add($"Date Mismatch: Submission incident date ({claim.IncidentDate:yyyy-MM-dd}) differs significantly from Bill date ({ocrData.BillDate.Value:yyyy-MM-dd}).");
                }
            }

            // 3. Keyword Check (Digital Manipulation)
            if (!string.IsNullOrEmpty(ocrData.RawText))
            {
                foreach (var word in SuspiciousKeywords)
                {
                    if (ocrData.RawText.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 30;
                        reasons.Add($"Suspicious Keyword Found: The document contains the restricted term '{word}'.");
                    }
                }
            }

            // 4. Blacklist Check (with Normalization)
            string? provider = ocrData.HospitalName?.Trim();
            if (!string.IsNullOrEmpty(provider))
            {
                if (BlacklistedHospitals.Any(h => provider.Contains(h, StringComparison.OrdinalIgnoreCase)))
                {
                    score += 50;
                    reasons.Add($"Provider Blacklisted: '{provider}' is associated with known fraudulent activity.");
                }
            }

            // 5. Historical Duplicate Check (The strongest signal)
            if (ocrData.BillAmount.HasValue && ocrData.BillDate.HasValue && !string.IsNullOrEmpty(provider))
            {
                bool isDuplicate = await _claimRepo.IsDuplicateBillAsync(provider, ocrData.BillAmount.Value, ocrData.BillDate.Value, claim.Id);
                if (isDuplicate)
                {
                    score += 90;
                    reasons.Add("Duplicate Bill Detection: This exact bill (Provider, Amount, Date) has already been submitted in another claim.");
                }
            }

            // Final Result
            return new FraudResult
            {
                Score = Math.Min(score, 100),
                Analysis = reasons.Any() ? string.Join(" | ", reasons) : "No obvious fraud indicators detected.",
                IsFlagged = score >= 40
            };
        }
    }
}
