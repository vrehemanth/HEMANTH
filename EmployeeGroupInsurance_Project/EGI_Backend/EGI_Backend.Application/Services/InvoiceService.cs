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
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IAuditLogRepository _auditLogRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;



        public InvoiceService(
            IInvoiceRepository invoiceRepo,
            IPaymentRepository paymentRepo,
            ICorporateClientRepository clientRepo,
            IPolicyAssignmentRepository policyRepo,
            IAuditLogRepository auditLogRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _paymentRepo = paymentRepo;
            _clientRepo = clientRepo;
            _policyRepo = policyRepo;
            _auditLogRepo = auditLogRepo;
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
                DueDate = DateTime.UtcNow.AddDays(EGI_Backend.Domain.Constants.BusinessRules.InvoiceDueGraceDays),
                Status = InvoiceStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _invoiceRepo.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ApplyCreditToNextInvoiceAsync(Guid policyAssignmentId, decimal creditAmount)
        {
            if (creditAmount <= 0) return;

            // Find all invoices that aren't paid yet
            var invoices = await _invoiceRepo.GetByPolicyAssignmentIdAsync(policyAssignmentId);
            var pendingInvoices = invoices
                .Where(i => i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.Overdue)
                .OrderBy(i => i.BillingPeriodFrom) // Apply to the SOONEST available invoices first (Waterfall)
                .ToList();

            if (!pendingInvoices.Any()) return;

            decimal remainingCredit = creditAmount;

            foreach (var targetInvoice in pendingInvoices)
            {
                if (remainingCredit <= 0) break;

                // Check if this invoice can swallow the whole credit or just part of it
                if (targetInvoice.Amount <= remainingCredit)
                {
                    // Credit is larger than or equal to this invoice
                    remainingCredit -= targetInvoice.Amount;
                    targetInvoice.Amount = 0;
                    targetInvoice.Status = InvoiceStatus.Paid;
                }
                else
                {
                    // Credit is smaller than this invoice
                    targetInvoice.Amount = Math.Round(targetInvoice.Amount - remainingCredit, 2);
                    remainingCredit = 0;
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task GenerateAdjustmentInvoiceAsync(PolicyAssignment policy, decimal adjustmentAmount, DateTime? customPeriodTo = null)
        {
            decimal roundedAmount = Math.Round(adjustmentAmount, 2);

            // Only generate if there is a positive adjustment after rounding
            if (roundedAmount <= 0) return;

            // An adjustment invoice covers from "Today" until the end of the CURRENT billing period.
            // For Monthly payers, this is the current month. For Annual, it's the year-end.
            var periodFrom = DateTime.UtcNow.Date;
            var periodTo = (customPeriodTo ?? policy.EndDate).Date; 

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                PolicyAssignmentId = policy.Id,
                InvoiceNo = GenerateInvoiceNo() + "-ADJ", // Suffix to mark as adjustment
                InvoiceDate = DateTime.UtcNow,
                BillingPeriodFrom = periodFrom,
                BillingPeriodTo = periodTo,
                Amount = roundedAmount, // ONLY the extra amount, not the new total
                DueDate = DateTime.UtcNow.AddDays(EGI_Backend.Domain.Constants.BusinessRules.InvoiceDueGraceDays),
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
                    DueDate = DateTime.UtcNow.AddDays(EGI_Backend.Domain.Constants.BusinessRules.InvoiceDueGraceDays),
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

            decimal balanceBefore = invoice.Amount - invoice.TotalPaid;
            if (dto.PaidAmount <= 0)
                throw new BadRequestException("Paid amount must be greater than zero.");

            if (dto.PaidAmount > balanceBefore)
                throw new BadRequestException($"Paid amount (₹{dto.PaidAmount}) exceeds the remaining balance (₹{balanceBefore}).");

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

            invoice.TotalPaid += dto.PaidAmount;
            
            if (invoice.TotalPaid >= invoice.Amount)
                invoice.Status = InvoiceStatus.Paid;
            else
                invoice.Status = InvoiceStatus.PartiallyPaid;

            // COMMISSION CALCULATION: Based on THIS POLICY'S categorized size
            var policy = await _policyRepo.GetByIdWithDetailsAsync(invoice.PolicyAssignmentId);
            if (policy != null)
            {
                decimal commissionPercentage = EGI_Backend.Domain.Constants.BusinessRules.GetCommissionRate(policy.BusinessCategory);

                // Commission is earned on the CURRENT paid amount
                decimal earnedCommission = Math.Round(dto.PaidAmount * commissionPercentage, 2);
                invoice.CommissionEarned += earnedCommission; // Accumulate as they pay
                policy.CommissionAmount += earnedCommission;
                
                await _policyRepo.UpdateAsync(policy);

                // AUDIT LOGGING: CommissionEvent for transparency
                var agentName = policy.Agent?.Name ?? "Unknown Agent";
                var ratePercent = (int)(commissionPercentage * 100);
                var logMessage = $"Commission of ₹{earnedCommission} calculated for Agent {agentName} on Invoice #{invoice.InvoiceNo} (Amount: ₹{dto.PaidAmount}, Rate: {ratePercent}%)";

                await _auditLogRepo.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = policy.AgentId.ToString(),
                    Action = "CommissionCalculation",
                    EntityName = "Invoice",
                    EntityId = invoice.Id.ToString(),
                    NewValues = logMessage,
                    Timestamp = DateTime.UtcNow
                });
            }

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
