using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly EGIDbContext _context;

        public InvoiceRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices
                .Include(i => i.PolicyAssignment)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId)
        {
            return await _context.Invoices
                .Where(i => i.PolicyAssignmentId == policyAssignmentId)
                .OrderByDescending(i => i.BillingPeriodFrom)
                .ToListAsync();
        }

        public async Task<List<PolicyAssignment>> GetActivePoliciesDueForInvoiceAsync(int billingDay)
        {
            return await _context.PolicyAssignments
                .Include(pa => pa.InsurancePlan)
                .Where(pa =>
                    pa.Status == PolicyStatus.Active &&
                    pa.StartDate.Day == billingDay)
                .ToListAsync();
        }

        public async Task<Invoice?> GetLatestInvoiceForPolicyAsync(Guid policyAssignmentId)
        {
            return await _context.Invoices
                .Where(i => i.PolicyAssignmentId == policyAssignmentId)
                .OrderByDescending(i => i.BillingPeriodTo)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Invoice>> GetOverduePendingInvoicesAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Invoices
                .Where(i => (i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.PartiallyPaid) && i.DueDate.Date < today)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Invoices
                .SumAsync(i => i.TotalPaid);
        }

        public async Task<int> CountUnpaidByClientAsync(Guid clientId)
        {
            return await _context.Invoices
                .Include(i => i.PolicyAssignment)
                .CountAsync(i => i.PolicyAssignment.CorporateClientId == clientId && i.Status != InvoiceStatus.Paid);
        }

        public async Task<decimal> GetTotalBalanceByClientAsync(Guid clientId)
        {
            return await _context.Invoices
                .Include(i => i.PolicyAssignment)
                .Where(i => i.PolicyAssignment.CorporateClientId == clientId && i.Status != InvoiceStatus.Paid)
                .SumAsync(i => i.Amount - i.TotalPaid);
        }

        public async Task<List<Invoice>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Invoices
                .Include(i => i.PolicyAssignment)
                .Where(i => i.PolicyAssignment.CorporateClientId == clientId)
                .OrderByDescending(i => i.BillingPeriodFrom)
                .ToListAsync();
        }
        public async Task<Dictionary<Guid, decimal>> GetBalancesByClientsAsync(List<Guid> clientIds)
        {
            return await _context.Invoices
                .Include(i => i.PolicyAssignment)
                .Where(i => i.PolicyAssignment != null && clientIds.Contains(i.PolicyAssignment.CorporateClientId) && i.Status != InvoiceStatus.Paid)
                .GroupBy(i => i.PolicyAssignment.CorporateClientId)
                .Select(g => new { ClientId = g.Key, Balance = g.Sum(x => x.Amount - x.TotalPaid) })
                .ToDictionaryAsync(x => x.ClientId, x => x.Balance);
        }
    }
}
