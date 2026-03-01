using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IClaimRepository _claimRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IAuditLogRepository _auditRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly ICorporateClientService _corporateService;
        private readonly IMapper _mapper;

        public AdminDashboardService(
            IUserRepository userRepo,
            ICorporateClientRepository clientRepo,
            IClaimRepository claimRepo,
            IPolicyAssignmentRepository policyRepo,
            IAuditLogRepository auditRepo,
            IInvoiceRepository invoiceRepo,
            ICorporateClientService corporateService,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _claimRepo = claimRepo;
            _policyRepo = policyRepo;
            _auditRepo = auditRepo;
            _invoiceRepo = invoiceRepo;
            _corporateService = corporateService;
            _mapper = mapper;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            return new DashboardSummaryDto
            {
                AgentCount = await _userRepo.CountByRoleAsync(UserRole.Agent),
                CustomerCount = await _userRepo.CountByRoleAsync(UserRole.Customer),
                ClaimsOfficerCount = await _userRepo.CountByRoleAsync(UserRole.ClaimsOfficer),
                PendingClientsCount = await _clientRepo.CountPendingAsync(),
                TotalClaimsCount = await _claimRepo.CountAsync(),
                PendingClaimsCount = await _claimRepo.CountPendingAsync(),
                TotalPoliciesCount = await _policyRepo.CountAsync(),
                TotalRevenue = await _invoiceRepo.GetTotalRevenueAsync(),
                TotalPayouts = await _claimRepo.GetTotalPayoutsAsync(),
                TotalCommissionPayouts = await _policyRepo.GetTotalCommissionAsync()
            };
        }

        public async Task<List<CorporateClientResponseDto>> GetPendingClientsAsync()
        {
            var pending = await _clientRepo.GetPendingAsync();
            return _mapper.Map<List<CorporateClientResponseDto>>(pending);
        }

        public async Task<bool> ReviewClientAsync(Guid id, Guid adminId, ReviewCorporateClientDto dto)
        {
            await _corporateService.ReviewClientAsync(id, adminId, dto);
            return true;
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetAllPolicyAssignmentsAsync()
        {
            var assignments = await _policyRepo.GetAllWithDetailsAsync();
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(assignments);
        }

        public async Task<List<ClaimResponseDto>> GetAllClaimsAsync()
        {
            var claims = await _claimRepo.GetAllAsync();
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<UserResponseDto>> GetAllStaffAsync(string role)
        {
            if (!Enum.TryParse<UserRole>(role, true, out var userRole))
                return new List<UserResponseDto>();

            var staff = await _userRepo.GetAllByRoleAsync(userRole);
            var dtos = _mapper.Map<List<UserResponseDto>>(staff);

            if (userRole == UserRole.Agent)
            {
                foreach (var dto in dtos)
                {
                    dto.CommissionEarned = await _policyRepo.GetTotalCommissionForAgentAsync(dto.Id);
                }
            }

            return dtos;
        }

        public async Task<bool> ToggleUserStatusAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            user.Status = user.Status == UserStatus.Active ? UserStatus.Inactive : UserStatus.Active;
            await _userRepo.UpdateAsync(user);
            return true;
        }

        public async Task<List<CorporateClientResponseDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepo.GetAllAsync();
            return _mapper.Map<List<CorporateClientResponseDto>>(clients);
        }

        public async Task<List<AuditLogResponseDto>> GetAuditLogsAsync(string? userId = null, string? entityName = null)
        {
            var logs = await _auditRepo.GetFilteredAsync(userId, entityName);
            return _mapper.Map<List<AuditLogResponseDto>>(logs);
        }

        public async Task<int> RecalculateAllCommissionsAsync()
        {
            var policies = await _policyRepo.GetAllWithDetailsAsync();
            int updatedCount = 0;

            foreach (var policy in policies)
            {
                if (policy.CorporateClient != null)
                {
                    decimal commissionPercentage = policy.CorporateClient.BusinessCategory switch
                    {
                        BusinessCategory.Enterprise => 0.02m,
                        BusinessCategory.Large => 0.04m,
                        BusinessCategory.Medium => 0.06m,
                        BusinessCategory.Small => 0.08m,
                        _ => 0.08m
                    };

                    decimal newCommission = policy.AnnualPremium * commissionPercentage;
                    int years = policy.EndDate.Year - policy.StartDate.Year;
                    if (years <= 0) years = 1; // Fallback
                    decimal newTotalPremium = policy.AnnualPremium * years;
                    
                    if (policy.CommissionAmount != newCommission || policy.TotalPremium != newTotalPremium)
                    {
                        policy.CommissionAmount = newCommission;
                        policy.TotalPremium = newTotalPremium;
                        await _policyRepo.UpdateAsync(policy);
                        updatedCount++;
                    }
                }
            }

            return updatedCount;
        }
    }
}
