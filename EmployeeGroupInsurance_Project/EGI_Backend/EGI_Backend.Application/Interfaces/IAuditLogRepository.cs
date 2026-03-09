using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log);
        Task<List<AuditLog>> GetAllAsync();
        Task<List<AuditLog>> GetFilteredAsync(string? userId, string? entityName);
        Task<int> CountRecentLogsAsync(DateTime since);
        Task<int> CountSignificantEventsAsync(DateTime since);
        Task<List<AuditLog>> GetTopRecentLogsAsync(int count);
    }
}
