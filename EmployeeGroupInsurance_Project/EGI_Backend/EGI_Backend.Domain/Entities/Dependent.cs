using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class Dependent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        public RelationshipType Relationship { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SumInsured { get; set; }

        // Soft-delete flag — false means this dependent was removed via endorsement
        public bool IsActive { get; set; } = true;
    }
}
