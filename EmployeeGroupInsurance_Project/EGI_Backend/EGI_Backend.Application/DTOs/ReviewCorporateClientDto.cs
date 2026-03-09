using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class ReviewCorporateClientDto
    {
        public bool IsApproved { get; set; }
        public string RejectionReason { get; set; } = string.Empty;
    }
}
