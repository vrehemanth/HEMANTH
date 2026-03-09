using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class PolicyAssignmentService : IPolicyAssignmentService
    {
        private readonly IPolicyAssignmentRepository _policyAssignmentRepo;
        private readonly IInsurancePlanRepository _planRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IAgentCustomerRepository _agentCustomerRepo;
        private readonly IInvoiceService _invoiceService;
        private readonly IUnitOfWork _unitOfWork;

        public PolicyAssignmentService(
            IPolicyAssignmentRepository policyAssignmentRepo,
            IInsurancePlanRepository planRepo,
            ICorporateClientRepository clientRepo,
            IAgentCustomerRepository agentCustomerRepo,
            IInvoiceService invoiceService,
            IUnitOfWork unitOfWork)
        {
            _policyAssignmentRepo = policyAssignmentRepo;
            _planRepo = planRepo;
            _clientRepo = clientRepo;
            _agentCustomerRepo = agentCustomerRepo;
            _invoiceService = invoiceService;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ProcessMembersExcelAsync(UploadMembersDto dto)
        {
            // 1. Validations
            var plan = await _planRepo.GetByIdAsync(dto.InsurancePlanId);
            if (plan == null) throw new NotFoundException("Insurance Plan not found.");
            if (!plan.Status) throw new BadRequestException("This insurance plan is currently inactive and cannot be purchased.");

            var client = await _clientRepo.GetByIdAsync(dto.CorporateClientId);
            if (client == null) throw new NotFoundException("Corporate Client not found.");

            var agentAssignment = await _agentCustomerRepo.GetByCorporateClientIdAsync(dto.CorporateClientId);
            if (agentAssignment == null) throw new NotFoundException("No agent assigned to this corporate client.");

            // 2. Compute base values
            decimal basePremium = plan.BasePremium;
            decimal employeeSumInsured = plan.Coverages.Sum(c => c.CoverageAmount);

            // 3. Setup PolicyAssignment
            var policyAssignment = new PolicyAssignment
            {
                Id = Guid.NewGuid(),
                PolicyNo = $"POL-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}",
                CorporateClientId = dto.CorporateClientId,
                InsurancePlanId = dto.InsurancePlanId,
                AgentId = agentAssignment.AgentId,
                StartDate = dto.StartDate,
                EndDate = dto.StartDate.AddYears(dto.DurationInYears),
                Status = PolicyStatus.Active,
                BillingFrequency = dto.BillingFrequency,
                AnnualPremium = 0m // Will accumulate
            };

            var membersMap = new Dictionary<string, Member>();
            decimal totalAnnualPremium = 0m;

            // 4. Parse Excel
            using var memberStream = dto.MembersFile.OpenReadStream();
            using var memberWorkbook = new XLWorkbook(memberStream);

            // 4A. Parse Members (Sheet 1)
            var memberSheet = memberWorkbook.Worksheet(1);
            var memberRows = memberSheet.RowsUsed().Skip(1); // skip header
            foreach (var row in memberRows)
            {
                var empCode = row.Cell(1).GetString().Trim();
                if (string.IsNullOrEmpty(empCode)) continue;

                var member = new Member
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode = empCode,
                    FullName = row.Cell(2).GetString().Trim(),
                    Email = row.Cell(3).GetString().Trim(),
                    PhoneNo = row.Cell(4).GetString().Trim(),
                    DateOfBirth = row.Cell(5).GetDateTime(),
                    Gender = Enum.TryParse<Gender>(row.Cell(6).GetString(), true, out var g) ? g : Gender.Male,
                    SumInsured = employeeSumInsured,
                    Status = true
                };

                membersMap.Add(empCode, member);
                totalAnnualPremium += basePremium; // 1.0x multiplier
                policyAssignment.Members.Add(member);
            }

            // 4B. Parse Dependents
            if (dto.DependentsFile != null)
            {
                using var dependentStream = dto.DependentsFile.OpenReadStream();
                using var dependentWorkbook = new XLWorkbook(dependentStream);
                var dependentSheet = dependentWorkbook.Worksheet(1);
                var dependentRows = dependentSheet.RowsUsed().Skip(1);
                foreach (var row in dependentRows)
                {
                    var empCode = row.Cell(1).GetString().Trim();
                    if (string.IsNullOrEmpty(empCode) || !membersMap.ContainsKey(empCode)) continue;

                    var relationshipStr = row.Cell(3).GetString().Replace(" ", "");
                    if (!Enum.TryParse<RelationshipType>(relationshipStr, true, out var relationship))
                        relationship = RelationshipType.Other;

                    decimal dependentSumInsured = 0m;
                    foreach (var coverage in plan.Coverages)
                    {
                        if (relationship == RelationshipType.Spouse || relationship == RelationshipType.Child)
                        {
                            if (coverage.CoveredGroup == CoveredGroup.EmployeeAndFamily || coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents)
                                dependentSumInsured += coverage.CoverageAmount;
                        }
                        else if (relationship == RelationshipType.Father || relationship == RelationshipType.Mother)
                        {
                            if (coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents)
                                dependentSumInsured += coverage.CoverageAmount;
                        }
                    }

                    var dependent = new Dependent
                    {
                        Id = Guid.NewGuid(),
                        MemberId = membersMap[empCode].Id,
                        FullName = row.Cell(2).GetString().Trim(),
                        Relationship = relationship,
                        DateOfBirth = row.Cell(4).GetDateTime(),
                        Gender = Enum.TryParse<Gender>(row.Cell(5).GetString(), true, out var dg) ? dg : Gender.Male,
                        SumInsured = dependentSumInsured
                    };

                    membersMap[empCode].Dependents.Add(dependent);

                    // Multipliers
                    if (relationship == RelationshipType.Spouse) totalAnnualPremium += basePremium * 0.8m;
                    else if (relationship == RelationshipType.Father || relationship == RelationshipType.Mother) totalAnnualPremium += basePremium * 1.2m;
                    else totalAnnualPremium += basePremium * 0.5m; // Children or others
                }
            }

            // 5. Apply Discounts based on Billing Frequency
            if (dto.BillingFrequency == BillingFrequency.Annual)
            {
                // Give a 5% discount (Total * 0.95)
                totalAnnualPremium = totalAnnualPremium * 0.95m;
            }

            // 6. AUTO-CATEGORIZATION: Determine Category based on THIS POLICY size only
            int currentPolicyHeadcount = membersMap.Count + (dto.DependentsFile != null ? membersMap.Values.Sum(m => m.Dependents.Count) : 0);
            
            policyAssignment.BusinessCategory = EGI_Backend.Domain.Constants.BusinessRules.GetCategoryByHeadcount(currentPolicyHeadcount);

            // 7. Finalize and Save
            policyAssignment.AnnualPremium = totalAnnualPremium;
            policyAssignment.TotalPremium = totalAnnualPremium * dto.DurationInYears;
            policyAssignment.CommissionAmount = 0m; // Initial commission is 0 until payment is made

            await _policyAssignmentRepo.AddAsync(policyAssignment);
            await _unitOfWork.SaveChangesAsync();

            // 7. Auto-generate first invoice immediately
            await _invoiceService.GenerateFirstInvoiceAsync(policyAssignment);

            return $"Successfully processed {membersMap.Count} members and generated Policy No: {policyAssignment.PolicyNo}. Total Annual Premium: ₹{totalAnnualPremium}";
        }
    }
}
