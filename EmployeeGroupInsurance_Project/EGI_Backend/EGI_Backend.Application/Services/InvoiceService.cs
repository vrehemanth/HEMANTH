using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const int DueDaysGrace = 7; // Invoice due 7 days after generation

        public InvoiceService(
            IInvoiceRepository invoiceRepo,
            IPaymentRepository paymentRepo,
            ICorporateClientRepository clientRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _paymentRepo = paymentRepo;
            _clientRepo = clientRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ─── Helpers ─────────────────────────────────────────────

        private static decimal CalculateInvoiceAmount(PolicyAssignment policy)
        {
            return policy.BillingFrequency == BillingFrequency.Monthly
                ? Math.Round(policy.AnnualPremium / 12, 2)
                : policy.AnnualPremium;
        }

        private static string GenerateInvoiceNo()
        {
            return $"INV-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        private static DateTime AddOneBillingCycle(DateTime date, BillingFrequency freq)
        {
            return freq == BillingFrequency.Monthly
                ? date.AddMonths(1)
                : date.AddYears(1);
        }

        // ─── Core Invoice Generation ──────────────────────────────

        public async Task GenerateFirstInvoiceAsync(PolicyAssignment policy)
        {
            var periodFrom = policy.StartDate.Date;
            var periodTo = AddOneBillingCycle(periodFrom, policy.BillingFrequency).AddDays(-1);

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                PolicyAssignmentId = policy.Id,
                InvoiceNo = GenerateInvoiceNo(),
                InvoiceDate = DateTime.UtcNow,
                BillingPeriodFrom = periodFrom,
                BillingPeriodTo = periodTo > policy.EndDate.Date ? policy.EndDate.Date : periodTo,
                Amount = CalculateInvoiceAmount(policy),
                DueDate = DateTime.UtcNow.AddDays(DueDaysGrace),
                Status = InvoiceStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _invoiceRepo.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task GenerateAdjustmentInvoiceAsync(PolicyAssignment policy, decimal adjustmentAmount)
        {
            // Only generate if there is a positive adjustment
            if (adjustmentAmount <= 0) return;

            // An adjustment invoice covers from "Today" until the end of the CURRENT billing period.
            // For simplicity, we align it to the end of the policy's first year if it's an annual plan.
            var periodFrom = DateTime.UtcNow.Date;
            var periodTo = policy.EndDate.Date; // Until end of the policy year

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                PolicyAssignmentId = policy.Id,
                InvoiceNo = GenerateInvoiceNo() + "-ADJ", // Suffix to mark as adjustment
                InvoiceDate = DateTime.UtcNow,
                BillingPeriodFrom = periodFrom,
                BillingPeriodTo = periodTo,
                Amount = adjustmentAmount, // ONLY the extra amount, not the new total
                DueDate = DateTime.UtcNow.AddDays(DueDaysGrace),
                Status = InvoiceStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _invoiceRepo.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task GenerateDueInvoicesAsync()
        {
            int today = DateTime.UtcNow.Day;
            var policies = await _invoiceRepo.GetActivePoliciesDueForInvoiceAsync(today);

            foreach (var policy in policies)
            {
                // Get the latest invoice so we can continue from where it left off
                var latest = await _invoiceRepo.GetLatestInvoiceForPolicyAsync(policy.Id);
                if (latest == null) continue; // First invoice handled at creation

                var nextPeriodFrom = latest.BillingPeriodTo.Date.AddDays(1);
                var nextPeriodTo = AddOneBillingCycle(nextPeriodFrom, policy.BillingFrequency).AddDays(-1);

                // REAL WORLD GUARD: Only generate if the next period starts within the next 30 days.
                // We don't want to generate invoices for Year 2030 while we are in 2026!
                if (nextPeriodFrom > DateTime.UtcNow.AddDays(30))
                {
                    continue; 
                }

                // Don't generate beyond the policy EndDate or if the next period hasn't started yet
                if (nextPeriodFrom > policy.EndDate.Date) continue;
                
                // CRITICAL: Check if we have already generated an invoice for this year to avoid duplicates on app restarts
                var alreadyExists = await _invoiceRepo.GetByPolicyAssignmentIdAsync(policy.Id);
                if (alreadyExists.Any(i => i.BillingPeriodFrom.Date == nextPeriodFrom.Date && !i.InvoiceNo.EndsWith("-ADJ")))
                {
                    continue;
                }

                var invoice = new Invoice
                {
                    Id = Guid.NewGuid(),
                    PolicyAssignmentId = policy.Id,
                    InvoiceNo = GenerateInvoiceNo(),
                    InvoiceDate = DateTime.UtcNow,
                    BillingPeriodFrom = nextPeriodFrom,
                    BillingPeriodTo = nextPeriodTo > policy.EndDate.Date ? policy.EndDate.Date : nextPeriodTo,
                    Amount = CalculateInvoiceAmount(policy),
                    DueDate = DateTime.UtcNow.AddDays(DueDaysGrace),
                    Status = InvoiceStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                await _invoiceRepo.AddAsync(invoice);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarkOverdueInvoicesAsync()
        {
            var overdueList = await _invoiceRepo.GetOverduePendingInvoicesAsync();
            foreach (var inv in overdueList)
                inv.Status = InvoiceStatus.Overdue;

            await _unitOfWork.SaveChangesAsync();
        }

        // ─── Payment ──────────────────────────────────────────────

        public async Task<PaymentResponseDto> PayInvoiceAsync(Guid invoiceId, Guid customerUserId, PayInvoiceDto dto)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new NotFoundException("Invoice not found.");

            if (invoice.Status == InvoiceStatus.Paid)
                throw new BadRequestException("Invoice is already paid.");

            if (dto.PaidAmount < invoice.Amount)
                throw new BadRequestException($"Paid amount (₹{dto.PaidAmount}) is less than the invoice amount (₹{invoice.Amount}).");

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                PaidBy = customerUserId,
                TransactionId = dto.TransactionId,
                PaidAmount = dto.PaidAmount,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                Status = PaymentStatus.Success,
                CreatedAt = DateTime.UtcNow
            };

            invoice.Status = InvoiceStatus.Paid;

            await _paymentRepo.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            // Reload for correct mapping of navigation properties
            var paymentResult = await _paymentRepo.GetByIdAsync(payment.Id);
            return _mapper.Map<PaymentResponseDto>(paymentResult);
        }

        // ─── Queries ──────────────────────────────────────────────

        public async Task<List<InvoiceResponseDto>> GetInvoicesByPolicyAsync(Guid policyAssignmentId)
        {
            var invoices = await _invoiceRepo.GetByPolicyAssignmentIdAsync(policyAssignmentId);
            return _mapper.Map<List<InvoiceResponseDto>>(invoices);
        }

        public async Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid invoiceId)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(invoiceId);
            if (invoice == null) throw new NotFoundException("Invoice not found.");
            return _mapper.Map<InvoiceResponseDto>(invoice);
        }

        public async Task<List<PaymentResponseDto>> GetPaymentsByInvoiceAsync(Guid invoiceId)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(invoiceId);
            if (invoice == null) throw new NotFoundException("Invoice not found.");

            var payments = await _paymentRepo.GetByInvoiceIdAsync(invoiceId);
            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }
    }
}
