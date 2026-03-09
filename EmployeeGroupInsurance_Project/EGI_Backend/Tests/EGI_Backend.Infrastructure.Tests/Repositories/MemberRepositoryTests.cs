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
    public class MemberRepositoryTests
    {
        private DbContextOptions<EGIDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<EGIDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddAsync_ValidMember_SavesToDatabase()
        {
            var options = GetOptions();
            var member = new Member { Id = Guid.NewGuid(), FullName = "Bob" };

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new MemberRepository(ctx);
                await repo.AddAsync(member);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var saved = await ctx.Members.FindAsync(member.Id);
                Assert.NotNull(saved);
                Assert.Equal("Bob", saved.FullName);
            }
        }

        [Fact]
        public async Task GetByIdAsync_MemberExists_ReturnsCorrectMember()
        {
            var options = GetOptions();
            var id = Guid.NewGuid();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                ctx.Members.Add(new Member { Id = id, FullName = "Alice" });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new MemberRepository(ctx);
                var result = await repo.GetByIdAsync(id);
                Assert.NotNull(result);
                Assert.Equal("Alice", result.FullName);
            }
        }

        [Fact]
        public async Task GetByIdAsync_MemberDoesNotExist_ReturnsNull()
        {
            var options = GetOptions();
            
            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new MemberRepository(ctx);
                var result = await repo.GetByIdAsync(Guid.NewGuid());
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task UpdateAsync_ExistingMember_UpdatesDatabase()
        {
            var options = GetOptions();
            var id = Guid.NewGuid();

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                ctx.Members.Add(new Member { Id = id, FullName = "Old" });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new MemberRepository(ctx);
                var member = await repo.GetByIdAsync(id);
                member.FullName = "New";
                await repo.UpdateAsync(member);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var updated = await ctx.Members.FindAsync(id);
                Assert.Equal("New", updated.FullName);
            }
        }

        [Fact]
        public async Task AddAsync_MultipleMembers_PersistsAll()
        {
            var options = GetOptions();
            
            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                var repo = new MemberRepository(ctx);
                await repo.AddAsync(new Member { Id = Guid.NewGuid() });
                await repo.AddAsync(new Member { Id = Guid.NewGuid() });
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new EGIDbContext(options, new Mock<IHttpContextAccessor>().Object))
            {
                Assert.Equal(2, await ctx.Members.CountAsync());
            }
        }
    }
}
