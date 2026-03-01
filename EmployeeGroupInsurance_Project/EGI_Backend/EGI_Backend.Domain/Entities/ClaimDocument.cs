using System;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class ClaimDocument
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClaimId { get; set; }
        public Claim Claim { get; set; }

        public ClaimDocumentType DocumentType { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
