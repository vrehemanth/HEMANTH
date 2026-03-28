using System;

namespace EGI_Backend.Application.DTOs
{
    public class ClinicalDispatchDto
    {
        public Guid Id { get; set; }
        public string HospitalName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string PolicyNo { get; set; } = string.Empty;
        public string CoverageSummary { get; set; } = string.Empty;
        public DateTime DispatchDate { get; set; }
        public string Status { get; set; } = string.Empty;
        
        // Links for auto-filling partnership claim
        public Guid HospitalId { get; set; }
        public Guid MemberId { get; set; }
        public Guid? DependentId { get; set; }
    }
}
