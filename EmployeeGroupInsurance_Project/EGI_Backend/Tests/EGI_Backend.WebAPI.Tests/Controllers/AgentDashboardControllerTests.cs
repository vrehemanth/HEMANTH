using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EGI_Backend.WebAPI.Tests.Controllers
{
    public class AgentDashboardControllerTests
    {
        private readonly Mock<IAgentDashboardService> _mockDashboardSvc = new();
        private readonly Mock<IAgentCustomerService> _mockCustSvc = new();
        private readonly Mock<IPolicyAssignmentService> _mockPolicySvc = new();
        private readonly Mock<IInsurancePlanService> _mockPlanSvc = new();
        private readonly Mock<IClaimService> _mockClaimSvc = new();
        private readonly Mock<IInvoiceService> _mockInvoiceSvc = new();
        private readonly Mock<IPolicyEndorsementService> _mockEndorseSvc = new();
        private readonly AgentDashboardController _controller;

        public AgentDashboardControllerTests()
        {
            _controller = new AgentDashboardController(
                _mockDashboardSvc.Object, _mockCustSvc.Object, _mockPolicySvc.Object,
                _mockPlanSvc.Object, _mockClaimSvc.Object, _mockInvoiceSvc.Object,
                _mockEndorseSvc.Object);
        }

        private void SetupUser(Guid userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GetPendingEndorsements_ValidRequest_ReturnsOkWithList()
        {
            var agentId = Guid.NewGuid();
            SetupUser(agentId);
            var expectedList = new List<EndorsementResponseDto> { new EndorsementResponseDto() };
            _mockEndorseSvc.Setup(x => x.GetPendingEndorsementsAsync(It.IsAny<Guid>(), "Agent")).ReturnsAsync(expectedList);

            var result = await _controller.GetPendingEndorsements();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<EndorsementResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task GetEndorsementsByPolicy_ValidId_ReturnsOkWithList()
        {
            var agentId = Guid.NewGuid();
            SetupUser(agentId);
            var policyId = Guid.NewGuid();
            var expectedList = new List<EndorsementResponseDto> { new EndorsementResponseDto() };
            _mockEndorseSvc.Setup(x => x.GetEndorsementsByPolicyAsync(policyId, It.IsAny<Guid>(), "Agent")).ReturnsAsync(expectedList);

            var result = await _controller.GetEndorsementsByPolicy(policyId);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<EndorsementResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task ReviewEndorsement_UserAuthenticated_ReturnsOk()
        {
            var agentId = Guid.NewGuid();
            var endorsementId = Guid.NewGuid();
            SetupUser(agentId);
            var dto = new ReviewEndorsementDto();
            var response = new EndorsementResponseDto();
            _mockEndorseSvc.Setup(x => x.ReviewEndorsementAsync(agentId, "Agent", endorsementId, dto)).ReturnsAsync(response);

            var result = await _controller.ReviewEndorsement(endorsementId, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<EndorsementResponseDto>(ok.Value);
        }

        [Fact]
        public async Task CreateCustomer_UserAuthenticated_ReturnsOk()
        {
            var agentId = Guid.NewGuid();
            SetupUser(agentId);
            var dto = new AgentCreateCustomerDto();
            _mockCustSvc.Setup(x => x.CreateCustomerByAgentAsync(agentId, dto)).Returns(Task.CompletedTask);

            var result = await _controller.CreateCustomer(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ReviewEndorsement_UserUnauthenticated_ThrowsUnauthorizedAccessException()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity()) }
            };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _controller.ReviewEndorsement(Guid.NewGuid(), new ReviewEndorsementDto()));
        }
    }
}
