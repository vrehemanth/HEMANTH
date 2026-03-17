using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Interfaces
{
    public interface IOCRService
    {
        Task<OCRExtractedData> ExtractDataAsync(IFormFile file, CoverageType claimType);
    }

    public class OCRExtractedData
    {
        // Shared
        public bool IsSuccess { get; set; }
        public string? RawText { get; set; }

        // Health / Medical
        public string? HospitalName { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BillAmount { get; set; }

        // Life / Mortality
        public DateTime? DateOfDeath { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? PlaceOfDeath { get; set; }

        // Accidental Trauma
        public string? FirNumber { get; set; }
        public string? PoliceStation { get; set; }
        public DateTime? IncidentDate { get; set; }

        // Critical Illness
        public string? Diagnosis { get; set; }
    }
}
