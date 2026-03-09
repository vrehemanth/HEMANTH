using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Services
{
    public class CustomerDashboardService : ICustomerDashboardService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CustomerDashboardService(
            IServiceScopeFactory scopeFactory,
            IMapper mapper,
            IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _cache = cache;
        }

        private async Task<Guid> GetClientIdAsync(Guid userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var clientRepo = scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>();
            var client = await clientRepo.GetByUserIdAsync(userId);
            if (client == null) throw new NotFoundException("Corporate Client profile not found.");
            return client.Id;
        }

        public async Task<CorporateClientResponseDto> GetProfileAsync(Guid userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var clientRepo = scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>();
            var client = await clientRepo.GetByUserIdAsync(userId);
            if (client == null) throw new NotFoundException("Profile not found.");
            return _mapper.Map<CorporateClientResponseDto>(client);
        }

        public async Task<CustomerDashboardSummaryDto> GetSummaryAsync(Guid userId)
        {
            string cacheKey = $"CustomerSummary_{userId}";
            if (_cache.TryGetValue(cacheKey, out CustomerDashboardSummaryDto cached))
            {
                return cached;
            }

            var clientId = await GetClientIdAsync(userId);

            // Parallel execution for dashboard metrics
            var policyTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().CountByClientIdAsync(clientId);
            });

            var memberTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IMemberRepository>().CountByClientIdAsync(clientId);
            });

            var claimTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountPendingForClientAsync(clientId);
            });

            var unpaidTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IInvoiceRepository>().CountUnpaidByClientAsync(clientId);
            });

            var balanceTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IInvoiceRepository>().GetTotalBalanceByClientAsync(clientId);
            });

            await Task.WhenAll(policyTask, memberTask, claimTask, unpaidTask, balanceTask);

            var summary = new CustomerDashboardSummaryDto
            {
                TotalPolicies = policyTask.Result,
                TotalMembers = memberTask.Result,
                PendingClaims = claimTask.Result,
                UnpaidInvoices = unpaidTask.Result,
                TotalPremiumDue = balanceTask.Result
            };

            _cache.Set(cacheKey, summary, TimeSpan.FromSeconds(5));
            return summary;
        }

        public async Task<CustomerDashboardOverviewDto> GetOverviewAsync(Guid userId)
        {
            string cacheKey = $"CustomerOverview_{userId}";
            if (_cache.TryGetValue(cacheKey, out CustomerDashboardOverviewDto cached))
            {
                return cached;
            }

            var clientId = await GetClientIdAsync(userId);

            // True Parallelism for overview data
            var summaryTask = GetSummaryAsync(userId);

            var policiesTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().GetByClientIdAsync(clientId);
            });

            var claimsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetByClientIdAsync(clientId);
            });

            var invoicesTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IInvoiceRepository>().GetByClientIdAsync(clientId);
            });

            var endorsementTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyEndorsementRepository>().GetByClientIdAsync(clientId);
            });

            await Task.WhenAll(summaryTask, policiesTask, claimsTask, invoicesTask, endorsementTask);

            var overview = new CustomerDashboardOverviewDto
            {
                Summary = summaryTask.Result,
                RecentPolicies = _mapper.Map<List<PolicyAssignmentResponseDto>>(policiesTask.Result.Take(5)),
                RecentClaims = _mapper.Map<List<ClaimResponseDto>>(claimsTask.Result.Take(5)),
                RecentInvoices = _mapper.Map<List<InvoiceResponseDto>>(invoicesTask.Result.Take(5)),
                RecentEndorsements = _mapper.Map<List<EndorsementResponseDto>>(endorsementTask.Result.Take(5))
            };

            _cache.Set(cacheKey, overview, TimeSpan.FromSeconds(5));
            return overview;
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();
            var policies = await repo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(policies);
        }

        public async Task<List<MemberResponseDto>> GetMyMembersAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IMemberRepository>();
            var members = await repo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<MemberResponseDto>>(members);
        }

        public async Task<List<ClaimResponseDto>> GetMyClaimsAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IClaimRepository>();
            var claims = await repo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<InvoiceResponseDto>> GetMyInvoicesAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
            var invoices = await repo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<InvoiceResponseDto>>(invoices);
        }

        public async Task<List<EndorsementResponseDto>> GetMyEndorsementsAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IPolicyEndorsementRepository>();
            var endorsements = await repo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<EndorsementResponseDto>>(endorsements);
        }
    }
}
