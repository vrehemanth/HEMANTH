using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using EGI_Backend.Domain.Enums;
using System.Globalization;

namespace EGI_Backend.Infrastructure.Services
{
    public class OCRService : IOCRService
    {
        public async Task<OCRExtractedData> ExtractDataAsync(IFormFile file, CoverageType claimType)
        {
            try
            {
                string rawText = "";

                if (file.ContentType == "application/pdf")
                {
                    rawText = ExtractTextFromPdf(file);
                }
                else
                {
                    return new OCRExtractedData
                    {
                        IsSuccess = false,
                        RawText = $"Digital OCR is optimized for PDF documents (uploaded: {file.FileName})."
                    };
                }

                return ParseDataFromText(rawText, claimType);
            }
            catch (Exception ex)
            {
                return new OCRExtractedData
                {
                    IsSuccess = false,
                    RawText = $"Extraction failed: {ex.Message}"
                };
            }
        }

        private string ExtractTextFromPdf(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (var document = PdfDocument.Open(stream))
            {
                var text = "";
                foreach (var page in document.GetPages())
                {
                    text += page.Text + " ";
                }
                return text;
            }
        }

        private OCRExtractedData ParseDataFromText(string text, CoverageType claimType)
        {
            var result = new OCRExtractedData
            {
                IsSuccess = true,
                RawText = text
            };

            switch (claimType)
            {
                case CoverageType.Health:
                    ExtractHealthData(text, result);
                    break;
                case CoverageType.Life:
                    ExtractLifeData(text, result);
                    break;
                case CoverageType.Accident:
                    ExtractAccidentData(text, result);
                    break;
                case CoverageType.CriticalIllness:
                    ExtractCriticalIllnessData(text, result);
                    break;
            }

            return result;
        }

        private void ExtractHealthData(string text, OCRExtractedData result)
        {
            // 1. Hospital Name
            var hospitalMatch = Regex.Match(text, @"(?i)([\w\s]{2,50}?(?:Hospital|Clinic|Medical Center|Healthcare))");
            result.HospitalName = hospitalMatch.Success ? hospitalMatch.Value.Trim() : "Unknown Provider";

            // 2. Bill Amount
            var amountMatch = Regex.Match(text, @"(?i)(?:Total|Amount|Balance|Payable)[:\s]*₹?\s*(\d{1,7}(?:[.,]\d{2})?)");
            if (amountMatch.Success && decimal.TryParse(amountMatch.Groups[1].Value.Replace(",", ""), out decimal amount))
            {
                result.BillAmount = amount;
            }

            // 3. Bill Date
            var dateMatch = Regex.Match(text, @"(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})");
            if (dateMatch.Success && TryParseFlexibleDate(dateMatch.Value, out DateTime date))
            {
                result.BillDate = date;
            }

            result.IsSuccess = result.BillAmount.HasValue || result.HospitalName != "Unknown Provider";
        }

        private void ExtractLifeData(string text, OCRExtractedData result)
        {
            // 1. Date of Death - Enhanced with multiple patterns and flexible parsing
            var dodPatterns = new[] {
                @"(?i)(?:Date of Death|DOD)[:\s]*(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})",
                @"(?i)Death\s+Date[:\s]*(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})",
                @"(?i)D\.O\.D[:\s]*(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})"
            };

            foreach (var pattern in dodPatterns)
            {
                var dodMatch = Regex.Match(text, pattern);
                if (dodMatch.Success && TryParseFlexibleDate(dodMatch.Groups[1].Value, out DateTime dod))
                {
                    result.DateOfDeath = dod;
                    break;
                }
            }

            // Fallback: If no labeled date, just grab the first valid date in policy range
            if (!result.DateOfDeath.HasValue)
            {
                var allDates = Regex.Matches(text, @"(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})");
                foreach (Match m in allDates)
                {
                    if (TryParseFlexibleDate(m.Value, out DateTime dod))
                    {
                        // Assume the first valid looking date is the one if labeled fails
                        result.DateOfDeath = dod;
                        break;
                    }
                }
            }

            // 2. Cause of Death
            var causeMatch = Regex.Match(text, @"(?i)(?:Cause of Death|COD)[:\s]*([\w\s]{3,100})");
            if (causeMatch.Success) result.CauseOfDeath = causeMatch.Groups[1].Value.Trim();

            // 3. Place of Death
            var placeMatch = Regex.Match(text, @"(?i)(?:Place of Death)[:\s]*([\w\s]{3,100})");
            if (placeMatch.Success) result.PlaceOfDeath = placeMatch.Groups[1].Value.Trim();

            result.IsSuccess = result.DateOfDeath.HasValue || !string.IsNullOrEmpty(result.CauseOfDeath);
        }

        private void ExtractAccidentData(string text, OCRExtractedData result)
        {
            // 1. FIR Number
            var firMatch = Regex.Match(text, @"(?i)(?:FIR No|Crime No)[:\s]*([\w\d/]{3,20})");
            if (firMatch.Success) result.FirNumber = firMatch.Groups[1].Value.Trim();

            // 2. Police Station
            var psMatch = Regex.Match(text, @"(?i)(?:Police Station|P\.S\.)[:\s]*([\w\s]{3,50})");
            if (psMatch.Success) result.PoliceStation = psMatch.Groups[1].Value.Trim();

            // 3. Incident Date
            var dateMatch = Regex.Match(text, @"(?i)(?:Date of Incident|DOI)[:\s]*(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})");
            if (dateMatch.Success && TryParseFlexibleDate(dateMatch.Groups[1].Value, out DateTime doi))
            {
                result.IncidentDate = doi;
            }

            result.IsSuccess = !string.IsNullOrEmpty(result.FirNumber) || !string.IsNullOrEmpty(result.PoliceStation);
        }

        private void ExtractCriticalIllnessData(string text, OCRExtractedData result)
        {
            // 1. Diagnosis
            var diagMatch = Regex.Match(text, @"(?i)(?:Diagnosis|Nature of Illness)[:\s]*([\w\s]{3,200})");
            if (diagMatch.Success) result.Diagnosis = diagMatch.Groups[1].Value.Trim();

            result.IsSuccess = !string.IsNullOrEmpty(result.Diagnosis);
        }

        private bool TryParseFlexibleDate(string dateStr, out DateTime result)
        {
            // Specifically include dd/MM/yyyy for Indian certificates
            string[] formats = { 
                "dd/MM/yyyy", "MM/dd/yyyy", "dd-MM-yyyy", "MM-dd-yyyy", 
                "yyyy-MM-dd", "d/M/yyyy", "M/d/yyyy", "d-M-yyyy" 
            };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return true;

            return DateTime.TryParse(dateStr, out result);
        }
    }
}
