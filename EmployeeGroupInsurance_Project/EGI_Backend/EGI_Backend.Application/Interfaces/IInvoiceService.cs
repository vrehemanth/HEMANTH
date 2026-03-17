using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IInvoiceService
    {
        // Called at policy creation to generate first invoice
        Task GenerateFirstInvoiceAsync(PolicyAssignment policy);

        // Called for policy endorsements to bill the extra prorated amount
        Task GenerateAdjustmentInvoiceAsync(PolicyAssignment policy, decimal adjustmentAmount, DateTime? customPeriodTo = null);

        // Called for removals to subtract credit from the next available invoice
        Task ApplyCreditToNextInvoiceAsync(Guid policyAssignmentId, decimal creditAmount);

        // Called by background job daily
        Task GenerateDueInvoicesAsync();
        Task MarkOverdueInvoicesAsync();
        Task MarkOverdueInvoicesForClientAsync(Guid clientId);

        // Customer pays an invoice
        Task<PaymentResponseDto> PayInvoiceAsync(Guid invoiceId, Guid customerUserId, PayInvoiceDto dto);

        // Queries (Security Hardened)
        Task<List<InvoiceResponseDto>> GetInvoicesByPolicyAsync(Guid policyAssignmentId, Guid callerUserId, string role);
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid invoiceId, Guid callerUserId, string role);
        Task<List<PaymentResponseDto>> GetPaymentsByInvoiceAsync(Guid invoiceId, Guid callerUserId, string role);
    }
}
