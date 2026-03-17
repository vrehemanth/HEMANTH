namespace EGI_Backend.Application.Interfaces
{
    using EGI_Backend.Domain.Entities;
    using System.Threading.Tasks;
    public interface IFraudDetectionService
    {
        /// <summary>
        /// Analyzes a claim using OCR data and historical patterns to detect fraud.
        /// </summary>
        Task<FraudResult> AnalyzeClaimAsync(Claim claim, OCRExtractedData ocrData);
    }

    public class FraudResult
    {
        public int Score { get; set; }
        public string Analysis { get; set; } = string.Empty;
        public bool IsFlagged { get; set; }
    }
}
