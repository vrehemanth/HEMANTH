using EGI_Backend.Application.Interfaces;
using EGI_Backend.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EGIDbContext _context;

        public UnitOfWork(EGIDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
