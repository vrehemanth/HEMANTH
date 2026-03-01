using System;
using System.Collections.Generic;
using EGI_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using System.Linq;

namespace EGI_Backend.Infrastructure.Persistence
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string? UserId { get; set; }
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public Dictionary<string, object?> OldValues { get; } = new();
        public Dictionary<string, object?> NewValues { get; } = new();
        public List<string> ChangedColumns { get; } = new();

        public AuditLog ToAuditLog()
        {
            return new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                Action = Entry.State.ToString(),
                EntityName = EntityName ?? Entry.Entity.GetType().Name,
                EntityId = EntityId,
                Timestamp = DateTime.UtcNow,
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
            };
        }
    }
}
