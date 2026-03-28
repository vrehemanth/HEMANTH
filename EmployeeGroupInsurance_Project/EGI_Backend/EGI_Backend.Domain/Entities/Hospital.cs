using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Domain.Entities
{
    public class Hospital
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public string? Email { get; set; }

        public string? Specialties { get; set; } // Comma-separated or JSON list

        public bool IsNetworkHospital { get; set; } = true; // For cashless path

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
