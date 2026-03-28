using System;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Application.DTOs
{
    public class HospitalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Specialties { get; set; }
        public bool IsNetworkHospital { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUpdateHospitalDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; } = string.Empty;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Specialties { get; set; }

        public bool IsNetworkHospital { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }
}
