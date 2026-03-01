using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Services
{
    public class CustomerDashboardService : ICustomerDashboardService
    {
        private readonly ICorporateClientRepository _clientRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly IClaimRepository _claimRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;

        public CustomerDashboardService(
            ICorporateClientRepository clientRepo,
            IPolicyAssignmentRepository policyRepo,
            IMemberRepository memberRepo,
            IClaimRepository claimRepo,
            IInvoiceRepository invoiceRepo,
            IMapper mapper)
        {
            _clientRepo = clientRepo;
            _policyRepo = policyRepo;
            _memberRepo = memberRepo;
            _claimRepo = claimRepo;
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }

        private async Task<Guid> GetClientIdAsync(Guid userId)
        {
            var client = await _clientRepo.GetByUserIdAsync(userId);
            if (client == null) throw new NotFoundException("Corporate Client profile not found.");
            return client.Id;
        }

        public async Task<CustomerDashboardSummaryDto> GetSummaryAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
            return new CustomerDashboardSummaryDto
            {
                TotalPolicies = await _policyRepo.CountByClientIdAsync(clientId),
                TotalMembers = await _memberRepo.CountByClientIdAsync(clientId),
                PendingClaims = await _claimRepo.CountPendingForClientAsync(clientId),
                UnpaidInvoices = await _invoiceRepo.CountUnpaidByClientAsync(clientId),
                TotalPremiumDue = await _invoiceRepo.GetTotalBalanceByClientAsync(clientId)
            };
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid userId)
        {
            var clientId = await GetClientIdAsync(userId);
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
    }
}
