using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class CorporateDocumentRepository : ICorporateDocumentRepository
    {
        private readonly EGIDbContext _context;

        public CorporateDocumentRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CorporateClientDocument document)
        {
            await _context.CorporateClientDocuments.AddAsync(document);
        }
        
        public async Task<List<CorporateClientDocument>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.CorporateClientDocuments
                .Where(x => x.CorporateClientId == clientId)
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var doc = await _context.CorporateClientDocuments.FindAsync(id);
            if (doc != null)
            {
                _context.CorporateClientDocuments.Remove(doc);
            }
        }
    }
}
