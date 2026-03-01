using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class AgentDashboardService : IAgentDashboardService
    {
        private readonly IAgentCustomerRepository _agentCustRepo;
        private readonly IPolicyAssignmentRepository _policyRepo;
        private readonly IClaimRepository _claimRepo;
        private readonly IMapper _mapper;

        public AgentDashboardService(
            IAgentCustomerRepository agentCustRepo,
            IPolicyAssignmentRepository policyRepo,
            IClaimRepository claimRepo,
            IMapper mapper)
        {
            _agentCustRepo = agentCustRepo;
            _policyRepo = policyRepo;
            _claimRepo = claimRepo;
            _mapper = mapper;
        }

        public async Task<AgentDashboardSummaryDto> GetSummaryAsync(Guid agentId)
        {
            return new AgentDashboardSummaryDto
            {
                TotalCustomers = await _agentCustRepo.GetCustomerCountForAgentAsync(agentId),
                ActivePolicies = await _policyRepo.CountActiveForAgentAsync(agentId),
                TotalPremiumHandled = await _policyRepo.GetTotalPremiumForAgentAsync(agentId),
                TotalCommissionEarned = await _policyRepo.GetTotalCommissionForAgentAsync(agentId),
                PendingClaimsForCustomers = await _claimRepo.CountPendingForAgentCustomersAsync(agentId)
            };
        }

        public async Task<List<CorporateClientResponseDto>> GetMyCustomersAsync(Guid agentId)
        {
            var agentCustomers = await _agentCustRepo.GetByAgentIdWithDetailsAsync(agentId);
            var clients = new List<CorporateClient>();
            foreach (var ac in agentCustomers)
            {
                if (ac.CorporateClient != null)
                {
                    clients.Add(ac.CorporateClient);
                }
            }
            return _mapper.Map<List<CorporateClientResponseDto>>(clients);
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetMyPoliciesAsync(Guid agentId)
        {
            var policies = await _policyRepo.GetByAgentIdAsync(agentId);
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(policies);
        }
    }
}
