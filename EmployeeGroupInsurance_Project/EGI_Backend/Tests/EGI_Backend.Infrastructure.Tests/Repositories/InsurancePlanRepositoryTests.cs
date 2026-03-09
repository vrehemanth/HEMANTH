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
    public class InsurancePlanRepositoryTests
    {
        private DbContextOptions<EGIDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<EGIDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_PlansExist_ReturnsAll()
        {
            var options = GetOptions();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                ctx.InsurancePlans.Add(new InsurancePlan { Id = Guid.NewGuid(), PlanName = "Plan1" });
                ctx.InsurancePlans.Add(new InsurancePlan { Id = Guid.NewGuid(), PlanName = "Plan2" });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new InsurancePlanRepository(ctx);
                var result = await repo.GetAllAsync();
                Assert.Equal(2, System.Linq.Enumerable.Count(result));
            }
        }

        [Fact]
        public async Task GetAllAsync_EmptyTable_ReturnsEmptyList()
        {
            var options = GetOptions();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new InsurancePlanRepository(ctx);
                var result = await repo.GetAllAsync();
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetByIdAsync_PlanExists_ReturnsPlan()
        {
            var options = GetOptions();
            var id = Guid.NewGuid();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                ctx.InsurancePlans.Add(new InsurancePlan { Id = id, PlanName = "Gold" });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new InsurancePlanRepository(ctx);
                var result = await repo.GetByIdAsync(id);
                Assert.NotNull(result);
                Assert.Equal("Gold", result.PlanName);
            }
        }

        [Fact]
        public async Task GetByIdAsync_DoesNotExist_ReturnsNull()
        {
            var options = GetOptions();
            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new InsurancePlanRepository(ctx);
                var result = await repo.GetByIdAsync(Guid.NewGuid());
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task AddAsync_ValidPlan_AddsSuccesfully()
        {
            var options = GetOptions();
            var id = Guid.NewGuid();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new InsurancePlanRepository(ctx);
                await repo.AddAsync(new InsurancePlan { Id = id, PlanName = "Silver" });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var saved = await ctx.InsurancePlans.FindAsync(id);
                Assert.NotNull(saved);
                Assert.Equal("Silver", saved.PlanName);
            }
        }
    }
}
