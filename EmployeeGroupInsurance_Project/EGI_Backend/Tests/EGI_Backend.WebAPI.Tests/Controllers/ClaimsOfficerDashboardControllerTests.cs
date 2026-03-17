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
    public class ClaimsOfficerDashboardControllerTests
    {
        private readonly Mock<IClaimsOfficerDashboardService> _mockDashboardSvc = new();
        private readonly Mock<IClaimService> _mockClaimSvc = new();
        private readonly Mock<IInsurancePlanService> _mockPlanSvc = new();
        private readonly Mock<IInvoiceService> _mockInvoiceSvc = new();
        private readonly ClaimsOfficerDashboardController _controller;

        public ClaimsOfficerDashboardControllerTests()
        {
            _controller = new ClaimsOfficerDashboardController(
                _mockDashboardSvc.Object, _mockClaimSvc.Object, _mockPlanSvc.Object, _mockInvoiceSvc.Object);
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
        public async Task GetInvoicesByPolicy_ValidId_ReturnsOkWithList()
        {
            var officerId = Guid.NewGuid();
            SetupUser(officerId);
            var policyId = Guid.NewGuid();
            var expectedList = new List<InvoiceResponseDto> { new InvoiceResponseDto() };
            _mockInvoiceSvc.Setup(x => x.GetInvoicesByPolicyAsync(policyId, It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(expectedList);

            var result = await _controller.GetInvoicesByPolicy(policyId);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<InvoiceResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task GetClaimsByPolicy_ValidId_ReturnsOkWithList()
        {
            var officerId = Guid.NewGuid();
            SetupUser(officerId);
            var policyId = Guid.NewGuid();
            var expectedList = new List<ClaimResponseDto> { new ClaimResponseDto() };
            _mockClaimSvc.Setup(x => x.GetClaimsByPolicyAsync(policyId, It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(expectedList);

            var result = await _controller.GetClaimsByPolicy(policyId);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ClaimResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task GetSummary_ValidRequest_ReturnsOkWithSummary()
        {
            var officerId = Guid.NewGuid();
            SetupUser(officerId);
            var summary = new ClaimsOfficerDashboardSummaryDto { TotalPendingClaimsInSystem = 10 };
            _mockDashboardSvc.Setup(x => x.GetSummaryAsync(officerId)).ReturnsAsync(summary);

            var result = await _controller.GetSummary();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<ClaimsOfficerDashboardSummaryDto>(ok.Value);
        }

        [Fact]
        public async Task GetPendingClaims_ValidRequest_ReturnsOkWithList()
        {
            var expectedList = new List<ClaimResponseDto> { new ClaimResponseDto() };
            _mockClaimSvc.Setup(x => x.GetPendingClaimsAsync()).ReturnsAsync(expectedList);

            var result = await _controller.GetPendingClaims();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<ClaimResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task ReviewClaim_ValidRequest_ReturnsOk()
        {
            var officerId = Guid.NewGuid();
            SetupUser(officerId);
            var claimId = Guid.NewGuid();
            var dto = new ReviewClaimDto { IsApproved = true };
            _mockClaimSvc.Setup(x => x.ReviewClaimAsync(officerId, claimId, dto)).Returns(Task.CompletedTask);

            var result = await _controller.ReviewClaim(claimId, dto);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
