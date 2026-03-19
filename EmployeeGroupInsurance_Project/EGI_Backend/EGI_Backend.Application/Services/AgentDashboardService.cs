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

        public AgentDashboardService(
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public async Task<AgentDashboardSummaryDto> GetSummaryAsync(Guid agentId)
        {
            // Parallel Execution for Summary Metrics
            var countTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IAgentCustomerRepository>().GetCustomerCountForAgentAsync(agentId);
            });
            var activePolTask = Task.Run(async () => {
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
            var pendingClaimTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountPendingForAgentCustomersAsync(agentId);
            });

            await Task.WhenAll(countTask, activePolTask, premiumTask, commissionTask, pendingClaimTask);

            var summary = new AgentDashboardSummaryDto
            {
                TotalCustomers = countTask.Result,
                ActivePolicies = activePolTask.Result,
                TotalPremiumHandled = premiumTask.Result,
                TotalCommissionEarned = commissionTask.Result,
                PendingClaimsForCustomers = pendingClaimTask.Result,
                ProjectedCommission = commissionTask.Result * 1.12m
            };

            using var scope2 = _scopeFactory.CreateScope();
            var agentCustRepo = scope2.ServiceProvider.GetRequiredService<IAgentCustomerRepository>();
            var invoiceRepo = scope2.ServiceProvider.GetRequiredService<IInvoiceRepository>();
            var claimRepo = scope2.ServiceProvider.GetRequiredService<IClaimRepository>();
            var policyRepo = scope2.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();

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
                    Documents = c.Documents.Select(doc => new ClaimDocumentDto
                    {
                        Id = doc.Id,
                        DocumentType = doc.DocumentType.ToString(),
                        FileName = doc.FileName,
                        FilePath = doc.FilePath,
                        UploadedAt = doc.UploadedAt
                    }).ToList()
                }).ToList();
            }

            var now = DateTime.UtcNow;
            var policies = await policyRepo.GetByAgentIdAsync(agentId);
            summary.PoliciesSoldThisMonth = policies.Count(p => p.CreatedAt.Month == now.Month && p.CreatedAt.Year == now.Year);

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
