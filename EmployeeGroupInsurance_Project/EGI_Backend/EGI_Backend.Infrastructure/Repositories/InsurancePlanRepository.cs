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

        public async Task<List<InsurancePlan>> GetActivePlansAsync()
        {
            return await _context.InsurancePlans
                .AsNoTracking()
                .Include(p => p.Coverages)
                .Where(p => p.Status)
                .ToListAsync();
        }

        public async Task<bool> IsPlanInUseAsync(Guid planId)
        {
            return await _context.PolicyAssignments.AnyAsync(pa => pa.InsurancePlanId == planId);
        }

        public async Task<List<(Guid PlanId, string PlanName, int MemberCount)>> GetPlanEnrollmentStatsAsync()
        {
            return await _context.InsurancePlans
                .AsNoTracking()
                .Select(p => new
                {
                    p.Id,
                    p.PlanName,
                    Count = _context.Members.Count(m => m.PolicyAssignment.InsurancePlanId == p.Id)
                })
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => (x.Id, x.PlanName, x.Count)).ToList());
        }
    }
}
