using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<List<Invoice>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId);

        // Gets all active policies that have a billing day matching today
        Task<List<PolicyAssignment>> GetActivePoliciesDueForInvoiceAsync(int billingDay);

        // Latest invoice end date for a policy (to calculate next billing period)
        Task<Invoice?> GetLatestInvoiceForPolicyAsync(Guid policyAssignmentId);

        // Pending invoices past their due date (for marking Overdue)
        Task<List<Invoice>> GetOverduePendingInvoicesAsync();
        
        Task<decimal> GetTotalRevenueAsync();
        Task<int> CountUnpaidByClientAsync(Guid clientId);
        Task<decimal> GetTotalBalanceByClientAsync(Guid clientId);
        Task<List<Invoice>> GetByClientIdAsync(Guid clientId);
    }
}
