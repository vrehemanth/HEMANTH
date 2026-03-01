using System;

namespace EGI_Backend.Application.DTOs
{
    public class MemberResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public decimal SumInsured { get; set; }
        public bool Status { get; set; }
        public string? PolicyNo { get; set; }
    }
}
