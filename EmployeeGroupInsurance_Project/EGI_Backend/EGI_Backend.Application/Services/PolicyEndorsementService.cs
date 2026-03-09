using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System.Text.Json;

namespace EGI_Backend.Application.Services
{
    public class PolicyEndorsementService : IPolicyEndorsementService
    {
        private readonly IPolicyEndorsementRepository _endorsementRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly IDependentRepository _dependentRepo;
        private readonly IInvoiceService _invoiceService;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolicyEndorsementService(
            IPolicyEndorsementRepository endorsementRepo,
            IPolicyAssignmentRepository policyRepo,
            IMemberRepository memberRepo,
            IDependentRepository dependentRepo,
            ICorporateClientRepository clientRepo,
            IInvoiceService invoiceService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _endorsementRepo = endorsementRepo;
            _policyRepo = policyRepo;
            _memberRepo = memberRepo;
            _dependentRepo = dependentRepo;
            _clientRepo = clientRepo;
            _invoiceService = invoiceService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EndorsementResponseDto> SubmitEndorsementAsync(Guid customerId, SubmitEndorsementDto dto)
        {
            var policy = await _policyRepo.GetByIdWithDetailsAsync(dto.PolicyAssignmentId);
            if (policy == null) throw new NotFoundException("Policy not found.");

            // Flatten the EndorsementData to a string for DB storage
            string jsonData = JsonSerializer.Serialize(dto.EndorsementData);
            using var jsonDoc = JsonDocument.Parse(jsonData);
            var root = jsonDoc.RootElement;

            // VALIDATION: Prevent removing already inactive members/dependents
            if (dto.Type == EndorsementType.RemoveMember)
            {
                if (!root.TryGetProperty("MemberId", out var idProp) || !Guid.TryParse(idProp.GetString(), out var mid))
                    throw new BadRequestException("MemberId is required.");
                
                var m = await _memberRepo.GetByIdAsync(mid);
                if (m == null || !m.Status) throw new BadRequestException("This member is already inactive or not found.");
            }
            else if (dto.Type == EndorsementType.RemoveDependent)
            {
                if (!root.TryGetProperty("DependentId", out var idProp) || !Guid.TryParse(idProp.GetString(), out var did))
                    throw new BadRequestException("DependentId is required.");

                var d = await _dependentRepo.GetByIdAsync(did);
                if (d == null || !d.IsActive) throw new BadRequestException("This dependent is already inactive or not found.");
            }

            // Calculate estimated premium adjustment
            decimal adjustment = CalculateProratedAdjustment(policy, dto.Type, jsonData);

            var endorsement = new PolicyEndorsement
            {
                Id = Guid.NewGuid(),
                PolicyAssignmentId = dto.PolicyAssignmentId,
                Type = dto.Type,
                Description = dto.Description,
                EndorsementData = jsonData,
                Status = EndorsementStatus.Pending,
                PremiumAdjustment = adjustment,
                RequestedByUserId = customerId,
                CreatedAt = DateTime.UtcNow
            };

            await _endorsementRepo.AddAsync(endorsement);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EndorsementResponseDto>(endorsement);
        }

        public async Task<EndorsementResponseDto> ReviewEndorsementAsync(Guid agentId, Guid endorsementId, ReviewEndorsementDto dto)
        {
            var endorsement = await _endorsementRepo.GetByIdAsync(endorsementId);
            if (endorsement == null) throw new NotFoundException("Endorsement not found.");

            if (endorsement.Status != EndorsementStatus.Pending)
                throw new BadRequestException("Endorsement has already been processed.");

            var policy = await _policyRepo.GetByIdWithDetailsAsync(endorsement.PolicyAssignmentId);
            if (policy == null) throw new NotFoundException("Associated policy not found.");

            endorsement.Status = dto.Status;
            endorsement.ReviewedByUserId = agentId;
            endorsement.ReviewedAt = DateTime.UtcNow;

            if (dto.Status == EndorsementStatus.Approved)
            {
                // 1. Calculate the FULL annual rate impact (un-prorated)
                decimal fullAnnualImpact = CalculateFullAnnualRate(policy, endorsement.Type, endorsement.EndorsementData);

                // 2. Apply the physical changes (Add member/dependent, soft-delete removed ones)
                await ApplyEndorsementChanges(policy, endorsement);

                // 3. Update Policy master premium data
                policy.AnnualPremium += fullAnnualImpact;
                policy.TotalPremium += Math.Round(fullAnnualImpact * (decimal)((policy.EndDate.Date - DateTime.UtcNow.Date).TotalDays / 365.0), 2);

                // 4. Calculate and record the COMMISSION ADJUSTMENT for this endorsement
                // RE-CATEGORIZATION: Recalculate THIS POLICY size based on the NEW member headcounts
                int policyLives = policy.Members.Count + policy.Members.Sum(m => m.Dependents.Count(d => d.IsActive));
                
                policy.BusinessCategory = EGI_Backend.Domain.Constants.BusinessRules.GetCategoryByHeadcount(policyLives);
                decimal commissionPercentage = EGI_Backend.Domain.Constants.BusinessRules.GetCommissionRate(policy.BusinessCategory);
                
                endorsement.CommissionAdjustment = Math.Round(endorsement.PremiumAdjustment * commissionPercentage, 2);

                await _policyRepo.UpdateAsync(policy);

                // 4. Financial adjustments
                if (endorsement.PremiumAdjustment > 0)
                {
                    DateTime? customPeriodTo = null;
                    if (policy.BillingFrequency == BillingFrequency.Monthly)
                    {
                        var cycleStart = policy.StartDate.Date;
                        while (cycleStart.AddMonths(1) <= DateTime.UtcNow.Date)
                            cycleStart = cycleStart.AddMonths(1);
                        customPeriodTo = cycleStart.AddMonths(1).AddDays(-1);
                    }

                    await _invoiceService.GenerateAdjustmentInvoiceAsync(policy, endorsement.PremiumAdjustment, customPeriodTo);
                }
                else if (endorsement.PremiumAdjustment < 0)
                {
                    await _invoiceService.ApplyCreditToNextInvoiceAsync(policy.Id, Math.Abs(endorsement.PremiumAdjustment));
                }
            }

            await _endorsementRepo.UpdateAsync(endorsement);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EndorsementResponseDto>(endorsement);
        }

        public async Task<List<EndorsementResponseDto>> GetEndorsementsByPolicyAsync(Guid policyAssignmentId)
        {
            var endorsements = await _endorsementRepo.GetByPolicyIdAsync(policyAssignmentId);
            return _mapper.Map<List<EndorsementResponseDto>>(endorsements);
        }

        public async Task<List<EndorsementResponseDto>> GetPendingEndorsementsAsync()
        {
            var endorsements = await _endorsementRepo.GetPendingAsync();
            return _mapper.Map<List<EndorsementResponseDto>>(endorsements);
        }

        // ─── Private Helpers ────────────────────────────────────────────────────

        private async Task ApplyEndorsementChanges(PolicyAssignment policy, PolicyEndorsement e)
        {
            using var doc = JsonDocument.Parse(e.EndorsementData);
            var root = doc.RootElement;

            switch (e.Type)
            {
                case EndorsementType.AddMember:
                    string firstName = root.TryGetProperty("FirstName", out var fn) ? fn.GetString() ?? "" : "";
                    string lastName  = root.TryGetProperty("LastName",  out var ln) ? ln.GetString() ?? "" : "";
                    string fullName  = root.TryGetProperty("FullName",  out var fullN) ? fullN.GetString() ?? "" : $"{firstName} {lastName}".Trim();

                    var member = new Member
                    {
                        Id               = Guid.NewGuid(),
                        PolicyAssignmentId = policy.Id,
                        EmployeeCode     = root.TryGetProperty("EmployeeCode", out var ec)  ? ec.GetString()  ?? "" : "",
                        FullName         = fullName,
                        Email            = root.TryGetProperty("Email",        out var em)  ? em.GetString()  ?? "" : "",
                        PhoneNo          = root.TryGetProperty("PhoneNo",      out var ph)  ? ph.GetString()  ?? "" : "",
                        DateOfBirth      = DateTime.TryParse(
                                               root.TryGetProperty("DateOfBirth", out var dob) ? dob.GetString() : null,
                                               out var parsedDob) ? parsedDob : DateTime.MinValue,
                        Gender           = root.TryGetProperty("Gender",       out var gen) ? ParseEnum<Gender>(gen) : Gender.Male,
                        SumInsured       = GetHealthSumInsured(policy.InsurancePlan, CoveredGroup.EmployeeOnly),
                        Status           = true,
                        CreatedAt        = DateTime.UtcNow
                    };
                    await _memberRepo.AddAsync(member);
                    break;

                case EndorsementType.AddDependent:
                    if (!root.TryGetProperty("MemberId", out var midProp) || string.IsNullOrEmpty(midProp.GetString()))
                        throw new BadRequestException("MemberId is required for AddDependent endorsement.");

                    var memberId  = Guid.Parse(midProp.GetString()!);
                    var depFullName = root.TryGetProperty("FullName", out var dfn)
                        ? dfn.GetString() ?? "" : "";

                    if (!root.TryGetProperty("Relationship", out var relPropDep))
                        throw new BadRequestException("Relationship is required for AddDependent endorsement.");

                    var relationship = ParseEnum<RelationshipType>(relPropDep);

                    var dependent = new Dependent
                    {
                        Id           = Guid.NewGuid(),
                        MemberId     = memberId,
                        FullName     = depFullName,
                        Relationship = relationship,
                        DateOfBirth  = DateTime.TryParse(
                                           root.TryGetProperty("DateOfBirth", out var ddob) ? ddob.GetString() : null,
                                           out var parsedDepDob) ? parsedDepDob : DateTime.MinValue,
                        Gender       = root.TryGetProperty("Gender", out var dgen) ? ParseEnum<Gender>(dgen) : Gender.Male,
                        SumInsured   = 0,
                        IsActive     = true
                    };

                    if (policy.InsurancePlan != null)
                    {
                        var targetGroup = MapToCoveredGroup(dependent.Relationship);
                        dependent.SumInsured = GetHealthSumInsured(policy.InsurancePlan, targetGroup);
                    }

                    await _dependentRepo.AddAsync(dependent);
                    break;

                case EndorsementType.RemoveMember:
                    if (!root.TryGetProperty("MemberId", out var rmProp) || string.IsNullOrEmpty(rmProp.GetString()))
                        throw new BadRequestException("MemberId is required for RemoveMember endorsement.");

                    var mId = Guid.Parse(rmProp.GetString()!);
                    var m   = await _memberRepo.GetByIdAsync(mId);
                    if (m == null) throw new NotFoundException($"Member with ID '{mId}' not found.");

                    m.Status = false; // Soft-delete
                    await _memberRepo.UpdateAsync(m);
                    break;

                case EndorsementType.RemoveDependent:
                    if (!root.TryGetProperty("DependentId", out var rdProp) || string.IsNullOrEmpty(rdProp.GetString()))
                        throw new BadRequestException("DependentId is required for RemoveDependent endorsement.");

                    var depId = Guid.Parse(rdProp.GetString()!);
                    var dep   = await _dependentRepo.GetByIdAsync(depId);
                    if (dep == null) throw new NotFoundException($"Dependent with ID '{depId}' not found.");

                    dep.IsActive = false; // Soft-delete (preserves claim history)
                    await _dependentRepo.UpdateAsync(dep);
                    break;

                default:
                    throw new BadRequestException($"Unsupported endorsement type: {e.Type}");
            }
        }

        private static T ParseEnum<T>(JsonElement element) where T : struct, Enum
        {
            if (element.ValueKind == JsonValueKind.Number)
                return (T)(object)element.GetInt32();

            if (element.ValueKind == JsonValueKind.String)
            {
                if (Enum.TryParse<T>(element.GetString() ?? "", true, out var result))
                    return result;
            }

            return default;
        }

        private static CoveredGroup MapToCoveredGroup(RelationshipType relationship) =>
            relationship switch
            {
                RelationshipType.Spouse => CoveredGroup.EmployeeAndFamily,
                RelationshipType.Child  => CoveredGroup.EmployeeAndFamily,
                RelationshipType.Father => CoveredGroup.EmployeeFamilyAndParents,
                RelationshipType.Mother => CoveredGroup.EmployeeFamilyAndParents,
                _                       => CoveredGroup.EmployeeOnly
            };

        private static decimal GetHealthSumInsured(InsurancePlan? plan, CoveredGroup targetGroup)
        {
            if (plan?.Coverages == null) return 0;

            var coverage = plan.Coverages
                .Where(c => c.Type == CoverageType.Health && c.IsActive)
                .OrderByDescending(c => c.CoveredGroup == targetGroup)
                .ThenBy(c => (int)c.CoveredGroup)
                .FirstOrDefault()
                           ?? plan.Coverages
                              .Where(c => c.Type == CoverageType.Health && c.IsActive)
                              .OrderBy(c => (int)c.CoveredGroup)
                              .FirstOrDefault();

            return coverage?.CoverageAmount ?? 0;
        }

        private decimal CalculateFullAnnualRate(PolicyAssignment policy, EndorsementType type, string jsonStr)
        {
            if (policy.InsurancePlan == null) return 0;

            decimal cost = policy.InsurancePlan.BasePremium * GetMultiplier(type, jsonStr);

            if (type == EndorsementType.RemoveMember || type == EndorsementType.RemoveDependent)
                cost *= -1m;

            return Math.Round(cost, 2);
        }

        private decimal CalculateProratedAdjustment(PolicyAssignment policy, EndorsementType type, string jsonStr)
        {
            var annualRate = CalculateFullAnnualRate(policy, type, jsonStr);
            if (annualRate == 0) return 0;

            var today = DateTime.UtcNow.Date;
            if (today >= policy.EndDate.Date) return 0;

            // Determine the "Reference End Date" for the adjustment invoice
            // For Annual payers: Charge/Credit until the end of the policy year.
            // For Monthly payers: Charge/Credit ONLY for the remaining days of the CURRENT month.
            // This is because future months will automatically use the updated monthly rate.
            DateTime referenceEndDate;
            if (policy.BillingFrequency == BillingFrequency.Monthly)
            {
                var cycleStart = policy.StartDate.Date;
                while (cycleStart.AddMonths(1) <= today)
                {
                    cycleStart = cycleStart.AddMonths(1);
                }
                referenceEndDate = cycleStart.AddMonths(1).AddDays(-1);

                // Boundary check
                if (referenceEndDate > policy.EndDate.Date)
                    referenceEndDate = policy.EndDate.Date;
            }
            else
            {
                referenceEndDate = policy.EndDate.Date;
            }

            const decimal daysInYear = 365m;
            var daysInAdjustmentPeriod = (decimal)(referenceEndDate - today).TotalDays;

            // Ensure non-negative or at least partial-day credit handling if needed
            if (daysInAdjustmentPeriod < 0) daysInAdjustmentPeriod = 0;

            return Math.Round(annualRate * (daysInAdjustmentPeriod / daysInYear), 2);
        }

        private static decimal GetMultiplier(EndorsementType type, string jsonStr)
        {
            if (type == EndorsementType.AddMember || type == EndorsementType.RemoveMember)
                return 1.0m;

            if (type == EndorsementType.AddDependent || type == EndorsementType.RemoveDependent)
            {
                try
                {
                    using var doc = JsonDocument.Parse(jsonStr);
                    if (doc.RootElement.TryGetProperty("Relationship", out var relProp))
                    {
                        string relStr = "";
                        
                        if (relProp.ValueKind == JsonValueKind.String)
                        {
                            var s = relProp.GetString() ?? "";
                            if (int.TryParse(s, out int relInt))
                            {
                                relStr = ((RelationshipType)relInt).ToString().ToLower();
                            }
                            else
                            {
                                relStr = s.ToLower();
                            }
                        }
                        else if (relProp.ValueKind == JsonValueKind.Number)
                        {
                            relStr = ((RelationshipType)relProp.GetInt32()).ToString().ToLower();
                        }

                        if (relStr == "spouse")                                                return 0.8m;
                        if (relStr == "child")                                                 return 0.4m;
                        if (relStr is "father" or "mother" || relStr.Contains("parent"))       return 1.2m;
                    }
                }
                catch { /* malformed JSON — fall through to default */ }
            }

            return 1.0m;
        }
    }
}
