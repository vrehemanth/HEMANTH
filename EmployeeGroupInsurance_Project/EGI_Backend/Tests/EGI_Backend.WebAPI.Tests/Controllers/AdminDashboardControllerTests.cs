using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EGI_Backend.WebAPI.Tests.Controllers
{
    public class AdminDashboardControllerTests
    {
        private readonly Mock<IAdminDashboardService> _dashboardSvc = new();
        private readonly Mock<IInsurancePlanService> _planSvc = new();
        private readonly Mock<ICorporateClientService> _clientSvc = new();
        private readonly Mock<IClaimService> _claimSvc = new();
        private readonly Mock<IInvoiceService> _invoiceSvc = new();
        private readonly Mock<IPolicyEndorsementService> _endorSvc = new();
        private readonly AdminDashboardController _controller;

        public AdminDashboardControllerTests()
        {
            _controller = new AdminDashboardController(
                _dashboardSvc.Object, _planSvc.Object, _clientSvc.Object,
                _claimSvc.Object, _invoiceSvc.Object, _endorSvc.Object);
        }

        [Fact]
        public async Task GetSummary_ValidRequest_ReturnsOkWithData()
        {
            var summary = new DashboardSummaryDto { CustomerCount = 5 };
            _dashboardSvc.Setup(x => x.GetSummaryAsync()).ReturnsAsync(summary);

            var result = await _controller.GetSummary();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var val = Assert.IsType<DashboardSummaryDto>(ok.Value);
            Assert.Equal(5, val.CustomerCount);
        }

        [Fact]
        public async Task GetAllClients_ValidRequest_ReturnsOkWithList()
        {
            var list = new List<CorporateClientResponseDto> { new CorporateClientResponseDto() };
            _dashboardSvc.Setup(x => x.GetAllClientsAsync()).ReturnsAsync(list);

            var result = await _controller.GetAllClients();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<CorporateClientResponseDto>>(ok.Value);
        }

        [Fact]
        public async Task CreatePlan_ValidInput_ReturnsCreatedAtAction()
        {
            var req = new CreateInsurancePlanDto();
            var id = Guid.NewGuid();
            var dto = new InsurancePlanDto { Id = id };
            _planSvc.Setup(x => x.CreatePlanAsync(req)).ReturnsAsync(dto);

            var result = await _controller.CreatePlan(req);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(AdminDashboardController.GetPlanById), created.ActionName);
            Assert.Equal(id, created.RouteValues["id"]);
        }

        [Fact]
        public async Task GetPlanById_PlanExists_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var dto = new InsurancePlanDto();
            _planSvc.Setup(x => x.GetPlanByIdAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetPlanById(id);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<InsurancePlanDto>(ok.Value);
        }

        [Fact]
        public async Task UpdatePlan_PlanExists_ReturnsOk()
        {
            var id = Guid.NewGuid();
            var req = new UpdateInsurancePlanDto();
            var dto = new InsurancePlanDto { Id = id };
            _planSvc.Setup(x => x.UpdatePlanAsync(id, req)).ReturnsAsync(dto);

            var result = await _controller.UpdatePlan(id, req);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<InsurancePlanDto>(ok.Value);
        }
    }
}
