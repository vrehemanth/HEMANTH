using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PolicyAssignmentId { get; set; }
        public PolicyAssignment PolicyAssignment { get; set; } = null!;

        [Required]
        public string InvoiceNo { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public DateTime BillingPeriodFrom { get; set; }

        public DateTime BillingPeriodTo { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
