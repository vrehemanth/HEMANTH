using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class PolicyEndorsementRepository : IPolicyEndorsementRepository
    {
        private readonly EGIDbContext _context;

        public PolicyEndorsementRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PolicyEndorsement endorsement)
        {
            await _context.PolicyEndorsements.AddAsync(endorsement);
        }

        public async Task<PolicyEndorsement?> GetByIdAsync(Guid id)
        {
            return await _context.PolicyEndorsements
                .Include(pe => pe.PolicyAssignment)
                .ThenInclude(pa => pa.InsurancePlan)
                .FirstOrDefaultAsync(pe => pe.Id == id);
        }

        public async Task UpdateAsync(PolicyEndorsement endorsement)
        {
            _context.PolicyEndorsements.Update(endorsement);
            // SaveChangesAsync is intentionally omitted — IUnitOfWork owns the transaction boundary
        }

        public async Task<List<PolicyEndorsement>> GetByPolicyIdAsync(Guid policyAssignmentId)
        {
            return await _context.PolicyEndorsements
                .Where(pe => pe.PolicyAssignmentId == policyAssignmentId)
                .OrderByDescending(pe => pe.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<PolicyEndorsement>> GetPendingAsync()
        {
            return await _context.PolicyEndorsements
                .Include(pe => pe.PolicyAssignment)
                .Where(pe => pe.Status == EndorsementStatus.Pending)
                .OrderBy(pe => pe.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<PolicyEndorsement>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.PolicyEndorsements
                .AsNoTracking()
                .Include(pe => pe.PolicyAssignment)
                .Where(pe => pe.PolicyAssignment.CorporateClientId == clientId)
                .OrderByDescending(pe => pe.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<PolicyEndorsement>> GetAllAsync()
        {
            return await _context.PolicyEndorsements
                .Include(pe => pe.PolicyAssignment)
                .OrderByDescending(pe => pe.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> IsDuplicateAsync(string submissionToken)
        {
            if (string.IsNullOrEmpty(submissionToken)) return false;
            return await _context.PolicyEndorsements.AnyAsync(pe => pe.SubmissionToken == submissionToken);
        }
    }
}
