using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Services;
using EGI_Backend.Domain.Entities;
using Moq;
using Xunit;

namespace EGI_Backend.Application.Tests.Services
{
    public class InsurancePlanServiceTests
    {
        private readonly Mock<IInsurancePlanRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUoW;
        private readonly InsurancePlanService _service;

        public InsurancePlanServiceTests()
        {
            _mockRepo = new Mock<IInsurancePlanRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUoW = new Mock<IUnitOfWork>();

            _service = new InsurancePlanService(_mockUoW.Object, _mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllPlansAsync_PlansExist_ReturnsMappedList()
        {
            var plans = new List<InsurancePlan> { new InsurancePlan { Id = Guid.NewGuid() } };
            var dtoList = new List<InsurancePlanDto> { new InsurancePlanDto { Id = Guid.NewGuid() } };

            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(plans);
            _mockMapper.Setup(x => x.Map<IEnumerable<InsurancePlanDto>>(plans)).Returns(dtoList);

            var result = await _service.GetAllPlansAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetPlanByIdAsync_PlanExists_ReturnsMappedItem()
        {
            var planId = Guid.NewGuid();
            var plan = new InsurancePlan { Id = planId };
            var dto = new InsurancePlanDto { Id = planId };

            _mockRepo.Setup(x => x.GetByIdAsync(planId)).ReturnsAsync(plan);
            _mockMapper.Setup(x => x.Map<InsurancePlanDto>(plan)).Returns(dto);

            var result = await _service.GetPlanByIdAsync(planId);

            Assert.NotNull(result);
            Assert.Equal(planId, result.Id);
        }

        [Fact]
        public async Task GetPlanByIdAsync_PlanDoesNotExist_ReturnsNull()
        {
            var planId = Guid.NewGuid();
            _mockRepo.Setup(x => x.GetByIdAsync(planId)).ReturnsAsync((InsurancePlan)null);

            var result = await _service.GetPlanByIdAsync(planId);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreatePlanAsync_ValidData_CreatesAndReturnsId()
        {
            var dto = new CreateInsurancePlanDto 
            { 
                PlanName = "Silver Plan",
                Coverages = new List<CreatePlanCoverageDto>() 
            };
            var expectedId = Guid.NewGuid();

            _mockMapper.Setup(x => x.Map<InsurancePlanDto>(It.IsAny<InsurancePlan>()))
                       .Returns(new InsurancePlanDto { Id = expectedId });
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<InsurancePlan>())).Returns(Task.CompletedTask);
            _mockUoW.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.CreatePlanAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
            _mockUoW.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePlanAsync_PlanDoesNotExist_ThrowsNotFoundException()
        {
            var planId = Guid.NewGuid();
            _mockRepo.Setup(x => x.GetByIdAsync(planId)).ReturnsAsync((InsurancePlan)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdatePlanAsync(planId, new UpdateInsurancePlanDto()));
        }
    }
}
