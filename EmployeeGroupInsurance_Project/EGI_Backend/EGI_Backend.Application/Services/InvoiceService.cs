using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EGI_Backend.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IAuditLogRepository _auditLogRepo;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepo;
        public InvoiceService(
            IInvoiceRepository invoiceRepo,
            IPaymentRepository paymentRepo,
            ICorporateClientRepository clientRepo,
            IPolicyAssignmentRepository policyRepo,
            IAuditLogRepository auditLogRepo,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService,
            IUserRepository userRepo)
        {
            _invoiceRepo = invoiceRepo;
            _paymentRepo = paymentRepo;
            _clientRepo = clientRepo;
            _policyRepo = policyRepo;
            _auditLogRepo = auditLogRepo;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _userRepo = userRepo;
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

        private async Task SendInvoiceEmailAsync(Invoice invoice, PolicyAssignment policy)
        {
            var corporateClient = await _clientRepo.GetByIdAsync(policy.CorporateClientId);
            if (corporateClient != null)
            {
                var user = await _userRepo.GetByIdAsync(corporateClient.UserId);
                if (user != null && !string.IsNullOrEmpty(user.Email)) // Or use user.Email directly
                {
                    var pdfBytes = GenerateInvoicePdf(invoice, corporateClient, policy, user.Email);
                    string pdfFileName = $"Invoice_{invoice.InvoiceNo}.pdf";
                    await _emailService.SendInvoiceGeneratedEmailAsync(user.Email, corporateClient.CompanyName, invoice.InvoiceNo, invoice.Amount, invoice.DueDate, pdfBytes, pdfFileName);
                }
            }
        }

        private byte[] GenerateInvoicePdf(Invoice invoice, CorporateClient client, PolicyAssignment policy, string userEmail)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial).FontColor("#4B5563"));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    void ComposeHeader(IContainer c)
                    {
                        c.Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("EGI").FontSize(32).Bold().FontColor("#1A56DB");
                                col.Item().PaddingBottom(10).Text("EMPLOYEE GROUP INSURANCE").FontSize(9).Bold().FontColor("#6B7280").LetterSpacing(0.05f);

                                col.Item().Text("Global Headquarters").FontSize(10).FontColor("#6B7280");
                                col.Item().Text("Financial District, Tower B").FontSize(10).FontColor("#6B7280");
                                col.Item().Text("billing@egi.insurance.com").FontSize(10).FontColor("#6B7280");
                                col.Item().Text("+1 (800) 555-0199").FontSize(10).FontColor("#6B7280");
                            });

                            row.ConstantItem(250).AlignRight().Column(col =>
                            {
                                col.Item().AlignRight().Text("INVOICE").FontSize(36).FontColor("#E5E7EB").Light();
                                col.Item().AlignRight().PaddingBottom(15).Text(invoice.InvoiceNo.StartsWith("#") ? invoice.InvoiceNo : $"#{invoice.InvoiceNo}").FontSize(12).Bold().FontColor("#374151");

                                col.Item().Row(r =>
                                {
                                    r.RelativeItem().AlignRight().Text("DATE OF ISSUE:").FontSize(9).Bold().FontColor("#6B7280").LetterSpacing(0.05f);
                                    r.ConstantItem(100).AlignRight().Text(invoice.InvoiceDate.ToString("dd MMMM yyyy")).FontSize(10).Bold().FontColor("#374151");
                                });
                                col.Item().PaddingTop(5).Row(r =>
                                {
                                    r.RelativeItem().AlignRight().Text("DUE DATE:").FontSize(9).Bold().FontColor("#6B7280").LetterSpacing(0.05f);
                                    r.ConstantItem(100).AlignRight().Text(invoice.DueDate.ToString("dd MMMM yyyy")).FontSize(10).Bold().FontColor("#374151");
                                });
                            });
                        });
                    }

                    void ComposeContent(IContainer c)
                    {
                        c.PaddingVertical(1, Unit.Centimetre).Column(col =>
                        {
                            col.Spacing(25);
                            col.Item().LineHorizontal(1).LineColor("#F3F4F6");

                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Column(innerCol =>
                                {
                                    innerCol.Item().PaddingBottom(5).Text("BILLED TO").FontSize(10).Bold().FontColor("#9CA3AF").LetterSpacing(0.05f);
                                    innerCol.Item().Text(client.CompanyName).FontSize(16).Bold().FontColor("#1F2937");
                                    innerCol.Item().PaddingTop(5).Text(userEmail).FontSize(12).FontColor("#6B7280");
                                    innerCol.Item().Text(client.Phone ?? "N/A").FontSize(11).FontColor("#6B7280");
                                    innerCol.Item().Text(client.Address ?? "N/A").FontSize(11).FontColor("#6B7280");
                                });

                                row.ConstantItem(200).AlignRight().Background("#F9FAFB").Border(1).BorderColor("#F3F4F6").Padding(15).Column(innerCol =>
                                {
                                    innerCol.Item().AlignCenter().Text("TOTAL AMOUNT DUE").FontSize(9).Bold().FontColor("#9CA3AF").LetterSpacing(0.05f);
                                    innerCol.Item().PaddingTop(5).PaddingBottom(10).AlignCenter().Text($"₹{invoice.Amount:N2}").FontSize(24).Bold().FontColor("#3B82F6");
                                    
                                    var isPaid = invoice.Status == InvoiceStatus.Paid;
                                    var statusColor = isPaid ? "#10B981" : "#F59E0B";
                                    var statusText = isPaid ? "PAID" : "PENDING";
                                    var statusBg = isPaid ? "#D1FAE5" : "#FEF3C7";

                                    innerCol.Item().AlignCenter().Background(statusBg).PaddingVertical(4).PaddingHorizontal(12).Row(cr =>
                                    {
                                        cr.AutoItem().PaddingRight(5).Text("●").FontSize(10).FontColor(statusColor);
                                        cr.AutoItem().Text(statusText).FontSize(10).Bold().FontColor(statusColor);
                                    });
                                });
                            });

                            col.Item().Border(1).BorderColor("#E5E7EB").Column(innerCol =>
                            {
                                innerCol.Item().Background("#F9FAFB").PaddingHorizontal(15).PaddingVertical(10).Row(row =>
                                {
                                    row.RelativeItem().Text("DESCRIPTION").FontSize(9).Bold().FontColor("#6B7280").LetterSpacing(0.05f);
                                    row.ConstantItem(100).AlignRight().Text("AMOUNT (INR)").FontSize(9).Bold().FontColor("#6B7280").LetterSpacing(0.05f);
                                });
                                
                                innerCol.Item().LineHorizontal(1).LineColor("#E5E7EB");
                                
                                innerCol.Item().PaddingHorizontal(15).PaddingVertical(15).Row(row =>
                                {
                                    row.RelativeItem().Column(i =>
                                    {
                                        i.Item().Text("Policy Premium & Administration").FontSize(12).Bold().FontColor("#1F2937");
                                        i.Item().PaddingTop(5).Text("Comprehensive Employee Group Insurance Coverage calculation mapped directly to active membership headcount.").FontSize(10).FontColor("#6B7280");
                                    });
                                    row.ConstantItem(100).AlignRight().Text($"₹{invoice.Amount:N2}").FontSize(11).FontColor("#374151");
                                });
                            });
                        });
                    }
                });
            }).GeneratePdf();
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

            // Notify Corporate Client
            var client = await _clientRepo.GetByIdAsync(policy.CorporateClientId);
            if (client != null)
            {
                await _notificationService.CreateNotificationAsync(client.UserId, "New Invoice", $"A new invoice {invoice.InvoiceNo} for ₹{invoice.Amount:N2} has been generated for your policy {policy.PolicyNo}.", "Info");
            }

            try { await SendInvoiceEmailAsync(invoice, policy); }
            catch (Exception ex) { Console.WriteLine($"[EMAIL ERROR]: {ex.Message}"); }
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

            // Notify Corporate Client
            var client = await _clientRepo.GetByUserIdAsync(policyAssignmentId); // This might be wrong logic in existing code, but I'll stick to notifying if possible
            // Actually, policyAssignmentId corresponds to CorporateClient. I should get the client.
            var policy = await _policyRepo.GetByIdWithDetailsAsync(policyAssignmentId);
            if (policy?.CorporateClient != null)
            {
                await _notificationService.CreateNotificationAsync(policy.CorporateClient.UserId, "Credit Applied", $"A credit of ₹{creditAmount:N2} has been applied to your outstanding invoices.", "Success");
            }
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

            // Notify Corporate Client
            var client = await _clientRepo.GetByIdAsync(policy.CorporateClientId);
            if (client != null)
            {
                await _notificationService.CreateNotificationAsync(client.UserId, "Adjustment Invoice", $"An adjustment invoice {invoice.InvoiceNo} for ₹{invoice.Amount:N2} has been generated due to policy changes.", "Warning");
            }

            try { await SendInvoiceEmailAsync(invoice, policy); }
            catch (Exception ex) { Console.WriteLine($"[EMAIL ERROR]: {ex.Message}"); }
        }

        public async Task GenerateDueInvoicesAsync()
        {
            int today = DateTime.UtcNow.Day;
            var policies = await _invoiceRepo.GetActivePoliciesDueForInvoiceAsync(today);

            var newlyGeneratedInvoices = new List<(Invoice Inv, PolicyAssignment Pol)>();

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
                newlyGeneratedInvoices.Add((invoice, policy));
            }

            await _unitOfWork.SaveChangesAsync();

            // Send Emails and Notifications after saving
            foreach (var item in newlyGeneratedInvoices)
            {
                var client = await _clientRepo.GetByIdAsync(item.Pol.CorporateClientId);
                if (client != null)
                {
                    await _notificationService.CreateNotificationAsync(client.UserId, "Recurring Invoice Issued", $"Invoice #{item.Inv.InvoiceNo} for ₹{item.Inv.Amount:N2} has been issued for policy #{item.Pol.PolicyNo}.", "Info");
                }

                try { await SendInvoiceEmailAsync(item.Inv, item.Pol); }
                catch (Exception ex) { Console.WriteLine($"[EMAIL ERROR]: {ex.Message}"); }
            }
        }

        public async Task MarkOverdueInvoicesAsync()
        {
            var overdueInvoices = await _invoiceRepo.GetOverduePendingInvoicesAsync();
            var today = DateTime.UtcNow.Date;

            foreach (var inv in overdueInvoices)
            {
                await ProcessInvoiceOverdueRulesInternalAsync(inv, today);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarkOverdueInvoicesForClientAsync(Guid clientId)
        {
            // Optimize: Include PolicyAssignment and CorporateClient to avoid N+1 inside rules processor
            var invoices = await _invoiceRepo.GetByClientIdAsync(clientId);
            var today = DateTime.UtcNow.Date;

            // Filter to standard invoices (not adjustments) that need checking
            var pendingOrOverdue = invoices
                .Where(i => i.Status != InvoiceStatus.Paid && !i.InvoiceNo.EndsWith("-ADJ"))
                .ToList();

            foreach (var inv in pendingOrOverdue)
            {
                // Note: inv.PolicyAssignment is already included by GetByClientIdAsync in InvoiceRepository
                await ProcessInvoiceOverdueRulesInternalAsync(inv, today);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ProcessInvoiceOverdueRulesInternalAsync(Invoice inv, DateTime today)
        {
            // First, ensure status is 'Overdue' if past DueDate
            if (inv.DueDate.Date <= today && inv.Status == InvoiceStatus.Pending)
            {
                inv.Status = InvoiceStatus.Overdue;
            }

            if (inv.Status != InvoiceStatus.Overdue && inv.Status != InvoiceStatus.PartiallyPaid) return;

            // Only process standard group invoices for mandatory 10% penalty & inactivation
            if (!inv.InvoiceNo.EndsWith("-ADJ"))
            {
                var daysOverdue = (today - inv.DueDate.Date).Days;
                
                // Optimized: Use the already included PolicyAssignment if available
                var policy = inv.PolicyAssignment ?? await _policyRepo.GetByIdWithDetailsAsync(inv.PolicyAssignmentId);
                if (policy == null) return;

                var customerUserId = policy.CorporateClient?.UserId;
                if (customerUserId == null) return;

                // 1. Initial Deadline Penalty (Applied on or after Day 7 from Invoice)
                if (daysOverdue >= 0 && !inv.IsPenaltyApplied)
                {
                    var penaltyAmount = Math.Round(inv.Amount * 0.10m, 2);
                    inv.Amount += penaltyAmount;
                    inv.IsPenaltyApplied = true;

                    await _auditLogRepo.AddAsync(new AuditLog
                    {
                        Id = Guid.NewGuid(),
                        Action = "OverduePenaltyApplied",
                        EntityName = "Invoice",
                        EntityId = inv.Id.ToString(),
                        NewValues = $"10% overdue penalty applied. Penalty: ₹{penaltyAmount}. New Amount: ₹{inv.Amount} (Applied on Day 7)",
                        Timestamp = DateTime.UtcNow
                    });

                    await _notificationService.CreateNotificationAsync(
                        customerUserId.Value,
                        "Overdue Penalty Applied",
                        $"Invoice #{inv.InvoiceNo} is past due. A 10% late penalty has been applied. New balance: ₹{inv.Amount - inv.TotalPaid}.",
                        "Warning"
                    );
                }

                // 2. Middle Warnings
                if (daysOverdue == 3)
                {
                    await _notificationService.CreateNotificationAsync(
                        customerUserId.Value,
                        "Policy Suspension Warning",
                        $"Your policy #{policy.PolicyNo} will be suspended in 4 days if the outstanding balance for Invoice #{inv.InvoiceNo} is not settled.",
                        "Warning"
                    );
                }
                else if (daysOverdue == 6)
                {
                    await _notificationService.CreateNotificationAsync(
                        customerUserId.Value,
                        "URGENT: Policy Suspension Tomorrow",
                        $"Final Notice: Your coverage under policy #{policy.PolicyNo} will be suspended tomorrow due to non-payment of Invoice #{inv.InvoiceNo}.",
                        "Error"
                    );
                }

                // 3. Critical Enforcement (Day 14 from Invoice / Day 7 Overdue)
                if (daysOverdue >= 7)
                {
                    if (policy.Status == PolicyStatus.Active)
                    {
                        policy.Status = PolicyStatus.Inactive;
                        await _policyRepo.UpdateAsync(policy);

                        await _auditLogRepo.AddAsync(new AuditLog
                        {
                            Id = Guid.NewGuid(),
                            Action = "PolicySuspension",
                            EntityName = "PolicyAssignment",
                            EntityId = policy.Id.ToString(),
                            NewValues = $"Policy suspended due to non-payment of Invoice #{inv.InvoiceNo} 7 days after penalty notification (14 days total since invoice date).",
                            Timestamp = DateTime.UtcNow
                        });

                        await _notificationService.CreateNotificationAsync(
                            customerUserId.Value,
                            "Policy Suspended",
                            $"Your policy #{policy.PolicyNo} has been inactivated due to non-payment. Contact Customer Care for more queries",
                            "Error"
                        );
                    }
                }
            }
        }

        // ─── Payment ──────────────────────────────────────────────

        public async Task<PaymentResponseDto> PayInvoiceAsync(Guid invoiceId, Guid customerUserId, PayInvoiceDto dto)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new NotFoundException("Invoice not found.");

            if (invoice.Status == InvoiceStatus.Paid)
                throw new BadRequestException("Invoice is already paid.");

            // Rule: Block payment if 14+ days from InvoiceDate (7+ days from DueDate)
            if (!invoice.InvoiceNo.EndsWith("-ADJ") && (DateTime.UtcNow.Date - invoice.DueDate.Date).Days >= 7)
                throw new BadRequestException("Payment is blocked for this invoice because the 14-day grace period has expired. Your policy has been suspended. Please contact customer support for reinstatement.");

            decimal balanceBefore = invoice.Amount - invoice.TotalPaid;
            if (dto.PaidAmount <= 0)
                throw new BadRequestException("Paid amount must be greater than zero.");

            if (dto.PaidAmount > balanceBefore)
                throw new BadRequestException($"Paid amount (₹{dto.PaidAmount}) exceeds the remaining balance (₹{balanceBefore}).");

            decimal minRequired = Math.Min(Math.Round(invoice.Amount / 2, 2), balanceBefore);
            if (dto.PaidAmount < minRequired)
                throw new BadRequestException($"Partial payments must be at least 50% of the total invoice amount (₹{Math.Round(invoice.Amount / 2, 2)}). Minimum payment required: ₹{minRequired}.");

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

            // Notify Corporate Client and Agent
            if (policy != null)
            {
                await _notificationService.CreateNotificationAsync(customerUserId, "Payment Success", $"Your payment of ₹{dto.PaidAmount:N2} for Invoice #{invoice.InvoiceNo} was successful.", "Success");
                await _notificationService.CreateNotificationAsync(policy.AgentId, "Policy Payment Received", $"A payment of ₹{dto.PaidAmount:N2} was received for policy #{policy.PolicyNo} (Client: {policy.CorporateClient?.CompanyName}).", "Info");
            }

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
