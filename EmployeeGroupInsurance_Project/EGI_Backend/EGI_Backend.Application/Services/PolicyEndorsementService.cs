using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
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

        public PolicyEndorsementService(
            IPolicyEndorsementRepository endorsementRepo,
            IPolicyAssignmentRepository policyRepo,
            IMemberRepository memberRepo,
            IDependentRepository dependentRepo,
            IInvoiceService invoiceService)
        {
            _endorsementRepo = endorsementRepo;
            _policyRepo = policyRepo;
            _memberRepo = memberRepo;
            _dependentRepo = dependentRepo;
            _invoiceService = invoiceService;
        }

        public async Task<EndorsementResponseDto> SubmitEndorsementAsync(Guid customerId, SubmitEndorsementDto dto)
        {
            var policy = await _policyRepo.GetByIdWithDetailsAsync(dto.PolicyAssignmentId);
            if (policy == null) throw new Exception("Policy not found.");

            // Flatten the EndorsementData to a string for DB storage
            string jsonData = JsonSerializer.Serialize(dto.EndorsementData);

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
                PremiumAdjustment = adjustment, // Show the calculated cost immediately
                RequestedByUserId = customerId,
                CreatedAt = DateTime.UtcNow
            };

            await _endorsementRepo.AddAsync(endorsement);

            return MapToResponseDto(endorsement);
        }

        public async Task<EndorsementResponseDto> ReviewEndorsementAsync(Guid agentId, Guid endorsementId, ReviewEndorsementDto dto)
        {
            var endorsement = await _endorsementRepo.GetByIdAsync(endorsementId);
            if (endorsement == null) throw new Exception("Endorsement not found.");

            if (endorsement.Status != EndorsementStatus.Pending)
                throw new Exception("Endorsement has already been processed.");

            var policy = await _policyRepo.GetByIdWithDetailsAsync(endorsement.PolicyAssignmentId);
            
            endorsement.Status = dto.Status;
            endorsement.ReviewedByUserId = agentId;
            endorsement.ReviewedAt = DateTime.UtcNow;

            if (dto.Status == EndorsementStatus.Approved)
            {
                // Apply the physical changes (Add member/dependent)
                await ApplyEndorsementChanges(policy, endorsement);

                // Use the FINAL adjustment amount (from DB or updated by agent)
                policy.AnnualPremium += endorsement.PremiumAdjustment;
                await _policyRepo.UpdateAsync(policy);

                // If they owe money, generate the prorated adjustment invoice for ONLY the difference
                if (endorsement.PremiumAdjustment > 0)
                {
                    await _invoiceService.GenerateAdjustmentInvoiceAsync(policy, endorsement.PremiumAdjustment);
                }
            }

            await _endorsementRepo.UpdateAsync(endorsement);
            return MapToResponseDto(endorsement);
        }

        public async Task<List<EndorsementResponseDto>> GetEndorsementsByPolicyAsync(Guid policyAssignmentId)
        {
            var endorsements = await _endorsementRepo.GetByPolicyIdAsync(policyAssignmentId);
            return endorsements.Select(MapToResponseDto).ToList();
        }

        public async Task<List<EndorsementResponseDto>> GetPendingEndorsementsAsync()
        {
            var endorsements = await _endorsementRepo.GetPendingAsync();
            return endorsements.Select(MapToResponseDto).ToList();
        }

        private EndorsementResponseDto MapToResponseDto(PolicyEndorsement e)
        {
            return new EndorsementResponseDto
            {
                Id = e.Id,
                PolicyAssignmentId = e.PolicyAssignmentId,
                Type = e.Type.ToString(),
                Description = e.Description,
                EndorsementData = e.EndorsementData,
                Status = e.Status.ToString(),
                PremiumAdjustment = e.PremiumAdjustment,
                CreatedAt = e.CreatedAt,
                ReviewedAt = e.ReviewedAt
            };
        }

        private async Task ApplyEndorsementChanges(PolicyAssignment policy, PolicyEndorsement e)
        {
            using var doc = JsonDocument.Parse(e.EndorsementData);
            var root = doc.RootElement;

            switch (e.Type)
            {
                case EndorsementType.AddMember:
                    string firstName = root.TryGetProperty("FirstName", out var fn) ? fn.GetString() ?? "" : "";
                    string lastName = root.TryGetProperty("LastName", out var ln) ? ln.GetString() ?? "" : "";
                    string fullName = root.TryGetProperty("FullName", out var fullN) ? fullN.GetString() ?? "" : $"{firstName} {lastName}".Trim();

                    var member = new Member
                    {
                        Id = Guid.NewGuid(),
                        PolicyAssignmentId = policy.Id,
                        EmployeeCode = root.TryGetProperty("EmployeeCode", out var ec) ? ec.GetString() ?? "" : "",
                        FullName = fullName,
                        Email = root.TryGetProperty("Email", out var em) ? em.GetString() ?? "" : "",
                        PhoneNo = root.TryGetProperty("PhoneNo", out var ph) ? ph.GetString() ?? "" : "",
                        DateOfBirth = DateTime.Parse(root.TryGetProperty("DateOfBirth", out var dob) ? dob.GetString() ?? DateTime.MinValue.ToString() : DateTime.MinValue.ToString()),
                        Gender = root.TryGetProperty("Gender", out var gen) ? ParseEnum<Gender>(gen) : Gender.Male,
                        SumInsured = GetHealthSumInsured(policy.InsurancePlan, CoveredGroup.EmployeeOnly),
                        Status = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _memberRepo.AddAsync(member);
                    break;

                case EndorsementType.AddDependent:
                    var memberIdString = root.GetProperty("MemberId").GetString() ?? throw new Exception("MemberId is required for AddDependent endorsement.");
                    var memberId = Guid.Parse(memberIdString);
                    var dependent = new Dependent
                    {
                        Id = Guid.NewGuid(),
                        MemberId = memberId,
                        FullName = root.GetProperty("FullName").GetString() ?? "",
                        Relationship = ParseEnum<RelationshipType>(root.GetProperty("Relationship")),
                        DateOfBirth = DateTime.Parse(root.GetProperty("DateOfBirth").GetString() ?? DateTime.MinValue.ToString()),
                        Gender = ParseEnum<Gender>(root.GetProperty("Gender")),
                        SumInsured = 0 
                    };
                    
                    // Set sum insured based on relationship - Looking specifically for Health Coverage (Type 0)
                    if (policy.InsurancePlan != null)
                    {
                        var targetGroup = MapToCoveredGroup(dependent.Relationship);
                        dependent.SumInsured = GetHealthSumInsured(policy.InsurancePlan, targetGroup);
                    }

                    await _dependentRepo.AddAsync(dependent);
                    break;

                case EndorsementType.RemoveMember:
                    var mId = Guid.Parse(root.GetProperty("MemberId").GetString() ?? throw new Exception("MemberId is required for RemoveMember endorsement."));
                    var m = await _memberRepo.GetByIdAsync(mId);
                    if (m != null)
                    {
                        m.Status = false;
                        await _memberRepo.UpdateAsync(m);
                    }
                    break;

                case EndorsementType.RemoveDependent:
                    // Since Dependent doesn't have status, we could delete it or mark it somehow (currently deleting is common for these)
                    // But in a real app you might add a Status column to Dependents. For now we will do nothing or throw.
                    throw new Exception("RemoveDependent is not fully implemented: Dependents currently lack a Status column.");
            }
        }

        private T ParseEnum<T>(JsonElement element) where T : struct, Enum
        {
            if (element.ValueKind == JsonValueKind.Number)
            {
                return (T)(object)element.GetInt32();
            }
            if (element.ValueKind == JsonValueKind.String)
            {
                if (Enum.TryParse<T>(element.GetString() ?? "", true, out T result))
                {
                    return result;
                }
            }
            return default(T);
        }

        private CoveredGroup MapToCoveredGroup(RelationshipType relationship)
        {
            return relationship switch
            {
                RelationshipType.Spouse => CoveredGroup.EmployeeAndFamily,
                RelationshipType.Child => CoveredGroup.EmployeeAndFamily,
                RelationshipType.Father => CoveredGroup.EmployeeFamilyAndParents,
                RelationshipType.Mother => CoveredGroup.EmployeeFamilyAndParents,
                _ => CoveredGroup.EmployeeOnly
            };
        }

        private decimal GetHealthSumInsured(InsurancePlan? plan, CoveredGroup targetGroup)
        {
            if (plan == null || plan.Coverages == null) return 0;

            // Priority logic for Health Sum Insured:
            // 1. Exact match for the target group (e.g. EmployeeOnly)
            // 2. Fallback to family tiers (if the plan is defined broadly)
            var coverage = plan.Coverages
                .Where(c => c.Type == CoverageType.Health && c.IsActive)
                .OrderByDescending(c => c.CoveredGroup == targetGroup) // Exact match first
                .ThenBy(c => (int)c.CoveredGroup) // Then pick the lowest available tier (EmployeeOnly -> Family -> Parents)
                .FirstOrDefault();

            // If we still haven't found a match specifically for the group, 
            // pick ANY available Health coverage as a last resort, 
            // since this policy is of type 'Health' and should have a value.
            if (coverage == null)
            {
                coverage = plan.Coverages
                    .Where(c => c.Type == CoverageType.Health && c.IsActive)
                    .OrderBy(c => (int)c.CoveredGroup)
                    .FirstOrDefault();
            }

            return coverage?.CoverageAmount ?? 0;
        }

        private decimal CalculateProratedAdjustment(PolicyAssignment policy, EndorsementType type, string jsonStr)
        {
            if (policy.InsurancePlan == null) return 0; // Guard clause

            decimal baseRate = policy.InsurancePlan.BasePremium;
            decimal multiplier = 1.0m;

            // Determine Multiplier based on Rule
            if (type == EndorsementType.AddMember || type == EndorsementType.RemoveMember)
            {
                multiplier = 1.0m; // Employee
            }
            else if (type == EndorsementType.AddDependent || type == EndorsementType.RemoveDependent)
            {
                try
                {
                    using var doc = JsonDocument.Parse(jsonStr);
                    if (doc.RootElement.TryGetProperty("Relationship", out var relProp))
                    {
                        string relString = string.Empty;
                        if (relProp.ValueKind == JsonValueKind.String)
                        {
                            relString = relProp.GetString()?.ToLower() ?? "";
                        }
                        else if (relProp.ValueKind == JsonValueKind.Number)
                        {
                            var relInt = relProp.GetInt32();
                            relString = ((RelationshipType)relInt).ToString().ToLower();
                        }

                        if (relString == "spouse") multiplier = 0.8m;
                        else if (relString == "child") multiplier = 0.4m;
                        else if (relString.Contains("parent") || relString == "father" || relString == "mother") multiplier = 1.2m;
                    }
                }
                catch
                {
                    // Fallback to 1.0 multiplier
                }
            }
            else
            {
                return 0; // No premium change for "Other"
            }

            decimal targetAnnualCost = baseRate * multiplier;

            // Calculate Days Remaining in the Policy Year
            var today = DateTime.UtcNow.Date;
            if (today >= policy.EndDate.Date) return 0; // Policy already expired

            var totalDaysInYear = (policy.EndDate.Date - policy.StartDate.Date).TotalDays;
            if (totalDaysInYear <= 0) totalDaysInYear = 365;

            var daysRemaining = (policy.EndDate.Date - today).TotalDays;
            
            // Decimal Math: Target Cost * (Days Left / Total Days)
            decimal proratedCost = targetAnnualCost * ((decimal)daysRemaining / (decimal)totalDaysInYear);

            // If it's a REMOVAL, it's a negative adjustment (refund)
            if (type == EndorsementType.RemoveMember || type == EndorsementType.RemoveDependent)
            {
                proratedCost = proratedCost * -1m;
            }

            return Math.Round(proratedCost, 2);
        }
    }
}
