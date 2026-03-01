using System;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        private readonly EGIDbContext _context;

        public DependentRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<Dependent?> GetByIdAsync(Guid id)
        {
            return await _context.Dependents.FindAsync(id);
        }

        public async Task AddAsync(Dependent dependent)
        {
            await _context.Dependents.AddAsync(dependent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Dependent dependent)
        {
            _context.Dependents.Update(dependent);
            await _context.SaveChangesAsync();
        }
    }
}
