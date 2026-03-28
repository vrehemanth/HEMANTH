using System;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Domain.Entities
{
    public class ClinicalDispatch
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public Guid? DependentId { get; set; }
        public Dependent? Dependent { get; set; }

        public string PatientName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string PolicyNo { get; set; } = string.Empty;

        public string CoverageSummaryJson { get; set; } = string.Empty;

        public DateTime DispatchDate { get; set; } = DateTime.UtcNow;
        
        // Status: Intransit, Admitted, Discharged, Cancelled
        public string Status { get; set; } = "Intransit";
        
        public bool IsClosed { get; set; } = false;
    }
}
