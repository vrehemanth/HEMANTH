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
            string cacheKey = $"AgentDashboard_{agentId}";
            if (_cache.TryGetValue(cacheKey, out AgentDashboardSummaryDto cached))
            {
                return cached;
            }

            // Using parallel tasks with individual scopes for maximum speed and DbContext safety
            var custCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IAgentCustomerRepository>().GetCustomerCountForAgentAsync(agentId);
            });

            var activePoliciesTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().CountActiveForAgentAsync(agentId);
            });

            var premiumTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().GetTotalPremiumForAgentAsync(agentId);
            });

            var commissionTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().GetTotalCommissionForAgentAsync(agentId);
            });

            var pendingClaimsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountPendingForAgentCustomersAsync(agentId);
            });

            await Task.WhenAll(custCountTask, activePoliciesTask, premiumTask, commissionTask, pendingClaimsTask);

            var summary = new AgentDashboardSummaryDto
            {
                TotalCustomers = custCountTask.Result,
                ActivePolicies = activePoliciesTask.Result,
                TotalPremiumHandled = premiumTask.Result,
                TotalCommissionEarned = commissionTask.Result,
                PendingClaimsForCustomers = pendingClaimsTask.Result,
                ProjectedCommission = commissionTask.Result * 1.12m
            };

            // Get client IDs for list data
            List<Guid> customerIds;
            using (var scope = _scopeFactory.CreateScope())
            {
                var agentCustRepo = scope.ServiceProvider.GetRequiredService<IAgentCustomerRepository>();
                var agentCustomers = await agentCustRepo.GetByAgentIdWithDetailsAsync(agentId);
                customerIds = agentCustomers.Select(ac => ac.CorporateClientId).ToList();

                var invoiceRepo = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
                var balances = await invoiceRepo.GetBalancesByClientsAsync(customerIds);

                decimal totalPending = 0;
                summary.CustomerPendingPremiums = new List<CustomerPendingPremiumDto>();
                foreach (var ac in agentCustomers)
                {
                    if (balances.TryGetValue(ac.CorporateClientId, out var bal) && bal > 0)
                    {
                        summary.CustomerPendingPremiums.Add(new CustomerPendingPremiumDto
                        {
                            CompanyName = ac.CorporateClient?.CompanyName ?? "Unknown Client",
                            Amount = bal
                        });
                        totalPending += bal;
                    }
                }
                summary.PendingPremium = totalPending;
            }

            // Optimized Data: Fetch ONLY what's needed for the dashboard (Top 5 and Rate)
            if (customerIds.Any())
            {
                var rateTask = Task.Run(async () => {
                    using var scope = _scopeFactory.CreateScope();
                    return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetApprovalRateForClientsAsync(customerIds);
                });

                var recentClaimsTask = Task.Run(async () => {
                    using var scope = _scopeFactory.CreateScope();
                    return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetTopClaimsForClientsAsync(customerIds, 5);
                });

                await Task.WhenAll(rateTask, recentClaimsTask);

                summary.ClaimApprovalRate = rateTask.Result;
                summary.RecentClaims = recentClaimsTask.Result.Select(c => new AgentRecentClaimDto
                {
                    Id = c.Id,
                    MemberName = c.Member?.FullName ?? "Member",
                    Amount = c.ClaimAmount,
                    Status = c.Status.ToString(),
                    Date = c.ClaimDate,
                    Documents = c.Status == ClaimStatus.Approved 
                        ? _mapper.Map<List<ClaimDocumentDto>>(c.Documents) 
                        : new List<ClaimDocumentDto>()
                }).ToList();
            }

            using (var scope = _scopeFactory.CreateScope())
            {
                var policyRepo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();
                var now = DateTime.UtcNow;
                var policies = await policyRepo.GetByAgentIdAsync(agentId);
                summary.PoliciesSoldThisMonth = policies.Count(p => p.CreatedAt.Month == now.Month && p.CreatedAt.Year == now.Year);
            }

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
