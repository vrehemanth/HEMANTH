using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Services;
using EGI_Backend.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace EGI_Backend.Application.Tests.Services
{
    public class CustomerDashboardServiceTests
    {
        private readonly Mock<ICorporateClientRepository> _mockClientRepo;
        private readonly Mock<IPolicyAssignmentRepository> _mockPolicyRepo;
        private readonly Mock<IMemberRepository> _mockMemberRepo;
        private readonly Mock<IClaimRepository> _mockClaimRepo;
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepo;
        private readonly Mock<IPolicyEndorsementRepository> _mockEndorsementRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly CustomerDashboardService _service;

        public CustomerDashboardServiceTests()
        {
            _mockClientRepo = new Mock<ICorporateClientRepository>();
            _mockPolicyRepo = new Mock<IPolicyAssignmentRepository>();
            _mockMemberRepo = new Mock<IMemberRepository>();
            _mockClaimRepo = new Mock<IClaimRepository>();
            _mockInvoiceRepo = new Mock<IInvoiceRepository>();
            _mockEndorsementRepo = new Mock<IPolicyEndorsementRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();
            _mockCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(mockCacheEntry.Object);

            // Setup Mock Invoice Service
            _mockInvoiceService = new Mock<IInvoiceService>();

            // Mock Cache TryGetValue to always return false (miss)
            object cacheEntry = null;
            _mockCache.Setup(x => x.TryGetValue(It.IsAny<object>(), out cacheEntry)).Returns(false);

            _service = new CustomerDashboardService(
                _mockClientRepo.Object,
                _mockPolicyRepo.Object,
                _mockMemberRepo.Object,
                _mockClaimRepo.Object,
                _mockInvoiceRepo.Object,
                _mockEndorsementRepo.Object,
                _mockInvoiceService.Object,
                _mockMapper.Object,
                _mockCache.Object
            );
        }

        [Fact]
        public async Task GetProfileAsync_ClientExists_ReturnsProfileDto()
        {
            var userId = Guid.NewGuid();
            var client = new CorporateClient { Id = Guid.NewGuid(), UserId = userId };
            var dto = new CorporateClientResponseDto { Id = client.Id };

            _mockClientRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(client);
            _mockMapper.Setup(x => x.Map<CorporateClientResponseDto>(client)).Returns(dto);

            var result = await _service.GetProfileAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(client.Id, result.Id);
        }

        [Fact]
        public async Task GetProfileAsync_ClientDoesNotExist_ThrowsNotFoundException()
        {
            var userId = Guid.NewGuid();
            _mockClientRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync((CorporateClient)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetProfileAsync(userId));
        }

        [Fact]
        public async Task GetSummaryAsync_ValidUserId_ReturnsSummaryData()
        {
            var userId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var client = new CorporateClient { Id = clientId, UserId = userId };

            _mockClientRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(client);
            _mockPolicyRepo.Setup(x => x.CountByClientIdAsync(clientId)).ReturnsAsync(5);
            _mockMemberRepo.Setup(x => x.CountByClientIdAsync(clientId)).ReturnsAsync(20);

            var result = await _service.GetSummaryAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalPolicies);
            Assert.Equal(20, result.TotalMembers);
        }

        [Fact]
        public async Task GetMyPoliciesAsync_ValidUserId_ReturnsPolicyList()
        {
            var userId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var client = new CorporateClient { Id = clientId, UserId = userId };
            var policies = new List<PolicyAssignment> { new PolicyAssignment() };
            var dtos = new List<PolicyAssignmentResponseDto> { new PolicyAssignmentResponseDto() };

            _mockClientRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(client);
            _mockPolicyRepo.Setup(x => x.GetByClientIdAsync(clientId)).ReturnsAsync(policies);
            _mockMapper.Setup(x => x.Map<List<PolicyAssignmentResponseDto>>(policies)).Returns(dtos);

            var result = await _service.GetMyPoliciesAsync(userId);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetMyMembersAsync_ValidUserId_ReturnsMemberList()
        {
            var userId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var client = new CorporateClient { Id = clientId, UserId = userId };
            var members = new List<Member> { new Member() };
            var dtos = new List<MemberResponseDto> { new MemberResponseDto() };

            _mockClientRepo.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(client);
            _mockMemberRepo.Setup(x => x.GetByClientIdAsync(clientId)).ReturnsAsync(members);
            _mockMapper.Setup(x => x.Map<List<MemberResponseDto>>(members)).Returns(dtos);

            var result = await _service.GetMyMembersAsync(userId);

            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
