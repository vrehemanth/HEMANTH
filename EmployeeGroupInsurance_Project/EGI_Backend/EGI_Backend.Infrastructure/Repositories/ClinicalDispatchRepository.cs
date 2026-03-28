using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class ClinicalDispatchRepository : IClinicalDispatchRepository
    {
        private readonly EGIDbContext _context;

        public ClinicalDispatchRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClinicalDispatch dispatch)
        {
            await _context.ClinicalDispatches.AddAsync(dispatch);
        }

        public async Task<List<ClinicalDispatch>> GetAllActiveAsync()
        {
            return await _context.ClinicalDispatches
                .Include(d => d.Hospital)
                .Include(d => d.Member)
                .Where(d => !d.IsClosed)
                .OrderByDescending(d => d.DispatchDate)
                .ToListAsync();
        }

        public async Task<ClinicalDispatch?> GetByIdAsync(Guid id)
        {
            return await _context.ClinicalDispatches.FindAsync(id);
        }

        public async Task UpdateAsync(ClinicalDispatch dispatch)
        {
            _context.ClinicalDispatches.Update(dispatch);
            await Task.CompletedTask;
        }
    }
}
