using System;
using System.ComponentModel.DataAnnotations;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        // Customer who made the payment
        public Guid PaidBy { get; set; }
        public User PaidByUser { get; set; } = null!;

        /// <summary>Reference from payment gateway / manual ref (e.g. UPI txn ID)</summary>
        public string? TransactionId { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
