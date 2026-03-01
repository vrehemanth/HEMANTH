using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EGI_Backend.Application.DTOs
{
    public class SubmitClaimDto
    {
        [Required]
        public Guid PolicyAssignmentId { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        // Leave null if this is the member's own claim
        public Guid? DependentId { get; set; }

        [Required]
        public CoverageType ClaimType { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Claim amount must be greater than 0.")]
        public decimal ClaimAmount { get; set; }

        [Required]
        public string ClaimReason { get; set; } = string.Empty;

        // Supporting documents: each file maps to a document type by index
        [Required]
        public List<IFormFile> Documents { get; set; } = new();

        [Required]
        public List<ClaimDocumentType> DocumentTypes { get; set; } = new();
    }
}
