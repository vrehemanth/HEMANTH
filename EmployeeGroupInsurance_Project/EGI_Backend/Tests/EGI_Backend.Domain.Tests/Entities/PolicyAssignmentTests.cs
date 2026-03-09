using System;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Xunit;

namespace EGI_Backend.Domain.Tests.Entities
{
    public class PolicyAssignmentTests
    {
        [Fact]
        public void Constructor_ValidProperties_SetsPropertiesCorrectly()
        {
            var planId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var start = DateTime.UtcNow;
            var end = start.AddYears(1);

            var policy = new PolicyAssignment
            {
                InsurancePlanId = planId,
                CorporateClientId = clientId,
                StartDate = start,
                EndDate = end,
                Status = PolicyStatus.Active,
                PolicyNo = "POL-123"
            };

            Assert.Equal(planId, policy.InsurancePlanId);
            Assert.Equal(clientId, policy.CorporateClientId);
            Assert.Equal(start, policy.StartDate);
            Assert.Equal(end, policy.EndDate);
            Assert.Equal(PolicyStatus.Active, policy.Status);
            Assert.Equal("POL-123", policy.PolicyNo);
        }

        [Fact]
        public void Constructor_DefaultInitialization_SetsDatesCorrectly()
        {
            var policy = new PolicyAssignment();
            Assert.Equal(default, policy.StartDate);
            Assert.Equal(default, policy.EndDate);
        }

        [Fact]
        public void Status_WhenUpdated_ReturnsNewStatus()
        {
            var policy = new PolicyAssignment { Status = PolicyStatus.Expired };
            policy.Status = PolicyStatus.Active;
            Assert.Equal(PolicyStatus.Active, policy.Status);
        }

        [Fact]
        public void PolicyNo_WhenSet_ReturnsAssignedValue()
        {
            var policy = new PolicyAssignment();
            policy.PolicyNo = "EGI-999";
            Assert.Equal("EGI-999", policy.PolicyNo);
        }

        [Fact]
        public void StartDate_WhenChanged_UpdatesCorrectly()
        {
            var date = new DateTime(2025, 1, 1);
            var policy = new PolicyAssignment();
            policy.StartDate = date;
            Assert.Equal(date, policy.StartDate);
        }
    }
}
