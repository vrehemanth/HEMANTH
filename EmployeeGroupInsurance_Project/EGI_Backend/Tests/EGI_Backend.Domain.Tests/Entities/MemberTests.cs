using System;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Xunit;

namespace EGI_Backend.Domain.Tests.Entities
{
    public class MemberTests
    {
        [Fact]
        public void Constructor_ValidProperties_SetsPropertiesCorrectly()
        {
            var policyId = Guid.NewGuid();
            var dob = new DateTime(1990, 5, 5);

            var member = new Member
            {
                PolicyAssignmentId = policyId,
                FullName = "Alice Smith",
                EmployeeCode = "EMP001",
                DateOfBirth = dob,
                Gender = Gender.Female,
                SumInsured = 100000m,
                Status = true
            };

            Assert.Equal(policyId, member.PolicyAssignmentId);
            Assert.Equal("Alice Smith", member.FullName);
            Assert.Equal("EMP001", member.EmployeeCode);
            Assert.Equal(dob, member.DateOfBirth);
            Assert.Equal(Gender.Female, member.Gender);
            Assert.Equal(100000m, member.SumInsured);
            Assert.True(member.Status);
        }

        [Fact]
        public void Constructor_DefaultInitialization_SetsIsActiveToTrue()
        {
            var member = new Member();
            Assert.True(member.Status);
        }

        [Fact]
        public void SumInsured_WhenUpdated_ReturnsNewValue()
        {
            var member = new Member { SumInsured = 1000m };
            member.SumInsured = 50000m;
            Assert.Equal(50000m, member.SumInsured);
        }

        [Fact]
        public void FullName_WhenChanged_ReturnsNewName()
        {
            var member = new Member { FullName = "Old Name" };
            member.FullName = "New Name";
            Assert.Equal("New Name", member.FullName);
        }

        [Fact]
        public void Gender_WhenAssigned_ReturnsNewValue()
        {
            var member = new Member { Gender = Gender.Male };
            member.Gender = Gender.Female;
            Assert.Equal(Gender.Female, member.Gender);
        }
    }
}
