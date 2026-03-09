using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Services;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Moq;
using Xunit;

namespace EGI_Backend.Application.Tests.Services
{
    public class PolicyEndorsementServiceTests
    {
        private readonly Mock<IPolicyEndorsementRepository> _mockEndorsementRepo;
        private readonly Mock<IPolicyAssignmentRepository> _mockPolicyRepo;
        private readonly Mock<IMemberRepository> _mockMemberRepo;
        private readonly Mock<IDependentRepository> _mockDependentRepo;
        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly Mock<ICorporateClientRepository> _mockClientRepo;
        private readonly Mock<IUnitOfWork> _mockUoW;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PolicyEndorsementService _service;

        public PolicyEndorsementServiceTests()
        {
            _mockEndorsementRepo = new Mock<IPolicyEndorsementRepository>();
            _mockPolicyRepo = new Mock<IPolicyAssignmentRepository>();
            _mockMemberRepo = new Mock<IMemberRepository>();
            _mockDependentRepo = new Mock<IDependentRepository>();
            _mockInvoiceService = new Mock<IInvoiceService>();
            _mockClientRepo = new Mock<ICorporateClientRepository>();
            _mockUoW = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();

            _service = new PolicyEndorsementService(
                _mockEndorsementRepo.Object,
                _mockPolicyRepo.Object,
                _mockMemberRepo.Object,
                _mockDependentRepo.Object,
                _mockClientRepo.Object,
                _mockInvoiceService.Object,
                _mockUoW.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task SubmitEndorsementAsync_PolicyDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var dto = new SubmitEndorsementDto { PolicyAssignmentId = Guid.NewGuid() };
            var customerId = Guid.NewGuid();

            _mockPolicyRepo.Setup(x => x.GetByIdWithDetailsAsync(dto.PolicyAssignmentId))
                .ReturnsAsync((PolicyAssignment)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => 
                _service.SubmitEndorsementAsync(customerId, dto));
            Assert.Equal("Policy not found.", ex.Message);
        }

        [Fact]
        public async Task SubmitEndorsementAsync_PolicyExists_CreatesEndorsement()
        {
            // Arrange
            var policyId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var dto = new SubmitEndorsementDto 
            { 
                PolicyAssignmentId = policyId,
                Type = EndorsementType.AddMember,
                Description = "Adding new employee",
                EndorsementData = new { FirstName = "John", LastName = "Doe" }
            };

            var policy = new PolicyAssignment 
            { 
                Id = policyId,
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(355),
                InsurancePlan = new InsurancePlan { BasePremium = 1000m }
            };

            _mockPolicyRepo.Setup(x => x.GetByIdWithDetailsAsync(policyId)).ReturnsAsync(policy);
            _mockEndorsementRepo.Setup(x => x.AddAsync(It.IsAny<PolicyEndorsement>())).Returns(Task.CompletedTask);
            _mockUoW.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            
            var expectedResponse = new EndorsementResponseDto { Id = Guid.NewGuid() };
            _mockMapper.Setup(x => x.Map<EndorsementResponseDto>(It.IsAny<PolicyEndorsement>())).Returns(expectedResponse);

            // Act
            var result = await _service.SubmitEndorsementAsync(customerId, dto);

            // Assert
            Assert.NotNull(result);
            _mockEndorsementRepo.Verify(x => x.AddAsync(It.Is<PolicyEndorsement>(e => 
                e.PolicyAssignmentId == policyId && 
                e.Type == EndorsementType.AddMember &&
                e.Status == EndorsementStatus.Pending)), Times.Once);
            _mockUoW.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ReviewEndorsementAsync_EndorsementDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var endorsementId = Guid.NewGuid();
            _mockEndorsementRepo.Setup(x => x.GetByIdAsync(endorsementId)).ReturnsAsync((PolicyEndorsement)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _service.ReviewEndorsementAsync(Guid.NewGuid(), endorsementId, new ReviewEndorsementDto()));
        }

        [Fact]
        public async Task ReviewEndorsementAsync_EndorsementNotPending_ThrowsBadRequestException()
        {
            // Arrange
            var endorsementId = Guid.NewGuid();
            var endorsement = new PolicyEndorsement { Id = endorsementId, Status = EndorsementStatus.Approved };
            
            _mockEndorsementRepo.Setup(x => x.GetByIdAsync(endorsementId)).ReturnsAsync(endorsement);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.ReviewEndorsementAsync(Guid.NewGuid(), endorsementId, new ReviewEndorsementDto()));
        }

        [Fact]
        public async Task GetEndorsementsByPolicyAsync_PolicyExists_ReturnsMappedList()
        {
            // Arrange
            var policyId = Guid.NewGuid();
            var endorsementsList = new List<PolicyEndorsement> { new PolicyEndorsement { Id = Guid.NewGuid() } };
            
            _mockEndorsementRepo.Setup(x => x.GetByPolicyIdAsync(policyId)).ReturnsAsync(endorsementsList);
            
            var dtoList = new List<EndorsementResponseDto> { new EndorsementResponseDto { Id = Guid.NewGuid() } };
            _mockMapper.Setup(x => x.Map<List<EndorsementResponseDto>>(endorsementsList)).Returns(dtoList);

            // Act
            var result = await _service.GetEndorsementsByPolicyAsync(policyId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
