using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.DTOs
{
    public class CorporateClientResponseDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public bool IsBlocked { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int IndustryType { get; set; }
        public string? KybAiAnalysis { get; set; }
        public int KybAiConfidenceScore { get; set; }
        public List<CorporateClientDocumentDto> Documents { get; set; } = new();

        public DateTime? LastHealthCheckupDate { get; set; }
        public Guid? HealthCheckupHospitalId { get; set; }
        public string? HealthCheckupHospitalName { get; set; }
        public int HealthCheckupActualMemberCount { get; set; }
        public int HealthCheckupActualDependentCount { get; set; }
        public bool IsHealthCheckupClaimPending { get; set; }
        public DateTime? HealthCheckupVerifiedAt { get; set; }
    }
}
