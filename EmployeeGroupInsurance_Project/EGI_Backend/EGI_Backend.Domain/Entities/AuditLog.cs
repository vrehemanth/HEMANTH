using System;
using System.ComponentModel.DataAnnotations;

namespace EGI_Backend.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }

        public string? UserId { get; set; } // Storing as string to handle "System" or Guid strings

        [Required]
        public string Action { get; set; } = string.Empty; // Added, Modified, Deleted

        [Required]
        public string EntityName { get; set; } = string.Empty;

        public string? EntityId { get; set; }

        public string? OldValues { get; set; } // JSON string of old state

        public string? NewValues { get; set; } // JSON string of new state

        public string? AffectedColumns { get; set; } // List of columns that changed

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
