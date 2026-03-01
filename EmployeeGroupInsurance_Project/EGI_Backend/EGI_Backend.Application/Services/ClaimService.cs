using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClaimService(
            IClaimRepository claimRepo,
            IPolicyAssignmentRepository policyRepo,
            IInsurancePlanRepository planRepo,
            ICorporateClientRepository clientRepo,
            IUserRepository userRepo,
            IDocumentStorageService storage,
            IMemberRepository memberRepo,
            IDependentRepository dependentRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _claimRepo = claimRepo;
            _policyRepo = policyRepo;
            _planRepo = planRepo;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
            _storage = storage;
            _memberRepo = memberRepo;
            _dependentRepo = dependentRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            // 3. Verify the Member belongs to this policy
            var member = policy.Members.FirstOrDefault(m => m.Id == dto.MemberId);
            if (member == null)
                throw new NotFoundException("Member does not belong to this policy.");

            // 4. Verify the Dependent (if provided) belongs to this Member
            Dependent? dependent = null;
            if (dto.DependentId.HasValue)
            {
                dependent = member.Dependents.FirstOrDefault(d => d.Id == dto.DependentId.Value);
                if (dependent == null)
                    throw new NotFoundException("Dependent does not belong to this member.");
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

            // 12. Upload and attach documents
            for (int i = 0; i < dto.Documents.Count; i++)
            {
                var file = dto.Documents[i];
                var filePath = await _storage.UploadAsync(file);

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

            return $"Claim submitted successfully. Claim Number: {claim.ClaimNumber}. Status: Pending Review.";
        }

        public async Task ReviewClaimAsync(Guid claimsOfficerUserId, Guid claimId, ReviewClaimDto dto)
        {
            var claim = await _claimRepo.GetByIdAsync(claimId);
            if (claim == null)
                throw new NotFoundException("Claim not found.");

            if (claim.Status != ClaimStatus.Pending)
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

            return summary;
        }
    }
}
