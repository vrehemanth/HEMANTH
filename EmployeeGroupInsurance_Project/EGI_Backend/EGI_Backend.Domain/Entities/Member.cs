using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PolicyAssignmentId { get; set; }
        public PolicyAssignment PolicyAssignment { get; set; }

        [Required]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNo { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SumInsured { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
    }
}
