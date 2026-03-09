using EGI_Backend.Application.Interfaces;
using System;

namespace EGI_Backend.Application.DTOs
{
    public class DependentResponseDto
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public decimal SumInsured { get; set; }
        public bool IsActive { get; set; }
    }
}
