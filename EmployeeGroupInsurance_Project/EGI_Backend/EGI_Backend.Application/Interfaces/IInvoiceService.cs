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
        Task GenerateAdjustmentInvoiceAsync(PolicyAssignment policy, decimal adjustmentAmount);

        // Called by background job daily
        Task GenerateDueInvoicesAsync();
        Task MarkOverdueInvoicesAsync();

        // Customer pays an invoice
        Task<PaymentResponseDto> PayInvoiceAsync(Guid invoiceId, Guid customerUserId, PayInvoiceDto dto);

        // Queries
        Task<List<InvoiceResponseDto>> GetInvoicesByPolicyAsync(Guid policyAssignmentId);
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid invoiceId);
        Task<List<PaymentResponseDto>> GetPaymentsByInvoiceAsync(Guid invoiceId);
    }
}
