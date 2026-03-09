using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System;
using Xunit;

namespace EGI_Backend.Domain.Tests.Entities
{
    public class DependentTests
    {
        [Fact]
        public void Constructor_ValidProperties_SetsPropertiesCorrectly()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            var dob = new DateTime(2000, 1, 1);

            // Act
            var dependent = new Dependent
            {
                MemberId = memberId,
                FullName = "John Doe",
                Relationship = RelationshipType.Child,
                DateOfBirth = dob,
                Gender = Gender.Male,
                SumInsured = 50000m,
                IsActive = true
            };

            // Assert
            Assert.Equal(memberId, dependent.MemberId);
            Assert.Equal("John Doe", dependent.FullName);
            Assert.Equal(RelationshipType.Child, dependent.Relationship);
            Assert.Equal(dob, dependent.DateOfBirth);
            Assert.Equal(Gender.Male, dependent.Gender);
            Assert.Equal(50000m, dependent.SumInsured);
            Assert.True(dependent.IsActive);
        }

        [Fact]
        public void Constructor_DefaultInitialization_SetsIsActiveToTrue()
        {
            // Arrange & Act
            var dependent = new Dependent();

            // Assert
            Assert.True(dependent.IsActive);
        }

        [Fact]
        public void SumInsured_WhenUpdated_ReturnsNewValue()
        {
            // Arrange
            var dependent = new Dependent { SumInsured = 1000m };

            // Act
            dependent.SumInsured = 5000m;

            // Assert
            Assert.Equal(5000m, dependent.SumInsured);
        }

        [Fact]
        public void Relationship_WhenAssigned_ReturnsNewValue()
        {
            // Arrange
            var dependent = new Dependent { Relationship = RelationshipType.Child };

            // Act
            dependent.Relationship = RelationshipType.Spouse;

            // Assert
            Assert.Equal(RelationshipType.Spouse, dependent.Relationship);
        }

        [Fact]
        public void FullName_WhenChanged_ReturnsNewName()
        {
            // Arrange
            var dependent = new Dependent { FullName = "Original Name" };

            // Act
            dependent.FullName = "New Name";

            // Assert
            Assert.Equal("New Name", dependent.FullName);
        }
    }
}
