using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly EGIDbContext _context;

        public AuditLogRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog log)
        {
            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetFilteredAsync(string? userId, string? entityName)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(entityName))
                query = query.Where(x => x.EntityName == entityName);

            return await query
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }
        public async Task<int> CountRecentLogsAsync(System.DateTime since)
        {
            return await _context.AuditLogs.CountAsync(x => x.Timestamp >= since);
        }

        public async Task<int> CountSignificantEventsAsync(System.DateTime since)
        {
            // Significant = High level business actions (Plan Creation, Client Approval, Claim, Payment, Login)
            // We EXCLUDE (Member, Dependent, Document, Notification, AuditLog entries)
            var ignoredEntities = new[] { "Member", "Dependent", "ClaimDocument", "Notification", "AuditLog", "AuditEntry" };
            
            return await _context.AuditLogs
                .Where(x => x.Timestamp >= since && !ignoredEntities.Contains(x.EntityName))
                .CountAsync();
        }

        public async Task<List<AuditLog>> GetTopRecentLogsAsync(int count)
        {
            return await _context.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}
