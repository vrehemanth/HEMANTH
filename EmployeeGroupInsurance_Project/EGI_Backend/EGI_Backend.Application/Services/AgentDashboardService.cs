using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class AgentDashboardService : IAgentDashboardService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public AgentDashboardService(
            IServiceScopeFactory scopeFactory,
            IMapper mapper,
            IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<AgentDashboardSummaryDto> GetSummaryAsync(Guid agentId)
        {
            string cacheKey = $"AgentDashboardSummary_{agentId}";
            if (_cache.TryGetValue(cacheKey, out AgentDashboardSummaryDto cached))
            {
                return cached;
            }

            using var scope = _scopeFactory.CreateScope();
            var agentCustRepo = scope.ServiceProvider.GetRequiredService<IAgentCustomerRepository>();
            var policyRepo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();
            var claimRepo = scope.ServiceProvider.GetRequiredService<IClaimRepository>();
            var invoiceRepo = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();

            var summary = new AgentDashboardSummaryDto
            {
                TotalCustomers = await agentCustRepo.GetCustomerCountForAgentAsync(agentId),
                ActivePolicies = await policyRepo.CountActiveForAgentAsync(agentId),
                TotalPremiumHandled = await policyRepo.GetTotalPremiumForAgentAsync(agentId),
                TotalCommissionEarned = await policyRepo.GetTotalCommissionForAgentAsync(agentId),
                PendingClaimsForCustomers = await claimRepo.CountPendingForAgentCustomersAsync(agentId),
                ProjectedCommission = (await policyRepo.GetTotalCommissionForAgentAsync(agentId)) * 1.12m
            };

            var customerIds = (await agentCustRepo.GetByAgentIdWithDetailsAsync(agentId)).Select(ac => ac.CorporateClientId).ToList();
            if (customerIds.Any())
            {
                var balances = await invoiceRepo.GetBalancesByClientsAsync(customerIds);
                summary.CustomerPendingPremiums = balances.Select(b => new CustomerPendingPremiumDto { CompanyName = "Client", Amount = b.Value }).ToList();
                summary.PendingPremium = balances.Values.Sum();
                summary.ClaimApprovalRate = await claimRepo.GetApprovalRateForClientsAsync(customerIds);
                var recentClaims = await claimRepo.GetTopClaimsForClientsAsync(customerIds, 5);
                summary.RecentClaims = recentClaims.Select(c => new AgentRecentClaimDto
                {
                    Id = c.Id,
                    MemberName = c.Member?.FullName ?? "Member",
                    Amount = c.ClaimAmount,
                    Status = c.Status.ToString(),
                    Date = c.ClaimDate,
                    Documents = new List<ClaimDocumentDto>()
                }).ToList();
            }

            var now = DateTime.UtcNow;
            var policies = await policyRepo.GetByAgentIdAsync(agentId);
            summary.PoliciesSoldThisMonth = policies.Count(p => p.CreatedAt.Month == now.Month && p.CreatedAt.Year == now.Year);

            _cache.Set(cacheKey, summary, TimeSpan.FromSeconds(5));
            return summary;
        }

        public async Task<List<CorporateClientResponseDto>> GetMyCustomersAsync(Guid agentId)
        {
            using var scope = _scopeFactory.CreateScope();
            var agentCustRepo = scope.ServiceProvider.GetRequiredService<IAgentCustomerRepository>();
            var agentCustomers = await agentCustRepo.GetByAgentIdWithDetailsAsync(agentId);
            var clients = agentCustomers.Where(ac => ac.CorporateClient != null).Select(ac => ac.CorporateClient!).ToList();
            return _mapper.Map<List<CorporateClientResponseDto>>(clients);
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid agentId)
        {
            using var scope = _scopeFactory.CreateScope();
            var policies = await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().GetByAgentIdAsync(agentId);
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(policies);
        }

        public async Task<List<AuditLogResponseDto>> GetCommissionLogsAsync(Guid agentId)
        {
            using var scope = _scopeFactory.CreateScope();
            var logs = await scope.ServiceProvider.GetRequiredService<IAuditLogRepository>().GetFilteredAsync(agentId.ToString(), "Invoice");
            var commissionLogs = logs.Where(l => l.Action == "CommissionCalculation").OrderByDescending(l => l.Timestamp).ToList();
            return _mapper.Map<List<AuditLogResponseDto>>(commissionLogs);
        }
    }
}
