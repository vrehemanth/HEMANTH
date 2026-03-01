using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class InsurancePlanRepository : IInsurancePlanRepository
    {
        private readonly EGIDbContext _context;

        public InsurancePlanRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsurancePlan>> GetAllAsync()
        {
            return await _context.InsurancePlans.Include(p => p.Coverages).ToListAsync();
        }

        public async Task<InsurancePlan?> GetByIdAsync(Guid id)
        {
            return await _context.InsurancePlans.Include(p => p.Coverages).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<InsurancePlan?> GetByPlanCodeAsync(string planCode)
        {
            return await _context.InsurancePlans.Include(p => p.Coverages).FirstOrDefaultAsync(p => p.PlanCode == planCode);
        }

        public async Task AddAsync(InsurancePlan plan)
        {
            await _context.InsurancePlans.AddAsync(plan);
        }

        public void Update(InsurancePlan plan)
        {
            _context.InsurancePlans.Update(plan);
        }

        public void Delete(InsurancePlan plan)
        {
            _context.InsurancePlans.Remove(plan);
        }
    }
}
