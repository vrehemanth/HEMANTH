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
        private readonly IMemberRepository _memberRepo;
        private readonly IInvoiceService _invoiceService;
        private readonly INotificationService _notificationService;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PolicyAssignmentService(
            IPolicyAssignmentRepository policyAssignmentRepo,
            IInsurancePlanRepository planRepo,
            ICorporateClientRepository clientRepo,
            IAgentCustomerRepository agentCustomerRepo,
            IMemberRepository memberRepo,
            IInvoiceService invoiceService,
            IInvoiceRepository invoiceRepo,
            INotificationService notificationService,
            IUnitOfWork unitOfWork)
        {
            _policyAssignmentRepo = policyAssignmentRepo;
            _planRepo = planRepo;
            _clientRepo = clientRepo;
            _agentCustomerRepo = agentCustomerRepo;
            _memberRepo = memberRepo;
            _invoiceService = invoiceService;
            _invoiceRepo = invoiceRepo;
            _notificationService = notificationService;
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
            
            // Flaw 19 Fix: Blocked clients shouldn't be able to onboard new members
            if (client.IsBlocked) 
                throw new ForbiddenException("Your corporate account is currently blocked. Onboarding is disabled.");

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
            var memberRows = memberSheet.RowsUsed().Skip(1).ToList(); // skip header

            decimal industryMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetIndustryMultiplier(client.IndustryType);
            int rowIdx = 2; // Starting from row 2 (row 1 is header)
            foreach (var row in memberRows)
            {
                try
                {
                    var empCode = row.Cell(1).GetString().Trim();
                    if (string.IsNullOrEmpty(empCode)) continue;

                    var fullName = row.Cell(2).GetString().Trim();
                    if (string.IsNullOrEmpty(fullName)) throw new BadRequestException($"Row {rowIdx}: Full Name is required.");

                    DateTime dob;
                    if (!row.Cell(5).TryGetValue(out dob))
                    {
                        // Fallback: try manual parse if Excel date format is weird
                        if (!DateTime.TryParse(row.Cell(5).GetString(), out dob))
                            throw new BadRequestException($"Row {rowIdx}: Invalid Date of Birth format.");
                    }

                    // 4A.1 Logic Update: To allow one person to have multiple policies, 
                    // we create a NEW Member record for this specific policy, even if they already exist for the client.
                    if (membersMap.ContainsKey(empCode)) continue; 

                    decimal ageMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetAgeMultiplier(dob);
                    decimal rowEmployeePremium = basePremium * industryMultiplier * ageMultiplier;

                    var member = new Member
                    {
                        Id = Guid.NewGuid(),
                        EmployeeCode = empCode,
                        FullName = fullName,
                        Email = row.Cell(3).GetString().Trim(),
                        PhoneNo = row.Cell(4).GetString().Trim(),
                        DateOfBirth = dob,
                        Gender = Enum.TryParse<Gender>(row.Cell(6).GetString(), true, out var g) ? g : Gender.Male,
                        SumInsured = employeeSumInsured,
                        Status = true,
                        CorporateClientId = dto.CorporateClientId,
                        PolicyAssignmentId = policyAssignment.Id // Link to specific policy
                    };

                    membersMap.Add(empCode, member);
                    totalAnnualPremium += rowEmployeePremium;
                    policyAssignment.Members.Add(member);
                }
                catch (Exception ex) when (!(ex is BaseException))
                {
                    throw new BadRequestException($"Member Ingestion Failure at Row {rowIdx}: {ex.Message}");
                }
                rowIdx++;
            }

            // 4B. Parse Dependents
            if (dto.DependentsFile != null)
            {
                using var dependentStream = dto.DependentsFile.OpenReadStream();
                using var dependentWorkbook = new XLWorkbook(dependentStream);
                var dependentSheet = dependentWorkbook.Worksheet(1);
                var dependentRows = dependentSheet.RowsUsed().Skip(1).ToList();

                int depRowIdx = 2;
                foreach (var row in dependentRows)
                {
                    try
                    {
                        var empCode = row.Cell(1).GetString().Trim();
                        if (string.IsNullOrEmpty(empCode)) continue;
                        if (!membersMap.ContainsKey(empCode)) continue;

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

                        DateTime depDob;
                        if (!row.Cell(4).TryGetValue(out depDob))
                        {
                            if (!DateTime.TryParse(row.Cell(4).GetString(), out depDob))
                                throw new BadRequestException($"Dependent Row {depRowIdx}: Invalid Date of Birth format.");
                        }

                        var dependent = new Dependent
                        {
                            Id = Guid.NewGuid(),
                            MemberId = membersMap[empCode].Id,
                            FullName = row.Cell(2).GetString().Trim(),
                            Relationship = relationship,
                            DateOfBirth = depDob,
                            Gender = Enum.TryParse<Gender>(row.Cell(5).GetString(), true, out var dg) ? dg : Gender.Male,
                            SumInsured = dependentSumInsured
                        };

                        membersMap[empCode].Dependents.Add(dependent);

                        // Unified Age-Based Multiplier for Dependents
                        decimal depAgeMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetAgeMultiplier(depDob);
                        totalAnnualPremium += basePremium * depAgeMultiplier;
                    }
                    catch (Exception ex) when (!(ex is BaseException))
                    {
                        throw new BadRequestException($"Dependent Ingestion Failure at Row {depRowIdx}: {ex.Message}");
                    }
                    depRowIdx++;
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

            // 8. Notify Corporate Client and Agent
            await _notificationService.CreateNotificationAsync(client.UserId, "Policy Created", $"Your policy {policyAssignment.PolicyNo} is now active. {membersMap.Count} members onboarded.", "Success");
            await _notificationService.CreateNotificationAsync(agentAssignment.AgentId, "New Policy Assigned", $"A new policy {policyAssignment.PolicyNo} for {client.CompanyName} has been assigned to you.", "Info");

            return $"Successfully processed {membersMap.Count} members and generated Policy No: {policyAssignment.PolicyNo}. Total Annual Premium: ₹{totalAnnualPremium}";
        }

        public async Task<RenewalQuoteResponseDto> GetRenewalQuoteAsync(Guid policyId, Guid corporateClientUserId, int years, BillingFrequency frequency)
        {
            var policy = await _policyAssignmentRepo.GetByIdWithDetailsAsync(policyId);
            if (policy == null) throw new NotFoundException("Policy not found.");

            if (years < 1 || years > 5) throw new BadRequestException("Renewal term must be between 1 and 5 years.");
            
            if (policy.Status == PolicyStatus.Expired)
            {
                throw new BadRequestException("This policy has expired and can no longer be renewed.");
            }

            if (policy.Status == PolicyStatus.Inactive && DateTime.UtcNow <= policy.EndDate)
            {
                throw new BadRequestException("Renewals are currently disabled for this policy by an administrator.");
            }

            if (policy.InsurancePlan != null && !policy.InsurancePlan.Status)
            {
                throw new BadRequestException("This insurance plan has been deactivated. Renewals are not available for this product.");
            }

            var (newAnnualRate, totalForTerm) = CalculateRenewalPremiums(policy, frequency, years);

            return new RenewalQuoteResponseDto
            {
                NewAnnualRate = newAnnualRate,
                NewMonthlyPremium = newAnnualRate / 12m,
                NewTotalPremium = totalForTerm,
                Years = years,
                SelectedFrequency = frequency,
                Note = $"Premium calculated based on current Plan Rate ({policy.InsurancePlan?.PlanName}) and Membership ({policy.Members?.Count ?? 0})."
            };
        }

        private (decimal Annual, decimal Total) CalculateRenewalPremiums(PolicyAssignment policy, BillingFrequency frequency, int years)
        {
            decimal currentBasePremium = policy.InsurancePlan?.BasePremium ?? 0m;
            decimal calculatedAnnual = 0m;

            if (policy.Members == null || !policy.Members.Any())
            {
                calculatedAnnual = policy.AnnualPremium;
            }
            else
            {
                decimal industryMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetIndustryMultiplier(policy.CorporateClient?.IndustryType ?? IndustryType.Others);
                foreach (var member in policy.Members)
                {
                    decimal ageMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetAgeMultiplier(member.DateOfBirth);
                    calculatedAnnual += currentBasePremium * industryMultiplier * ageMultiplier;

                    foreach (var dep in member.Dependents)
                    {
                        decimal depAgeMultiplier = EGI_Backend.Domain.Constants.BusinessRules.GetAgeMultiplier(dep.DateOfBirth);
                        calculatedAnnual += currentBasePremium * depAgeMultiplier;
                    }
                }
            }

            // Ensure we don't return 0
            if (calculatedAnnual <= 0) 
            {
                calculatedAnnual = policy.AnnualPremium > 0 ? policy.AnnualPremium : 1000m; 
            }

            decimal totalForTerm = calculatedAnnual * years;
            if (frequency == BillingFrequency.Annual) totalForTerm *= 0.95m;

            return (calculatedAnnual, totalForTerm);
        }

        public async Task<string> RenewPolicyAsync(Guid policyId, Guid corporateClientUserId, int years, BillingFrequency frequency)
        {
            var policy = await _policyAssignmentRepo.GetByIdWithDetailsAsync(policyId);
            if (policy == null) throw new NotFoundException("Policy not found.");

            if (years < 1 || years > 5) throw new BadRequestException("Renewal term must be between 1 and 5 years.");

            var client = await _clientRepo.GetByUserIdAsync(corporateClientUserId);
            if (client == null || policy.CorporateClientId != client.Id)
                throw new ForbiddenException("You are not authorized to renew this policy.");

            // Flaw 19 Fix: Blocked clients cannot renew policies
            if (client.IsBlocked)
                throw new ForbiddenException("Renewal Failed: Your corporate account is currently blocked.");
            
            if (policy.Status == PolicyStatus.Expired)
            {
                throw new BadRequestException("Renewal Failed: This policy has already expired beyond its grace period.");
            }

            if (policy.Status == PolicyStatus.Inactive && DateTime.UtcNow <= policy.EndDate)
            {
                throw new BadRequestException("Renewal Failed: This policy has been deactivated by an administrator.");
            }

            if (policy.InsurancePlan != null && !policy.InsurancePlan.Status)
            {
                throw new BadRequestException("Renewal Failed: The associated insurance plan has been deactivated by an administrator.");
            }

            // Flaw 14 Fix: Block renewal if there is significant outstanding debt
            var totalOverdue = await _invoiceRepo.GetTotalBalanceByClientAsync(client.Id);
            if (totalOverdue > 1000m) // Allow minor penny discrepancies but block major debt
            {
                throw new BadRequestException($"Renewal Denied: Your account has an outstanding balance of ₹{totalOverdue:N2}. All overdue invoices must be settled before renewal.");
            }

            // Check if within renewal window
            var now = DateTime.UtcNow;
            if (now < policy.EndDate.AddDays(-30) || now > policy.EndDate.AddDays(30))
            {
                throw new BadRequestException("Policy is not within its renewal window (30 days before/after expiry).");
            }

            // Extend the policy
            policy.EndDate = policy.EndDate.AddYears(years);
            policy.Status = PolicyStatus.Active;

            // Reset Sum Insured
            decimal employeeSumInsured = policy.InsurancePlan.Coverages.Sum(c => c.CoverageAmount);
            foreach (var member in policy.Members)
            {
                member.SumInsured = employeeSumInsured;
                member.Status = true;
                foreach (var dep in member.Dependents)
                {
                    decimal dependentSumInsured = 0m;
                    foreach (var coverage in policy.InsurancePlan.Coverages)
                    {
                        if (dep.Relationship == RelationshipType.Spouse || dep.Relationship == RelationshipType.Child)
                        {
                            if (coverage.CoveredGroup == CoveredGroup.EmployeeAndFamily || coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents)
                                dependentSumInsured += coverage.CoverageAmount;
                        }
                        else if (dep.Relationship == RelationshipType.Father || dep.Relationship == RelationshipType.Mother)
                        {
                            if (coverage.CoveredGroup == CoveredGroup.EmployeeFamilyAndParents)
                                dependentSumInsured += coverage.CoverageAmount;
                        }
                    }
                    dep.SumInsured = dependentSumInsured;
                    dep.IsActive = true;
                }
            }

            var (newAnnual, finalTotalPremium) = CalculateRenewalPremiums(policy, frequency, years);
            
            // Flaw 20 Fix: Re-evaluate Business Category on Renewal
            // Ensure commission rates reflect current company size for the new term
            int activeHeadcount = policy.Members.Count(m => m.Status) + 
                                 policy.Members.Where(m => m.Status)
                                               .Sum(m => m.Dependents.Count(d => d.IsActive));
            
            policy.BusinessCategory = EGI_Backend.Domain.Constants.BusinessRules.GetCategoryByHeadcount(activeHeadcount);

            // Update Billing Frequency if changed
            policy.BillingFrequency = frequency;

            // Updated Premiums
            policy.AnnualPremium = newAnnual;
            policy.TotalPremium = finalTotalPremium;

            // FIXED: Double Commission Bug. 
            // We RESET this to 0 and let InvoiceService accumulate it based on actual payments
            // to ensure consistency and correct business-tier rates.
            policy.CommissionAmount = 0m; 

            await _unitOfWork.SaveChangesAsync();

            // Explicitly trigger invoice generation for the new term
            await _invoiceService.GenerateDueInvoicesAsync();

            await _notificationService.CreateNotificationAsync(corporateClientUserId, "Policy Renewed", $"Your policy {policy.PolicyNo} has been successfully renewed until {policy.EndDate:dd MMM yyyy}.", "Success");

            return $"Policy {policy.PolicyNo} has been renewed successfully.";
        }

        public async Task UpdatePolicyStatusesAsync(Guid clientId)
        {
            var policies = await _policyAssignmentRepo.GetByClientIdAsync(clientId);
            var now = DateTime.UtcNow;
            bool hasChanges = false;

            foreach (var policy in policies)
            {
                var originalStatus = policy.Status;

                if (now > policy.EndDate.AddDays(30))
                {
                    // Past grace period -> Dead
                    policy.Status = PolicyStatus.Expired;
                }
                else if (now > policy.EndDate)
                {
                    // Past end date but within grace -> Inactive (Grace)
                    // Note: We only auto-set to Inactive if it wasn't already Expired
                    if (policy.Status != PolicyStatus.Expired)
                    {
                        policy.Status = PolicyStatus.Inactive;
                    }
                }
                // CRITICAL FIX: Removed the automatic flip back to 'Active'. 
                // If an Admin manually sets a policy to 'Inactive' or 'Expired' before the EndDate, 
                // it should STAY that way. The system only moves policies FORWARD in the lifecycle (Active -> Inactive -> Expired).

                if (policy.Status != originalStatus)
                {
                    hasChanges = true;
                    // Notify Client of Status change
                    try
                    {
                        var client = await _clientRepo.GetByIdAsync(policy.CorporateClientId);
                        if (client != null)
                        {
                            await _notificationService.CreateNotificationAsync(client.UserId, "Policy Status Update", $"Your policy {policy.PolicyNo} status has changed to {policy.Status}.", "Info");
                        }
                    }
                    catch { }
                }
            }

            if (hasChanges)
            {
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> TogglePolicyStatusAsync(Guid policyId)
        {
            var policy = await _policyAssignmentRepo.GetByIdAsync(policyId);
            if (policy == null) return false;

            // Simple toggle between Active and Inactive
            // If it's Expired, toggling it "on" makes it Active (if within grace period)
            if (policy.Status == PolicyStatus.Active)
            {
                policy.Status = PolicyStatus.Inactive;
            }
            else
            {
                // Safety: If an Admin tries to activate a policy whose grace period is over, 
                // flip it to Expired instead of allowing it to be Active.
                if (DateTime.UtcNow > policy.EndDate.AddDays(30))
                {
                    policy.Status = PolicyStatus.Expired;
                }
                else
                {
                    policy.Status = PolicyStatus.Active;
                }
            }

            // Sync with DB: Use UnitOfWork instead of Repo.Update to avoid marking the entire 
            // Member/Dependent graph as modified, which causes 500 errors on large policies.
            await _unitOfWork.SaveChangesAsync();
            
            // Notify Client
            try
            {
                var client = await _clientRepo.GetByIdAsync(policy.CorporateClientId);
                if (client != null)
                {
                    await _notificationService.CreateNotificationAsync(client.UserId, "Policy Status Toggled", $"Your policy {policy.PolicyNo} has been set to {policy.Status} by an administrator.", "Info");
                }
            }
            catch { }
            
            return true;
        }
    }
}
