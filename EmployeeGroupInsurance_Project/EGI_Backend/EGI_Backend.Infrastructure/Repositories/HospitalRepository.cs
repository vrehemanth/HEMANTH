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
    public class HospitalRepository : IHospitalRepository
    {
        private readonly EGIDbContext _context;

        public HospitalRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<List<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals
                .OrderBy(h => h.Name)
                .ToListAsync();
        }

        public async Task<Hospital?> GetByIdAsync(Guid id)
        {
            return await _context.Hospitals.FindAsync(id);
        }

        public async Task<List<Hospital>> GetNetworkHospitalsAsync()
        {
            return await _context.Hospitals
                .Where(h => h.IsNetworkHospital && h.IsActive)
                .OrderBy(h => h.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
        }

        public async Task UpdateAsync(Hospital hospital)
        {
            _context.Hospitals.Update(hospital);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Hospitals.AnyAsync(h => h.Id == id);
        }
    }
}
