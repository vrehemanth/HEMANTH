using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class UploadMembersDto
    {
        [Required]
        public Guid CorporateClientId { get; set; }

        [Required]
        public Guid InsurancePlanId { get; set; }

        [Required]
        public BillingFrequency BillingFrequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, 10)]
        public int DurationInYears { get; set; }

        [Required]
        public IFormFile MembersFile { get; set; }

        public IFormFile? DependentsFile { get; set; }
    }
}
