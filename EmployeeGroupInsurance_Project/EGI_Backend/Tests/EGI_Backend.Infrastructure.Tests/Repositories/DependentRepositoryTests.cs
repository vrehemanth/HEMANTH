using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using EGI_Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace EGI_Backend.Infrastructure.Tests.Repositories
{
    public class DependentRepositoryTests
    {
        private DbContextOptions<EGIDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<EGIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddAsync_ValidDependent_AddsDependentToDatabase()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var dependent = new Dependent
            {
                Id = Guid.NewGuid(),
                FullName = "Test Dependent",
                MemberId = Guid.NewGuid()
            };

            // Act
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repository = new DependentRepository(context);
                await repository.AddAsync(dependent);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var addedDependent = await context.Dependents.FindAsync(dependent.Id);
                Assert.NotNull(addedDependent);
                Assert.Equal("Test Dependent", addedDependent.FullName);
            }
        }

        [Fact]
        public async Task GetByIdAsync_DependentExists_ReturnsCorrectDependent()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var dependent = new Dependent { Id = Guid.NewGuid(), FullName = "Found Me", MemberId = Guid.NewGuid() };

            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                context.Dependents.Add(dependent);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repository = new DependentRepository(context);
                var result = await repository.GetByIdAsync(dependent.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Found Me", result.FullName);
            }
        }

        [Fact]
        public async Task GetByIdAsync_DependentDoesNotExist_ReturnsNull()
        {
            // Arrange
            var options = GetInMemoryOptions();

            // Act
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repository = new DependentRepository(context);
                var result = await repository.GetByIdAsync(Guid.NewGuid());

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task UpdateAsync_ExistingDependent_UpdatesDependentInDatabase()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var dependentId = Guid.NewGuid();
            var dependent = new Dependent { Id = dependentId, FullName = "Old Name", MemberId = Guid.NewGuid() };

            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                context.Dependents.Add(dependent);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repository = new DependentRepository(context);
                var toUpdate = await repository.GetByIdAsync(dependentId);
                toUpdate.FullName = "New Name";
                
                await repository.UpdateAsync(toUpdate);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var updated = await context.Dependents.FindAsync(dependentId);
                Assert.Equal("New Name", updated.FullName);
            }
        }

        [Fact]
        public async Task AddAsync_MultipleDependents_SavesSuccessfully()
        {
            // Arrange
            var options = GetInMemoryOptions();
            var dep1 = new Dependent { Id = Guid.NewGuid(), FullName = "Dep 1", MemberId = Guid.NewGuid() };
            var dep2 = new Dependent { Id = Guid.NewGuid(), FullName = "Dep 2", MemberId = Guid.NewGuid() };

            // Act
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repository = new DependentRepository(context);
                await repository.AddAsync(dep1);
                await repository.AddAsync(dep2);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var count = await context.Dependents.CountAsync();
                Assert.Equal(2, count);
            }
        }
    }
}
