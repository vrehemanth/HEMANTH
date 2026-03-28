using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System.IO;
using UglyToad.PdfPig;

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
        private readonly IOCRService _ocrService;
        private readonly IFraudDetectionService _fraudService;
        private readonly IAIAdjudicationService _aiService;
        private readonly IAuditLogRepository _auditRepo;
        private readonly IMemoryCache _cache;
        private readonly IHospitalRepository _hospitalRepo;
        private readonly IClinicalDispatchRepository _dispatchRepo;
 
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
            IEmailService emailService,
            IOCRService ocrService,
            IFraudDetectionService fraudService,
            IAIAdjudicationService aiService,
            IAuditLogRepository auditRepo,
            IMemoryCache cache,
            IHospitalRepository hospitalRepo,
            IClinicalDispatchRepository dispatchRepo)
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
            _ocrService = ocrService;
            _fraudService = fraudService;
            _aiService = aiService;
            _auditRepo = auditRepo;
            _cache = cache;
            _hospitalRepo = hospitalRepo;
            _dispatchRepo = dispatchRepo;
        }

        public async Task<string> SubmitClaimAsync(Guid corporateClientUserId, SubmitClaimDto dto)
        {
            if (!string.IsNullOrEmpty(dto.SubmissionToken))
            {
                if (await _claimRepo.IsDuplicateAsync(dto.SubmissionToken))
                    throw new BadRequestException("This claim has already been submitted (Duplicate Token).");
            }

            var member = await _memberRepo.GetByIdWithPolicyAsync(dto.MemberId);
            if (member == null)
                throw new NotFoundException("Member data mismatch. This individual is not recognized.");

            var policy = member.PolicyAssignment;
            if (policy == null || policy.Id != dto.PolicyAssignmentId)
                throw new NotFoundException("Policy not found or member does not belong to this policy.");

            if (await _invoiceRepo.HasUnpaidInvoicesAsync(policy.Id))
                throw new BadRequestException("Benefit Suspension: This policy has pending or overdue invoices. Claims cannot be filed until outstanding balances are settled.");

            OCRExtractedData? ocrData = null;
            if (dto.Documents.Count > 0)
            {
                try
                {
                    ocrData = await _ocrService.ExtractDataAsync(dto.Documents[0], dto.ClaimType);
                    if (ocrData != null && ocrData.IsSuccess)
                    {
                        if (dto.ClaimType == CoverageType.Life && ocrData.DateOfDeath.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.DateOfDeath.Value;
                        else if (dto.ClaimType == CoverageType.Health && ocrData.BillDate.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.BillDate.Value;
                        else if (dto.ClaimType == (CoverageType)CoverageType.Accident && ocrData.IncidentDate.HasValue && dto.IncidentDate == DateTime.MinValue)
                            dto.IncidentDate = ocrData.IncidentDate.Value;
                    }
                }
                catch { }
            }

            if (dto.IncidentDate.Date > DateTime.UtcNow.Date)
            {
                throw new BadRequestException("Claim Rejected: Incident date cannot be in the future.");
            }

            if (dto.IncidentDate.Date < policy.StartDate.Date || dto.IncidentDate.Date > policy.EndDate.Date)
            {
                throw new BadRequestException($"Claim Rejected: The incident date ({dto.IncidentDate:dd MMM yyyy}) is outside the policy coverage period ({policy.StartDate:dd MMM yyyy} to {policy.EndDate:dd MMM yyyy}).");
            }

            var submissionDeadline = policy.EndDate.AddDays(30);
            if (DateTime.UtcNow.Date > submissionDeadline.Date)
            {
                throw new BadRequestException($"Submission Blocked: The window for submitting claims for this policy period closed on {submissionDeadline:dd MMM yyyy}.");
            }

            if (policy.Status != PolicyStatus.Active && policy.Status != PolicyStatus.Expired)
                throw new ForbiddenException("Claims can only be submitted for active or recently expired policies.");

            Dependent? dependent = null;
            if (dto.DependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dto.DependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");
            }

            // 1. Status Check with "Endorsement" vs "Bereavement" distinction
            if (!member.Status || (dependent != null && !dependent.IsActive))
            {
                // If inactive, we ONLY allow a Life claim if it's a "refile" or update of an existing one.
                // If no Life claim exists in the system, it means they were removed via endorsement (End-of-Service).
                bool isRefile = dto.ClaimType == CoverageType.Life && 
                                await _claimRepo.GetApprovedClaimsTotalAsync(dto.PolicyAssignmentId, dto.MemberId, dto.DependentId, CoverageType.Life) > 0;
                
                if (!isRefile)
                {
                    string entity = dependent != null ? "dependent" : "primary member";
                    throw new ForbiddenException($"Claim Submission Terminated: This {entity} was removed from the policy (Endorsement) or is no longer eligible for coverage. Any previous and future benefit tallies are suspended.");
                }
            }

            // 2. Terminal Event Deactivation Logic (Only for active accounts filing discovery Life claim)
            if (dto.ClaimType == CoverageType.Life)
            {
                if (!dto.DependentId.HasValue && member.Status)
                {
                    // Primary Member Death
                    member.Status = false;
                    foreach (var dep in member.Dependents)
                    {
                        dep.IsActive = false;
                    }
                }
                else if (dto.DependentId.HasValue && dependent != null && dependent.IsActive)
                {
                    // Specific Dependent Death
                    dependent.IsActive = false;
                }
            }

            var client = await _clientRepo.GetByUserIdAsync(corporateClientUserId);
            if (client == null || policy.CorporateClientId != client.Id)
                throw new ForbiddenException("You are not authorized to submit claims for this policy.");

            var invoices = await _invoiceRepo.GetByPolicyAssignmentIdAsync(dto.PolicyAssignmentId);
            var overdueInvoices = invoices
                .Where(i => i.Status != InvoiceStatus.Paid && !i.InvoiceNo.EndsWith("-ADJ"))
                .Where(i => (DateTime.UtcNow.Date - i.DueDate.Date).Days >= 15)
                .ToList();

            if (overdueInvoices.Any())
            {
                var oldestDate = overdueInvoices.Min(i => i.DueDate);
                throw new BadRequestException($"Claim Rejected: Multiple invoices have been overdue since {oldestDate.ToShortDateString()}. Coverage is currently suspended.");
            }

            var plan = await _planRepo.GetByIdAsync(policy.InsurancePlanId);
            if (plan == null)
                throw new NotFoundException("Insurance plan not found.");

            var matchingCoverage = plan.Coverages.FirstOrDefault(c => c.Type == dto.ClaimType);
            if (matchingCoverage == null)
                throw new BadRequestException($"This plan does not cover '{dto.ClaimType}' claims.");

            if (dependent != null)
            {
                bool isParent = dependent.Relationship == RelationshipType.Father || dependent.Relationship == RelationshipType.Mother;
                bool coverageIncludesParents = matchingCoverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents;
                if (isParent && !coverageIncludesParents)
                    throw new BadRequestException($"The plan does not cover parents under '{dto.ClaimType}'.");

                if (!isParent && matchingCoverage.CoveredGroup == CoveredGroup.EmployeeOnly)
                    throw new BadRequestException($"The plan covers '{dto.ClaimType}' for Employee only.");
            }

            decimal coverageLimit = matchingCoverage.CoverageAmount;
            decimal utilizedAmount = await _claimRepo.GetApprovedClaimsTotalAsync(dto.PolicyAssignmentId, dto.MemberId, dto.DependentId, dto.ClaimType);
            decimal availableBalance = coverageLimit - utilizedAmount;

            if (dto.ClaimType == CoverageType.Life)
            {
                dto.ClaimAmount = coverageLimit;
            }

            if (dto.ClaimAmount <= 0)
            {
                throw new BadRequestException("Claim Rejected: Invalid claim amount.");
            }

            if (dto.ClaimAmount > availableBalance)
            {
                throw new BadRequestException($"Insufficient Coverage: Your remaining balance for {dto.ClaimType} is ₹{availableBalance:N2}.");
            }

            if (dto.Documents.Count != dto.DocumentTypes.Count)
                throw new BadRequestException("Each document must have a corresponding document type.");

            if (dto.Documents.Count == 0)
                throw new BadRequestException("At least one supporting document is required.");

            var claim = new Claim
            {
                Id = Guid.NewGuid(),
                ClaimNumber = $"CLM-{DateTime.UtcNow.Year}-{dto.ClaimAmount.ToString().GetHashCode():X4}-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}",
                PolicyAssignmentId = dto.PolicyAssignmentId,
                MemberId = dto.MemberId,
                DependentId = dto.DependentId,
                ClaimType = dto.ClaimType,
                ClaimAmount = dto.ClaimAmount,
                ClaimReason = dto.ClaimReason,
                IncidentDate = dto.IncidentDate,
                ClaimDate = DateTime.UtcNow,
                SubmissionToken = dto.SubmissionToken,
                Status = ClaimStatus.Pending
            };

            if (ocrData != null && ocrData.IsSuccess)
            {
                claim.ExtractedHospitalName = ocrData.HospitalName;
                claim.ExtractedBillDate = ocrData.BillDate;
                claim.ExtractedBillAmount = ocrData.BillAmount;
                claim.ExtractedDateOfDeath = ocrData.DateOfDeath;
                claim.ExtractedCauseOfDeath = ocrData.CauseOfDeath;
                claim.ExtractedFirNumber = ocrData.FirNumber;
                claim.ExtractedPoliceStation = ocrData.PoliceStation;
                claim.ExtractedIncidentDate = ocrData.IncidentDate;
                claim.ExtractedDiagnosis = ocrData.Diagnosis;

                var fraudResult = await _fraudService.AnalyzeClaimAsync(claim, ocrData);
                claim.FraudScore = fraudResult.Score;
                claim.FraudAnalysis = fraudResult.Analysis;
                claim.IsSuspectedFraud = fraudResult.IsFlagged;

                if (claim.IsSuspectedFraud)
                {
                    claim.RequiresAdminApproval = true;
                }

                // AI ADJUDICATION PIPELINE (Llama-3.3-70b Integration)
                if (!string.IsNullOrEmpty(ocrData.RawText))
                {
                    var aiResult = await _aiService.AdjudicateClaimAsync(claim, ocrData.RawText);
                    claim.AIConfidenceScore = aiResult.ConfidenceScore;
                    claim.AIAdjudicationReasoning = aiResult.Reasoning;
                    claim.IsAIApproved = aiResult.IsApprovedRecommendation;

                    // Autonomous Adjudication Rule: 
                    // If AI confidence is > 90% and it approves, we can mark it as 'Auto-Approved Option' 
                    // for the human reviewer to one-click confirm.
                    if (claim.IsAIApproved && claim.AIConfidenceScore >= 90 && claim.ClaimAmount < 50000)
                    {
                        claim.IsAutoApproved = true; 
                    }
                }
            }
 
            if (claim.ClaimAmount > 500000)
            {
                claim.RequiresAdminApproval = true;
            }

            var uploadedFilePaths = new List<string>();
            try
            {
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

                member.LastClaimAt = DateTime.UtcNow;
                await _memberRepo.UpdateAsync(member);

                await _claimRepo.AddAsync(claim);
                await _unitOfWork.SaveChangesAsync();

                await _notificationService.CreateNotificationAsync(corporateClientUserId, "Claim Submitted", $"Claim {claim.ClaimNumber} for {claim.ClaimType} has been submitted successfully.", "Success");

                // Notify Agent and Admins
                try
                {
                    if (policy.AgentId != Guid.Empty)
                    {
                        await _notificationService.CreateNotificationAsync(policy.AgentId, "New Claim Received", $"A new {claim.ClaimType} claim {claim.ClaimNumber} has been submitted for policy {policy.PolicyNo}.", "Info");
                    }

                    var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                    foreach (var admin in admins)
                    {
                        await _notificationService.CreateNotificationAsync(admin.Id, "Claim Submitted", $"A new {claim.ClaimType} claim {claim.ClaimNumber} was submitted for policy {policy.PolicyNo}.", "Info");
                    }
                }
                catch { }

                return $"Claim submitted successfully. Claim Number: {claim.ClaimNumber}.";
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                foreach (var path in uploadedFilePaths)
                {
                    try { await _storage.DeleteAsync(path); } catch { }
                }
                throw new BadRequestException("Multiple submission requests detected. Please wait a moment and try again.");
            }
            catch (Exception)
            {
                foreach (var path in uploadedFilePaths)
                {
                    try { await _storage.DeleteAsync(path); } catch { }
                }
                throw;
            }
        }

        public async Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto)
        {
            var user = await _userRepo.GetByIdAsync(claimsOfficerUserId);
            if (user == null) throw new UnauthorizedException("Reviewer not found.");

            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            if (claim.Status != ClaimStatus.Pending && claim.Status != ClaimStatus.InReview && claim.Status != ClaimStatus.PendingAdminApproval)
                throw new BadRequestException("Claim has already been finalized.");

            if (claim.Status == ClaimStatus.InReview && claim.ReviewedBy != claimsOfficerUserId && user.Role != UserRole.Admin)
            {
                throw new ForbiddenException("This claim is currently being reviewed by another officer.");
            }

            if (claim.Status == ClaimStatus.Pending || claim.Status == ClaimStatus.InReview)
            {
                claim.ReviewedBy = claimsOfficerUserId;
                claim.ReviewedAt = DateTime.UtcNow;
                claim.Status = ClaimStatus.InReview;
            }

            if (dto.IsApproved)
            {
                if (claim.FraudScore >= 80 && !dto.OverrideFraud && !claim.IsFraudOverridden)
                {
                    throw new BadRequestException("CRITICAL RISK: High-confidence fraud indicators detected. Override required.");
                }

                if ((claim.FraudScore >= 40 || (claim.IsSuspectedFraud && dto.OverrideFraud)) && user.Role != UserRole.Admin)
                {
                    claim.RequiresAdminApproval = true; 
                }

                if (dto.OverrideFraud && string.IsNullOrWhiteSpace(dto.FraudOverrideReason))
                {
                    throw new BadRequestException("An override justification is required.");
                }

                if (dto.OverrideFraud)
                {
                    claim.IsFraudOverridden = true;
                    claim.FraudOverriddenBy = claimsOfficerUserId;
                    claim.FraudOverrideReason = dto.FraudOverrideReason;
                }
            }

            if (dto.IsApproved)
            {
                if (claim.RequiresAdminApproval && claim.Status != ClaimStatus.PendingAdminApproval && user.Role != UserRole.Admin)
                {
                    claim.Status = ClaimStatus.PendingAdminApproval;
                    claim.ReviewedBy = claimsOfficerUserId;
                    claim.ReviewedAt = DateTime.UtcNow;
                    
                    await _unitOfWork.SaveChangesAsync();
                    
                    // Notify Admins for Executive Approval
                    try
                    {
                        var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                        foreach (var admin in admins)
                        {
                            await _notificationService.CreateNotificationAsync(admin.Id, "Executive Approval Required", $"Claim {claim.ClaimNumber} requires your final review due to high value or risk score.", "Warning");
                        }
                    }
                    catch { }
                    
                    return;
                }

                if (claim.Status == ClaimStatus.PendingAdminApproval)
                {
                    if (user.Role != UserRole.Admin)
                    {
                        throw new ForbiddenException("This high-value claim requires final sign-off from an Administrator.");
                    }
                    claim.AdminApprovedBy = claimsOfficerUserId; 
                    claim.AdminApprovedAt = DateTime.UtcNow;
                }

                claim.Status = ClaimStatus.Approved;
                claim.RejectionReason = null;

                await AdjustMemberCoverageAsync(claim);

                // Automate Balance Enforcement for Sibling Claims
                await EnforceRemainingCoverageForPendingClaimsAsync(claim);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(dto.RejectionReason))
                    throw new BadRequestException("Rejection reason is required.");

                claim.Status = ClaimStatus.Rejected;
                claim.RejectionReason = dto.RejectionReason;
            }

            await _unitOfWork.SaveChangesAsync();

            // Notification logic
            try
            {
                var policy = await _policyRepo.GetByIdWithDetailsAsync(claim.PolicyAssignmentId);
                if (policy != null)
                {
                    string title = $"Claim {claim.Status}";
                    string msgBase = $"Claim {claim.ClaimNumber} has been {claim.Status}.";
                    
                    // CRITICAL FIX: Use UserId from CorporateClient profile, NOT the profile PK
                    if (policy.CorporateClient != null)
                    {
                        await _notificationService.CreateNotificationAsync(policy.CorporateClient.UserId, title, msgBase, "Info");
                    }

                    if (policy.AgentId != Guid.Empty)
                    {
                        await _notificationService.CreateNotificationAsync(policy.AgentId, title, msgBase, "Info");
                    }
                }
            }
            catch { /* Notifications should not block the main transaction */ }
            _cache.Remove("AdminDashboardSummary");
        }

        public async Task<List<ClaimResponseDto>> GetClaimsByPolicyAsync(Guid policyAssignmentId, Guid callerUserId, string role)
        {
            var policy = await _policyRepo.GetByIdWithDetailsAsync(policyAssignmentId);
            if (policy == null) throw new NotFoundException("Policy not found.");

            bool isAuthorized = IsHighAuthority(role) || 
                              (role?.Equals("Customer", StringComparison.OrdinalIgnoreCase) == true && policy.CorporateClient?.UserId == callerUserId) ||
                              (role?.Equals("Agent", StringComparison.OrdinalIgnoreCase) == true && policy.AgentId == callerUserId);

            if (!isAuthorized) throw new ForbiddenException("Security Access Denied.");

            var claims = await _claimRepo.GetByPolicyAssignmentIdAsync(policyAssignmentId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimResponseDto>> GetPendingClaimsAsync()
        {
            var claims = await _claimRepo.GetPendingClaimsAsync();
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<ClaimDetailResponseDto>> GetClaimsByMemberAsync(Guid memberId, Guid callerUserId, string role)
        {
            var member = await _memberRepo.GetByIdAsync(memberId);
            if (member == null) throw new NotFoundException("Member not found.");

            var claims = await _claimRepo.GetByMemberIdAsync(memberId);
            return _mapper.Map<List<ClaimDetailResponseDto>>(claims);
        }

        public async Task<ClaimDetailResponseDto> GetClaimDetailAsync(Guid claimId, Guid callerUserId, string role)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

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

                decimal claimed = await _claimRepo.GetApprovedClaimsTotalAsync(policy.Id, memberId, dependentId, coverage.Type);
                decimal remaining = coverage.CoverageAmount - claimed;

                summary.CoverageBreakdown.Add(new CoverageItemDto
                {
                    ClaimType = coverage.Type.ToString(),
                    TotalCoverage = coverage.CoverageAmount,
                    TotalClaimed = claimed,
                    Remaining = Math.Max(0, remaining)
                });
            }

            // Aggregate totals using the Insurance Plan as the master limit
            summary.TotalLimitAllowed = plan.Coverages.Sum(c => c.CoverageAmount);
            summary.TotalAmountUtilized = await _claimRepo.GetApprovedClaimsTotalAsync(policy.Id, memberId, dependentId, null);
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
            claim.Status = ClaimStatus.InReview;
            claim.ReviewedBy = officerId;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReleaseClaimAsync(Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim != null && claim.Status == ClaimStatus.InReview)
            {
                claim.Status = ClaimStatus.Pending;
                claim.ReviewedBy = null;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task AdjustMemberCoverageAsync(Claim claim)
        {
            if (claim.ClaimType == CoverageType.Life)
            {
                if (claim.DependentId.HasValue)
                {
                    var dependent = await _dependentRepo.GetByIdAsync(claim.DependentId.Value);
                    if (dependent != null)
                    {
                        dependent.IsActive = false;
                        await _dependentRepo.UpdateAsync(dependent);
                    }
                }
                else
                {
                    var member = await _memberRepo.GetByIdAsync(claim.MemberId);
                    if (member != null)
                    {
                        member.Status = false;
                        await _memberRepo.UpdateAsync(member);
                    }
                }
            }
        }

        private async Task EnforceRemainingCoverageForPendingClaimsAsync(Claim approvedClaim)
        {
            // 1. Get current remaining coverage for this specific type and person
            // Note: GetCoverageSummaryAsync calculates from DB. Since approvedClaim isn't saved yet, 
            // the summary will NOT include it.
            var summary = await GetCoverageSummaryAsync(approvedClaim.MemberId, approvedClaim.DependentId);
            var coverageItem = summary.CoverageBreakdown.FirstOrDefault(c => c.ClaimType.Equals(approvedClaim.ClaimType.ToString(), StringComparison.OrdinalIgnoreCase));
            
            if (coverageItem == null) return;

            // Subtract the amount JUST approved in memory from the balance reported by DB (since DB only knows Approved/Settled)
            decimal currentRemainingBalance = coverageItem.Remaining - approvedClaim.ClaimAmount;
            var indianCulture = new System.Globalization.CultureInfo("en-IN");

            // 2. Fetch all other claims (Non-Final) that were already filed for the same person and type
            var siblingClaims = (await _claimRepo.GetByMemberIdAsync(approvedClaim.MemberId))
                .Where(c => c.Id != approvedClaim.Id && 
                            c.DependentId == approvedClaim.DependentId && 
                            c.ClaimType == approvedClaim.ClaimType &&
                            (c.Status == ClaimStatus.Pending || 
                             c.Status == ClaimStatus.InReview || 
                             c.Status == ClaimStatus.PendingAdminApproval))
                .OrderBy(c => c.ClaimDate)
                .ToList();

            if (!siblingClaims.Any()) return;

            foreach (var sc in siblingClaims)
            {
                // STRICT REJECTION RULE: If any single claim exceeds the *current* 
                // remaining balance, it is automatically rejected.
                if (sc.ClaimAmount > currentRemainingBalance)
                {
                    sc.Status = ClaimStatus.Rejected;
                    sc.RejectionReason = $"AUTOMATIC REJECTION: Plan limit of ₹{coverageItem.TotalCoverage.ToString("N0", indianCulture)} would be breached. Requested amount of ₹{sc.ClaimAmount.ToString("N0", indianCulture)} exceeds the live remaining balance of ₹{currentRemainingBalance.ToString("N0", indianCulture)} for this coverage classification.";
                    sc.ReviewedAt = DateTime.UtcNow;
                    sc.ReviewedBy = approvedClaim.AdminApprovedBy ?? approvedClaim.ReviewedBy; // Mark as system-rejected via same officer/admin
                }
                else
                {
                    // Deduct from temporary remaining balance to check the NEXT sibling claim in the queue
                    currentRemainingBalance -= sc.ClaimAmount;
                }
            }
        }

        public async Task<ClaimDetailResponseDto> RunAIAdjudicationAsync(Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            // We need a PDF document for OCR
            var pdfDoc = claim.Documents.FirstOrDefault(d => d.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));
            if (pdfDoc == null) throw new BadRequestException("No PDF document found in claim for AI analysis.");

            // 1. RE-RUN OCR
            // For simplicity, we bypass the IFormFile and read directly if physical file exists
            if (!System.IO.File.Exists(pdfDoc.FilePath)) throw new NotFoundException("Physical file not found for OCR.");

            // Mocking IFormFile for the OCR service might be complex, 
            // but OCRService just needs a way to read the stream.
            // Let's modify OCRService to accept a stream too, or just extract here for the test.
            
            // Actually, let's keep it simple and just use the extraction logic from OCRService
            var bytes = await System.IO.File.ReadAllBytesAsync(pdfDoc.FilePath);
            string rawText = "";
            using (var stream = new MemoryStream(bytes))
            using (var document = UglyToad.PdfPig.PdfDocument.Open(stream))
            {
                foreach (var page in document.GetPages())
                {
                    rawText += page.Text + " ";
                }
            }

            if (string.IsNullOrEmpty(rawText)) throw new BadRequestException("Could not extract text from document.");

            // 2. RUN AI
            var aiResult = await _aiService.AdjudicateClaimAsync(claim, rawText);
            
            claim.AIConfidenceScore = aiResult.ConfidenceScore;
            claim.AIAdjudicationReasoning = aiResult.Reasoning;
            claim.IsAIApproved = aiResult.IsApprovedRecommendation;

            if (claim.IsAIApproved && claim.AIConfidenceScore >= 90 && claim.ClaimAmount < 50000)
            {
                claim.IsAutoApproved = true;
            }

            await _unitOfWork.SaveChangesAsync();

            return await GetClaimDetailAsync(claimId, Guid.Empty, "Admin"); // caller details don't matter for detail return here
        }

        public async Task<(byte[] content, string contentType, string fileName)> GetSecureDocumentAsync(Guid userId, string role, Guid documentId)
        {
            var claimDoc = await _claimRepo.GetDocumentByIdAsync(documentId);
            string? filePath = null;
            string? fileName = null;
            bool hasAccess = IsHighAuthority(role);

            if (claimDoc != null)
            {
                if (!hasAccess && role != null && role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    if (claimDoc.Claim?.PolicyAssignment?.CorporateClient != null && 
                        claimDoc.Claim.PolicyAssignment.CorporateClient.UserId == userId)
                    {
                        hasAccess = true;
                    }
                }
                else if (!hasAccess && role != null && role.Equals("Agent", StringComparison.OrdinalIgnoreCase))
                {
                    if (claimDoc.Claim?.PolicyAssignment != null && 
                        claimDoc.Claim.PolicyAssignment.AgentId == userId)
                    {
                        hasAccess = true;
                    }
                }
                filePath = claimDoc.FilePath;
                fileName = claimDoc.FileName;
            }
            else
            {
                var clientDoc = await _clientRepo.GetDocumentByIdAsync(documentId);
                if (clientDoc == null) throw new NotFoundException("Document not found.");

                if (!hasAccess && role != null && role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    // Fix: Compare userId (User table PK) with CorporateClient.UserId
                    if (clientDoc.CorporateClient != null && clientDoc.CorporateClient.UserId == userId)
                    {
                        hasAccess = true;
                    }
                }
                
                filePath = clientDoc.FilePath;
                fileName = clientDoc.FileName;
            }

            if (!hasAccess) throw new ForbiddenException("Security Access Denied.");

            string finalPath = filePath;
            
            // If the specialized FilePath is empty, attempt recovery using the legacy FileName property
            // (Previous bugs stored the full path in FileName)
            if (string.IsNullOrEmpty(finalPath)) finalPath = fileName ?? "";

            if (!System.IO.File.Exists(finalPath))
            {
                var fileNameOnly = System.IO.Path.GetFileName(finalPath);
                
                if (string.IsNullOrEmpty(fileNameOnly))
                {
                    // If still empty, try extracting from the other property
                    fileNameOnly = System.IO.Path.GetFileName(fileName ?? "");
                }

                if (string.IsNullOrEmpty(fileNameOnly)) throw new NotFoundException("Document storage index is invalid (No filename specified).");

                // Search in common places
                var searchPaths = new List<string> {
                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileNameOnly),
                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "EGI_Backend.WebAPI", "Uploads", fileNameOnly),
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", fileNameOnly),
                    System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", "EGI_Backend.WebAPI", "Uploads", fileNameOnly)
                };

                bool found = false;
                foreach (var p in searchPaths)
                {
                    if (System.IO.File.Exists(p))
                    {
                        finalPath = p;
                        found = true;
                        break;
                    }
                }

                if (!found) throw new NotFoundException($"Document storage offline. Target segment: {fileNameOnly}");
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(finalPath);
            var extension = System.IO.Path.GetExtension(finalPath).ToLowerInvariant();
            string contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            return (bytes, contentType, fileName);
        }

        public async Task<string> GetClaimRejectionExplanationAsync(Guid userId, string role, Guid claimId)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null) throw new NotFoundException("Claim not found.");

            // 1. Authorization check: If Customer, they must own the policy or the member record
            if (role == "Customer")
            {
                var client = await _clientRepo.GetByUserIdAsync(userId);
                var policy = await _policyRepo.GetByIdAsync(claim.PolicyAssignmentId);
                if (policy == null || client == null || policy.CorporateClientId != client.Id)
                {
                    throw new ForbiddenException("You do not have permission to view this claim's details.");
                }
            }
            else if (!IsHighAuthority(role))
            {
                throw new ForbiddenException("Invalid role permissions.");
            }

            // 2. State Check
            if (claim.Status != ClaimStatus.Rejected)
            {
                return "This claim is not in a rejected state. No explanation needed.";
            }

            if (string.IsNullOrEmpty(claim.RejectionReason))
            {
                return "Your claim was rejected, but no specific reason was recorded. Please contact support.";
            }

            // 3. AI Translation (Groq)
            return await _aiService.GetClaimRejectionExplanationAsync(
                claim.RejectionReason, 
                claim.ClaimType.ToString(), 
                claim.ClaimAmount);
        }

        public async Task<MemberSearchResultDto> SearchMemberAsync(string identifier)
        {
            var result = new MemberSearchResultDto();

            // 1. Try Primary Member first
            var member = await _memberRepo.SearchAsync(identifier);
            if (member != null)
            {
                result.MemberId = member.Id;
                result.FullName = member.FullName;
                result.EmployeeCode = member.EmployeeCode;
                result.Category = "Primary Policyholder";
                result.PolicyNo = member.PolicyAssignment?.PolicyNo;
                result.PolicyAssignmentId = member.PolicyAssignmentId;
                
                // Only return active dependents for hospital search
                result.Dependents = _mapper.Map<List<DependentResponseDto>>(member.Dependents.Where(d => d.IsActive).ToList());
                return result;
            }

            // 2. Try Dependent search if member not found
            var dependent = await _memberRepo.SearchDependentAsync(identifier);
            if (dependent != null && dependent.Member != null)
            {
                result.MemberId = dependent.MemberId;
                result.DependentId = dependent.Id;
                result.FullName = dependent.FullName;
                result.EmployeeCode = dependent.Member.EmployeeCode;
                result.Category = $"Dependent ({dependent.Relationship})";
                result.PolicyNo = dependent.Member.PolicyAssignment?.PolicyNo;
                result.PolicyAssignmentId = dependent.Member.PolicyAssignmentId;
                result.Dependents = _mapper.Map<List<DependentResponseDto>>(dependent.Member.Dependents.Where(d => d.IsActive).ToList());
                return result;
            }

            throw new NotFoundException($"Identity '{identifier}' not recognized in our policy ledger. Check the Smart Card HEX ID.");
        }

        public async Task<string> RegisterPartnershipClaimAsync(Guid officerUserId, SubmitClaimDto dto)
        {
            var member = await _memberRepo.GetByIdWithPolicyAsync(dto.MemberId);
            if (member == null)
                throw new NotFoundException("Policyholder mismatch during intake.");

            var policy = member.PolicyAssignment;
            if (policy == null || policy.Id != dto.PolicyAssignmentId)
                throw new NotFoundException("Referenced policy assignment is missing or invalid.");

            Dependent? dependent = null;
            if (dto.DependentId.HasValue)
            {
                dependent = await _dependentRepo.GetByIdAsync(dto.DependentId.Value);
                if (dependent == null || dependent.MemberId != member.Id)
                    throw new NotFoundException("Dependent does not belong to this member.");
            }

            // Status Check with "Endorsement" vs "Bereavement" distinction
            if (!member.Status || (dependent != null && !dependent.IsActive))
            {
                // If inactive, we ONLY allow a Life claim if a preceding one already exists for this person (refile).
                bool isRefile = dto.ClaimType == CoverageType.Life && 
                                await _claimRepo.GetApprovedClaimsTotalAsync(policy.Id, member.Id, dto.DependentId, CoverageType.Life) > 0;
                
                if (!isRefile)
                {
                    string entity = dependent != null ? "dependent" : "primary member";
                    throw new ForbiddenException($"Partnership Billing Terminated: This {entity} was removed from the policy (Endorsement). No further coverage intakes authorized.");
                }
            }

            // Terminal Event Deactivation Logic (First filing)
            if (dto.ClaimType == CoverageType.Life)
            {
                if (!dto.DependentId.HasValue && member.Status)
                {
                    // Primary Member Death
                    member.Status = false;
                    foreach (var dep in member.Dependents)
                    {
                        dep.IsActive = false;
                    }
                }
                else if (dto.DependentId.HasValue && dependent != null && dependent.IsActive)
                {
                    // Specific Dependent Death
                    dependent.IsActive = false;
                }
            }

            if (await _invoiceRepo.HasUnpaidInvoicesAsync(policy.Id))
                throw new BadRequestException("Benefit Suspension: This policy has pending or overdue invoices. Partnership billings are restricted until payment is cleared.");

            var claim = new Claim
            {
                Id = Guid.NewGuid(),
                ClaimNumber = $"PB-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}", // PB for Partnership Bill
                PolicyAssignmentId = policy.Id,
                MemberId = member.Id,
                DependentId = dto.DependentId,
                ClaimType = dto.ClaimType,
                ClaimAmount = dto.ClaimAmount,
                ClaimReason = dto.ClaimReason,
                IncidentDate = dto.IncidentDate,
                ClaimDate = DateTime.UtcNow,
                Status = ClaimStatus.PendingAdminApproval,
                IsCashless = true,
                NetworkHospitalId = dto.NetworkHospitalId,
                ReviewedBy = officerUserId,
                ReviewedAt = DateTime.UtcNow
            };

            await _claimRepo.AddAsync(claim);

            // Handle Supporting Documents provided by the hospital/officer
            foreach (var file in dto.Documents)
            {
                var storedPath = await _storage.UploadAsync(file);
                claim.Documents.Add(new ClaimDocument
                {
                    Id = Guid.NewGuid(),
                    ClaimId = claim.Id,
                    FileName = file.FileName,
                    FilePath = storedPath,
                    DocumentType = ClaimDocumentType.HospitalDischargeReport,
                    UploadedAt = DateTime.UtcNow
                });
            }

            if (dto.DispatchId.HasValue)
            {
                var d = await _dispatchRepo.GetByIdAsync(dto.DispatchId.Value);
                if (d != null)
                {
                    d.IsClosed = true;
                    d.Status = "Admitted";
                    await _dispatchRepo.UpdateAsync(d);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            // Notify Admin for final authorization
            try
            {
                var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                foreach (var admin in admins)
                {
                    await _notificationService.CreateNotificationAsync(admin.Id, "Authorization Required", $"A new partnership bill {claim.ClaimNumber} was registered and requires your approval.", "Warning");
                }
            }
            catch { }

            _cache.Remove("AdminDashboardSummary");
            return claim.ClaimNumber;
        }

        private bool IsHighAuthority(string? role)
        {
            if (string.IsNullOrEmpty(role)) return false;
            var norm = role.Trim().Replace(" ", "").Replace("_", "").ToLowerInvariant();
            return norm == "admin" || norm == "claimsofficer" || norm == "officer";
        }
        public async Task DispatchEmergencyAsync(Guid userId, Guid hospitalId, Guid personId)
        {
            var hospital = await _hospitalRepo.GetByIdAsync(hospitalId);
            if (hospital == null) throw new NotFoundException("Hospital not found.");

            // Try find as member
            var member = await _memberRepo.GetByIdWithPolicyAsync(personId);
            Guid? dependentId = null;
            Dependent? patientDependent = null;

            if (member == null) {
                patientDependent = await _dependentRepo.GetByIdAsync(personId);
                if (patientDependent == null) throw new NotFoundException("Patient record not found.");
                member = await _memberRepo.GetByIdWithPolicyAsync(patientDependent.MemberId);
                dependentId = personId;
            }

            if (member == null) throw new NotFoundException("Parent member record missing.");

            // STRICT STATUS CHECK: No Emergency Intimation for Inactive/Terminated status
            if (!member.Status)
                throw new ForbiddenException("Emergency Intake Denied: Primary member record is inactive. Insurance coverage is suspended.");

            if (patientDependent != null && !patientDependent.IsActive)
                throw new ForbiddenException("Emergency Intake Denied: Patient dependent record is inactive. Insurance coverage is suspended.");

            var summary = await GetCoverageSummaryAsync(member.Id, dependentId);
            var patientName = dependentId.HasValue ? summary.DependentName : summary.MemberName;

            // Construct Dispatch SMS / Notification Content
            var coverageText = string.Join(", ", summary.CoverageBreakdown.Select(c => $"{c.ClaimType}: {c.Remaining:N0}"));
            var smsMsg = $"DISPATCH: {patientName} en route to {hospital.Name}. PIN: {summary.EmployeeCode}. Balances: {coverageText}.";
            var inAppMsg = $"**EMERGENCY PRE-INTIMATION**\nPatient: {patientName}\nHospital: {hospital.Name}\nRemaining Limits: {coverageText}\nMember ID: {summary.EmployeeCode}\nPolicy: {summary.PolicyNo}";

            // 1. Mock SMS (Logging)
            Console.WriteLine($"[SMS GATEWAY] To ClaimsOfficer ({hospital.ContactNumber}): {smsMsg}");

            // 2. Identify Claims Officers for the hospital
            // Since we don't have a rigid hospital assignment in User yet, notify all active Claims Officers
            var officers = await _userRepo.GetAllByRoleAsync(UserRole.ClaimsOfficer);
            foreach (var officer in officers)
            {
                await _notificationService.CreateNotificationAsync(officer.Id, "CLINICAL DISPATCH", inAppMsg, "Warning");
            }

            // 3. Persist Clinical Dispatch for Dashboard Queue
            var dispatch = new ClinicalDispatch
            {
                HospitalId = hospitalId,
                MemberId = member.Id,
                DependentId = dependentId,
                PatientName = patientName,
                EmployeeCode = summary.EmployeeCode,
                PolicyNo = summary.PolicyNo,
                CoverageSummaryJson = JsonSerializer.Serialize(summary.CoverageBreakdown),
                DispatchDate = DateTime.UtcNow,
                Status = "Intransit",
                IsClosed = false
            };
            await _dispatchRepo.AddAsync(dispatch);

            // 4. Audit Log
            var log = new AuditLog {
                UserId = userId.ToString(),
                Action = "Clinical Dispatch",
                NewValues = $"Emergency intimation for {patientName} to {hospital.Name}",
                EntityName = "Hospital",
                EntityId = hospitalId.ToString(),
                Timestamp = DateTime.UtcNow
            };
            await _auditRepo.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ClinicalDispatchDto>> GetLiveDispatchesAsync()
        {
            var active = await _dispatchRepo.GetAllActiveAsync();
            var dtos = new List<ClinicalDispatchDto>();

            foreach (var d in active)
            {
                // REAL-TIME LOOKUP: Fetch member to check live status
                var member = await _memberRepo.GetByIdWithPolicyAsync(d.MemberId);
                if (member == null || !member.Status) continue;

                // If dependent dispatch, check dependent status too
                if (d.DependentId.HasValue)
                {
                    var dep = member.Dependents.FirstOrDefault(x => x.Id == d.DependentId.Value);
                    if (dep == null || !dep.IsActive) continue;
                }

                // BALANCES SYNC: Fetch live coverage summary for remaining dispatches
                var summary = await GetCoverageSummaryAsync(d.MemberId, d.DependentId);
                
                dtos.Add(new ClinicalDispatchDto
                {
                    Id = d.Id,
                    HospitalName = d.Hospital?.Name ?? "Unknown",
                    PatientName = d.PatientName,
                    EmployeeCode = d.EmployeeCode,
                    PolicyNo = d.PolicyNo,
                    CoverageSummary = JsonSerializer.Serialize(summary.CoverageBreakdown),
                    DispatchDate = d.DispatchDate,
                    Status = d.Status,
                    HospitalId = d.HospitalId,
                    MemberId = d.MemberId,
                    DependentId = d.DependentId
                });
            }

            return dtos;
        }
    }
}
