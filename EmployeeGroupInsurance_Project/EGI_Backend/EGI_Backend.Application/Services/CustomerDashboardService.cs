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
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class CustomerDashboardService : ICustomerDashboardService
    {
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly IClaimRepository _claimRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPolicyEndorsementRepository _endorsementRepo;
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CustomerDashboardService(
            ICorporateClientRepository clientRepo,
            IPolicyAssignmentRepository policyRepo,
            IMemberRepository memberRepo,
            IClaimRepository claimRepo,
            IInvoiceRepository invoiceRepo,
            IPolicyEndorsementRepository endorsementRepo,
            IInvoiceService invoiceService,
            IMapper mapper,
            IMemoryCache cache)
        {
            _clientRepo = clientRepo;
            _policyRepo = policyRepo;
            _memberRepo = memberRepo;
            _claimRepo = claimRepo;
            _invoiceRepo = invoiceRepo;
            _endorsementRepo = endorsementRepo;
            _invoiceService = invoiceService;
            _mapper = mapper;
            _cache = cache;
        }

        private async Task<Guid> GetClientIdAsync(Guid userId)
        {
            var client = await _clientRepo.GetByUserIdAsync(userId);
            if (client == null) throw new NotFoundException("Corporate Client profile not found.");
            return client.Id;
        }

        public async Task<CorporateClientResponseDto> GetProfileAsync(Guid userId)
        {
            var client = await _clientRepo.GetByUserIdAsync(userId);
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

            var summary = new CustomerDashboardSummaryDto
            {
                TotalPolicies = await _policyRepo.CountByClientIdAsync(clientId),
                TotalMembers = await _memberRepo.CountByClientIdAsync(clientId),
                PendingClaims = await _claimRepo.CountPendingForClientAsync(clientId),
                UnpaidInvoices = await _invoiceRepo.CountUnpaidByClientAsync(clientId),
                TotalPremiumDue = await _invoiceRepo.GetTotalBalanceByClientAsync(clientId)
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
            var summary = await GetSummaryAsync(userId);
            
            // One-time check for overdue invoices - only if we have unpaid ones
            if (summary.UnpaidInvoices > 0)
            {
                await _invoiceService.MarkOverdueInvoicesForClientAsync(clientId);
            }

            // Fetch only necessary data for dashboard sequentially because EF Core DbContext is not thread-safe
            var policies = await _policyRepo.GetByClientIdAsync(clientId);
            var claims = await _claimRepo.GetByClientIdAsync(clientId);
            var invoices = await _invoiceRepo.GetByClientIdAsync(clientId);
            var endorsements = await _endorsementRepo.GetByClientIdAsync(clientId);

            var overview = new CustomerDashboardOverviewDto
            {
                Summary = summary,
                RecentPolicies = _mapper.Map<List<PolicyAssignmentResponseDto>>(policies.OrderByDescending(p => p.StartDate).Take(5)),
                RecentClaims = _mapper.Map<List<ClaimResponseDto>>(claims.OrderByDescending(c => c.ClaimDate).Take(5)),
                RecentInvoices = _mapper.Map<List<InvoiceResponseDto>>(invoices.OrderByDescending(i => i.BillingPeriodFrom).Take(5)),
                RecentEndorsements = _mapper.Map<List<EndorsementResponseDto>>(endorsements.OrderByDescending(e => e.CreatedAt).Take(5))
            };

            _cache.Set(cacheKey, overview, TimeSpan.FromSeconds(5));
            return overview;
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            await _invoiceService.MarkOverdueInvoicesForClientAsync(clientId);
            var policies = await _policyRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(policies);
        }

        public async Task<List<MemberResponseDto>> GetMyMembersAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            var members = await _memberRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<MemberResponseDto>>(members);
        }

        public async Task<List<ClaimResponseDto>> GetMyClaimsAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            var claims = await _claimRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<ClaimResponseDto>>(claims);
        }

        public async Task<List<InvoiceResponseDto>> GetMyInvoicesAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            var invoices = await _invoiceRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<InvoiceResponseDto>>(invoices);
        }

        public async Task<List<EndorsementResponseDto>> GetMyEndorsementsAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            var endorsements = await _endorsementRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<EndorsementResponseDto>>(endorsements);
        }
    }
}
