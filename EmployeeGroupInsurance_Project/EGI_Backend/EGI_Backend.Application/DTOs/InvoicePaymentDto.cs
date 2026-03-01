using System;
using System.Collections.Generic;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class InvoiceResponseDto
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string PolicyNo { get; set; } = string.Empty;
        public Guid PolicyAssignmentId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime BillingPeriodFrom { get; set; }
        public DateTime BillingPeriodTo { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PaymentResponseDto
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string? TransactionId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PayInvoiceDto
    {
        public string? TransactionId { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
