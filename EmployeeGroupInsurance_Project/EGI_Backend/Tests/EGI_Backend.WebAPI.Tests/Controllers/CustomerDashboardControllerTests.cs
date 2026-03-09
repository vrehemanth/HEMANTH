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
    public class CustomerDashboardControllerTests
    {
        private readonly Mock<ICustomerDashboardService> _mockDashboardService;
        private readonly Mock<ICorporateClientService> _mockClientService;
        private readonly Mock<IPolicyAssignmentService> _mockPolicyService;
        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly Mock<IClaimService> _mockClaimService;
        private readonly Mock<IPolicyEndorsementService> _mockEndorsementService;
        private readonly Mock<IInsurancePlanService> _mockPlanService;
        private readonly CustomerDashboardController _controller;

        public CustomerDashboardControllerTests()
        {
            _mockDashboardService = new Mock<ICustomerDashboardService>();
            _mockClientService = new Mock<ICorporateClientService>();
            _mockPolicyService = new Mock<IPolicyAssignmentService>();
            _mockInvoiceService = new Mock<IInvoiceService>();
            _mockClaimService = new Mock<IClaimService>();
            _mockEndorsementService = new Mock<IPolicyEndorsementService>();
            _mockPlanService = new Mock<IInsurancePlanService>();

            _controller = new CustomerDashboardController(
                _mockDashboardService.Object,
                _mockClientService.Object,
                _mockPolicyService.Object,
                _mockInvoiceService.Object,
                _mockClaimService.Object,
                _mockEndorsementService.Object,
                _mockPlanService.Object
            );
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
        public async Task GetProfile_UserIsAuthenticated_ReturnsOkWithProfileData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            SetupUser(userId);

            var expectedProfile = new CorporateClientResponseDto { Id = Guid.NewGuid(), CompanyName = "Acme Corp" };
            _mockDashboardService.Setup(s => s.GetProfileAsync(userId)).ReturnsAsync(expectedProfile);

            // Act
            var result = await _controller.GetProfile();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualProfile = Assert.IsType<CorporateClientResponseDto>(okResult.Value);
            Assert.Equal("Acme Corp", actualProfile.CompanyName);
        }

        [Fact]
        public async Task GetProfile_UserNotAuthenticated_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity()) } // No claims
            };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _controller.GetProfile());
        }

        [Fact]
        public async Task GetMyPolicies_UserHasPolicies_ReturnsOkWithPolicies()
        {
            // Arrange
            var userId = Guid.NewGuid();
            SetupUser(userId);

            var expectedPolicies = new List<PolicyAssignmentResponseDto> { new PolicyAssignmentResponseDto { PolicyNo = "POL-123" } };
            _mockDashboardService.Setup(s => s.GetMyPoliciesAsync(userId)).ReturnsAsync(expectedPolicies);

            // Act
            var result = await _controller.GetMyPolicies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var policies = Assert.IsType<List<PolicyAssignmentResponseDto>>(okResult.Value);
            Assert.Single(policies);
            Assert.Equal("POL-123", policies[0].PolicyNo);
        }

        [Fact]
        public async Task GetMyClaims_UserHasClaims_ReturnsOkWithClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            SetupUser(userId);

            var expectedClaims = new List<ClaimResponseDto> { new ClaimResponseDto { ClaimNumber = "CLM-999" } };
            _mockDashboardService.Setup(s => s.GetMyClaimsAsync(userId)).ReturnsAsync(expectedClaims);

            // Act
            var result = await _controller.GetMyClaims();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var claims = Assert.IsType<List<ClaimResponseDto>>(okResult.Value);
            Assert.Single(claims);
            Assert.Equal("CLM-999", claims[0].ClaimNumber);
        }

        [Fact]
        public async Task GetSummary_UserIsAuthenticated_ReturnsOkWithSummaryData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            SetupUser(userId);

            var expectedSummary = new CustomerDashboardSummaryDto { TotalPolicies = 2 };
            _mockDashboardService.Setup(s => s.GetSummaryAsync(userId)).ReturnsAsync(expectedSummary);

            // Act
            var result = await _controller.GetSummary();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var summary = Assert.IsType<CustomerDashboardSummaryDto>(okResult.Value);
            Assert.Equal(2, summary.TotalPolicies);
        }
    }
}
