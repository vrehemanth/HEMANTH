using EGI_Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log);
        Task<List<AuditLog>> GetAllAsync();
        Task<List<AuditLog>> GetFilteredAsync(string? userId, string? entityName);
    }
}
