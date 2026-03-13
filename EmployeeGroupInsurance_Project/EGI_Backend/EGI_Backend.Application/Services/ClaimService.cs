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
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IInsurancePlanRepository _planRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IUserRepository _userRepo;
        private readonly IDocumentStorageService _storage;
        private readonly IMemberRepository _memberRepo;
        private readonly IDependentRepository _dependentRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;

        public ClaimService(
            IClaimRepository claimRepo,
            IPolicyAssignmentRepository policyRepo,
            IInsurancePlanRepository planRepo,
            ICorporateClientRepository clientRepo,
            IUserRepository userRepo,
            IDocumentStorageService storage,
            IMemberRepository memberRepo,
            IDependentRepository dependentRepo,
            IInvoiceRepository invoiceRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            INotificationService notificationService,
            IEmailService emailService)
        {
            _claimRepo = claimRepo;
            _policyRepo = policyRepo;
            _planRepo = planRepo;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
            _storage = storage;
            _memberRepo = memberRepo;
            _dependentRepo = dependentRepo;
            _invoiceRepo = invoiceRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
            _emailService = emailService;
        }

        public async Task<string> SubmitClaimAsync(Guid corporateClientUserId, SubmitClaimDto dto)
        {
            // 1. Verify the PolicyAssignment exists and is Active
            var policy = await _policyRepo.GetByIdWithDetailsAsync(dto.PolicyAssignmentId);
            if (policy == null)
                throw new NotFoundException("Policy not found.");

            if (policy.Status != PolicyStatus.Active)
                throw new BadRequestException("Policy is not active. Claims cannot be submitted.");

            // 2. Verify the CorporateClient owns this policy
            var client = await _clientRepo.GetByUserIdAsync(corporateClientUserId);
            if (client == null || policy.CorporateClientId != client.Id)
                throw new ForbiddenException("You are not authorized to submit claims for this policy.");

            // 2.1 Verify Premium Payment (Strict check)
            var invoices = await _invoiceRepo.GetByPolicyAssignmentIdAsync(dto.PolicyAssignmentId);
            var unpaidInvoice = invoices
                .Where(i => i.Status != InvoiceStatus.Paid && i.InvoiceDate.AddDays(15) < DateTime.UtcNow)
                .OrderBy(i => i.InvoiceDate)
                .FirstOrDefault();

            if (unpaidInvoice != null)
            {
                throw new BadRequestException($"Premium overdue since {unpaidInvoice.InvoiceDate.ToShortDateString()}. The 15-day grace period has expired. Please settle your outstanding invoices to activate benefit claims.");
            }

            // 3. Verify the Member belongs to this policy
            var member = policy.Members.FirstOrDefault(m => m.Id == dto.MemberId);
            if (member == null)
                throw new NotFoundException("Member does not belong to this policy.");

            if (!member.Status)
                throw new BadRequestException("This member is currently inactive. Claims cannot be processed for inactive members.");

            // 4. Verify the Dependent (if provided) belongs to this Member
            Dependent? dependent = null;
            if (dto.DependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dto.DependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");

                if (!dependent.IsActive)
                    throw new BadRequestException("This dependent is currently inactive. Claims cannot be processed for inactive dependents.");
            }

            // 5. Verify the Plan covers this ClaimType
            var plan = await _planRepo.GetByIdAsync(policy.InsurancePlanId);
            if (plan == null)
                throw new NotFoundException("Insurance plan not found.");

            var matchingCoverage = plan.Coverages.FirstOrDefault(c => c.Type == dto.ClaimType);
            if (matchingCoverage == null)
                throw new BadRequestException($"This plan does not cover '{dto.ClaimType}' claims. Claim rejected.");

            // 6. Verify the CoveredGroup allows this Dependent to claim this type
            if (dependent != null)
            {
                bool isParent = dependent.Relationship == RelationshipType.Father || dependent.Relationship == RelationshipType.Mother;
                bool coverageIncludesParents = matchingCoverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents;
                bool coverageIncludesFamily = matchingCoverage.CoveredGroup == CoveredGroup.EmployeeAndFamily || coverageIncludesParents;

                if (isParent && !coverageIncludesParents)
                    throw new BadRequestException($"The plan does not cover parents under '{dto.ClaimType}'. Claim rejected.");

                if (!isParent && matchingCoverage.CoveredGroup == CoveredGroup.EmployeeOnly)
                    throw new BadRequestException($"The plan covers '{dto.ClaimType}' for Employee only. Dependents are not covered.");
            }

            // 7. Determine the SumInsured for this claimant (member or dependent)
            decimal claimantSumInsured = dto.DependentId.HasValue
                ? (dependent?.SumInsured ?? 0m)
                : member.SumInsured;

            // 8. Validate single claim amount does not exceed SumInsured
            if (dto.ClaimAmount > claimantSumInsured)
                throw new BadRequestException($"Claim amount (₹{dto.ClaimAmount}) exceeds the Sum Insured (₹{claimantSumInsured}) for this claimant.");

            // 9. Validate CUMULATIVE claims for this specific ClaimType don't exceed the coverage amount
            decimal previousApproved = await _claimRepo.GetApprovedClaimsTotalAsync(dto.MemberId, dto.DependentId, dto.ClaimType);
            decimal coverageLimit = matchingCoverage.CoverageAmount;

            if ((previousApproved + dto.ClaimAmount) > coverageLimit)
            {
                decimal remaining = coverageLimit - previousApproved;
                throw new BadRequestException(
                    $"Cumulative claim limit exceeded for '{dto.ClaimType}'. " +
                    $"Coverage Limit: ₹{coverageLimit}, Already Claimed: ₹{previousApproved}, Remaining: ₹{remaining}.");
            }

            // 10. Validate documents count matches document types
            if (dto.Documents.Count != dto.DocumentTypes.Count)
                throw new BadRequestException("Each document must have a corresponding document type.");

            if (dto.Documents.Count == 0)
                throw new BadRequestException("At least one supporting document is required.");

            // 11. All checks passed — create the claim
            var claim = new Claim
            {
                Id = Guid.NewGuid(),
                ClaimNumber = $"CLM-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PolicyAssignmentId = dto.PolicyAssignmentId,
                MemberId = dto.MemberId,
                DependentId = dto.DependentId,
                ClaimType = dto.ClaimType,
                ClaimAmount = dto.ClaimAmount,
                ClaimReason = dto.ClaimReason,
                ClaimDate = DateTime.UtcNow,
                Status = ClaimStatus.Pending
            };

            var uploadedFilePaths = new List<string>();
            try
            {
                // 12. Upload and attach documents
                for (int i = 0; i < dto.Documents.Count; i++)
                {
                    var file = dto.Documents[i];
                    var filePath = await _storage.UploadAsync(file);
                    uploadedFilePaths.Add(filePath);

                    claim.Documents.Add(new ClaimDocument
                    {
                        Id = Guid.NewGuid(),
                        ClaimId = claim.Id,
                        DocumentType = dto.DocumentTypes[i],
                        FileName = file.FileName,
                        FilePath = filePath,
                        UploadedAt = DateTime.UtcNow
                    });
                }

                await _claimRepo.AddAsync(claim);
                await _unitOfWork.SaveChangesAsync();

                // Notify Corporate Client
                await _notificationService.CreateNotificationAsync(corporateClientUserId, "Claim Submitted", $"Claim {claim.ClaimNumber} for {claim.ClaimType} has been submitted successfully.", "Success");

                return $"Claim submitted successfully. Claim Number: {claim.ClaimNumber}. Status: Pending Review.";
            }
            catch (Exception)
            {
                // 13. Cleanup uploaded files if the database transaction fails
                foreach (var path in uploadedFilePaths)
                {
                    try { await _storage.DeleteAsync(path); } catch { /* Ignore cleanup errors */ }
                }
                throw;
            }
        }

        public async Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null)
                throw new NotFoundException("Claim not found.");

            if (claim.Status != ClaimStatus.Pending && claim.Status != ClaimStatus.InReview)
                throw new BadRequestException("Claim has already been reviewed.");

            claim.ReviewedBy = claimsOfficerUserId;
            claim.ReviewedAt = DateTime.UtcNow;

            if (dto.IsApproved)
            {
                claim.Status = ClaimStatus.Approved;
                claim.RejectionReason = null;

                // Decrement SumInsured from Member or Dependent
                if (claim.DependentId.HasValue)
                {
                    var dependent = await _dependentRepo.GetByIdAsync(claim.DependentId.Value);
                    if (dependent != null)
                    {
                        dependent.SumInsured = Math.Max(0, dependent.SumInsured - claim.ClaimAmount);
                        await _dependentRepo.UpdateAsync(dependent);
                    }
                }
                else
                {
                    var member = await _memberRepo.GetByIdAsync(claim.MemberId);
                    if (member != null)
                    {
                        member.SumInsured = Math.Max(0, member.SumInsured - claim.ClaimAmount);
                        await _memberRepo.UpdateAsync(member);
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dto.RejectionReason))
                    throw new BadRequestException("Rejection reason is required when rejecting a claim.");

                claim.Status = ClaimStatus.Rejected;
                claim.RejectionReason = dto.RejectionReason;
            }

            await _unitOfWork.SaveChangesAsync();

            // Send in-app notifications
            try
            {
                var policy = await _policyRepo.GetByIdWithDetailsAsync(claim.PolicyAssignmentId);
                if (policy != null)
                {
                    var client = await _clientRepo.GetByIdAsync(policy.CorporateClientId);

                    string title = $"Claim {claim.Status}";
                    string msgBase = $"Claim {claim.ClaimNumber} for {claim.ClaimType} has been {claim.Status.ToString().ToLower()}.";
                    string type = claim.Status == ClaimStatus.Approved ? "Success" : "Error";

                    // Notify Corporate Client
                    if (client != null)
                    {
                        await _notificationService.CreateNotificationAsync(client.UserId, title, msgBase, type);
                    }

                    // Notify Agent
                    if (policy.AgentId != Guid.Empty)
                    {
                        await _notificationService.CreateNotificationAsync(policy.AgentId, title, msgBase, type);
                    }

                    // Notify Member
                    var member = await _memberRepo.GetByIdAsync(claim.MemberId);
                    if (member != null && !string.IsNullOrEmpty(member.Email))
                    {
                        var memberUser = await _userRepo.GetByEmailAsync(member.Email);
                        if (memberUser != null)
                        {
                            await _notificationService.CreateNotificationAsync(memberUser.Id, title, msgBase, type);
                        }

                        // Send Approved Email with PDF Attachment if Approved
                        if (dto.IsApproved)
                        {
                            var pdfBytes = GenerateClaimInvoicePdf(claim, member);
                            string pdfFileName = $"Settlement_{claim.ClaimNumber}.pdf";
                            await _emailService.SendClaimApprovedEmailAsync(member.Email, member.FullName, claim.ClaimNumber, claim.ClaimAmount, pdfBytes, pdfFileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARNING] Failed to send notifications for claim {claimId}: {ex.Message}");
            }
        }

        public async Task<List<ClaimResponseDto>> GetClaimsByPolicyAsync(Guid policyAssignmentId)
        {
            var claims = await _claimRepo.GetByPolicyAssignmentIdAsync(policyAssignmentId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimResponseDto>> GetPendingClaimsAsync()
        {
            var claims = await _claimRepo.GetPendingClaimsAsync();
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimDetailResponseDto>> GetClaimsByMemberAsync(Guid memberId)
        {
            var claims = await _claimRepo.GetByMemberIdAsync(memberId);
            return _mapper.Map<List<ClaimDetailResponseDto>>(claims);
        }

        public async Task<ClaimDetailResponseDto> GetClaimDetailAsync(Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null)
                throw new NotFoundException("Claim not found.");
            return _mapper.Map<ClaimDetailResponseDto>(claim);
        }

        public async Task<CoverageSummaryDto> GetCoverageSummaryAsync(Guid memberId, Guid? dependentId)
        {
            var member = await _memberRepo.GetByIdWithPolicyAsync(memberId);
            if (member == null)
                throw new NotFoundException("Member not found.");

            var policy = member.PolicyAssignment;
            if (policy == null)
                throw new NotFoundException("Policy assignment not found for this member.");

            var plan = policy.InsurancePlan;
            if (plan == null)
                throw new NotFoundException("Insurance plan not found.");

            // Validate dependent belongs to member (if provided)
            Dependent? dependent = null;
            if (dependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");
            }

            var summary = new CoverageSummaryDto
            {
                MemberId = member.Id,
                MemberName = member.FullName,
                EmployeeCode = member.EmployeeCode,
                DependentId = dependentId,
                DependentName = dependent?.FullName,
                PolicyNo = policy.PolicyNo
            };

            // For each coverage in the plan, calculate remaining balance
            foreach (var coverage in plan.Coverages)
            {
                // Skip coverages that don't apply to this dependent relationship
                if (dependent != null)
                {
                    bool isParent = dependent.Relationship == RelationshipType.Father || dependent.Relationship == RelationshipType.Mother;
                    bool coverageIncludesParents = coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents;
                    bool coverageIncludesFamily = coverage.CoveredGroup == CoveredGroup.EmployeeAndFamily || coverageIncludesParents;

                    if (isParent && !coverageIncludesParents) continue;
                    if (!isParent && coverage.CoveredGroup == CoveredGroup.EmployeeOnly) continue;
                }

                decimal claimed = await _claimRepo.GetApprovedClaimsTotalAsync(memberId, dependentId, coverage.Type);
                decimal remaining = coverage.CoverageAmount - claimed;

                summary.CoverageBreakdown.Add(new CoverageItemDto
                {
                    ClaimType = coverage.Type.ToString(),
                    TotalCoverage = coverage.CoverageAmount,
                    TotalClaimed = claimed,
                    Remaining = remaining < 0 ? 0m : remaining
                });
            }

            // Aggregate totals using the Member/Dependent SumInsured as the master limit
            summary.TotalLimitAllowed = dependent?.SumInsured ?? member.SumInsured;
            summary.TotalAmountUtilized = await _claimRepo.GetApprovedClaimsTotalAsync(memberId, dependentId, null);
            summary.RemainingBalance = Math.Max(0, summary.TotalLimitAllowed - summary.TotalAmountUtilized);

            return summary;
        }

        public async Task<List<ClaimResponseDto>> GetClaimsReviewedByOfficerAsync(Guid officerId)
        {
            var claims = await _claimRepo.GetReviewedByOfficerAsync(officerId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task TakeClaimAsync(Guid officerId, Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            if (claim.Status == ClaimStatus.InReview && claim.ReviewedBy != officerId)
            {
                throw new BadRequestException("This claim is already being reviewed by another officer.");
            }

            if (claim.Status != ClaimStatus.Pending && claim.Status != ClaimStatus.InReview)
            {
                throw new BadRequestException("This claim has already been adjudicated.");
            }

            claim.Status = ClaimStatus.InReview;
            claim.ReviewedBy = officerId;
            // Note: We don't set ReviewedAt yet, only on final decision
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReleaseClaimAsync(Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) return;

            if (claim.Status == ClaimStatus.InReview)
            {
                claim.Status = ClaimStatus.Pending;
                claim.ReviewedBy = null;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private byte[] GenerateClaimInvoicePdf(Claim claim, Member member)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Claim Settlement Invoice")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            x.Item().Text($"Claim Number: {claim.ClaimNumber}");
                            x.Item().Text($"Member: {member.FullName} ({member.EmployeeCode})");
                            x.Item().Text($"Type: {claim.ClaimType}");
                            x.Item().Text($"Approved Settled Amount: ₹{claim.ClaimAmount:N2}")
                                .Bold().FontSize(16).FontColor(Colors.Green.Darken2);
                            x.Item().Text($"Settlement Date: {DateTime.UtcNow.ToString("dd MMM yyyy")}");
                            x.Item().Text("This is a system generated settlement receipt. No signature is required.");
                        });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf();
        }
    }
}
