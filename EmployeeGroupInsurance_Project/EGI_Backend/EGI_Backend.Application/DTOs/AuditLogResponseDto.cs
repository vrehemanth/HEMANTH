using System;

namespace EGI_Backend.Application.DTOs
{
    public class AuditLogResponseDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string? EntityId { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
