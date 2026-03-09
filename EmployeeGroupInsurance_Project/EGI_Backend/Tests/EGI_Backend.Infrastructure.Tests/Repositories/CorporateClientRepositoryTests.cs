using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using EGI_Backend.Infrastructure.Persistence;
using EGI_Backend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EGI_Backend.Infrastructure.Tests.Repositories
{
    public class CorporateClientRepositoryTests : IDisposable
    {
        private readonly EGIDbContext _context;
        private readonly CorporateClientRepository _repository;

        public CorporateClientRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EGIDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _context = new EGIDbContext(options, mockHttpContextAccessor.Object);
            _repository = new CorporateClientRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task AddAsync_ValidCorporateClient_AddsToDatabase()
        {
            var client = new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Tech Corp", Address = "123 Tech St", UserId = Guid.NewGuid() };
            await _repository.AddAsync(client);
            await _context.SaveChangesAsync();

            var saved = await _context.CorporateClients.FirstOrDefaultAsync(c => c.Id == client.Id);
            Assert.NotNull(saved);
            Assert.Equal("Tech Corp", saved.CompanyName);
        }

        [Fact]
        public async Task GetByIdAsync_ClientExists_ReturnsClient()
        {
            var user = new User { Id = Guid.NewGuid(), Name = "Test User", Email = "test@example.com" };
            _context.Users.Add(user);

            var client = new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Health Inc", Address = "456 Wellness Way", UserId = user.Id };
            _context.CorporateClients.Add(client);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(client.Id);

            Assert.NotNull(result);
            Assert.Equal("Health Inc", result.CompanyName);
        }

        [Fact]
        public async Task GetByIdAsync_ClientDoesNotExist_ReturnsNull()
        {
            var result = await _repository.GetByIdAsync(Guid.NewGuid());
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByUserIdAsync_UserExists_ReturnsClient()
        {
            var user = new User { Id = Guid.NewGuid(), Name = "Logistics User", Email = "logistics@example.com" };
            _context.Users.Add(user);

            var userId = user.Id;
            var client = new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Logistics Co", Address = "789 Freight Rd", UserId = userId };
            _context.CorporateClients.Add(client);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal("Logistics Co", result.CompanyName);
        }

        [Fact]
        public async Task CountPendingAsync_MultipleClients_ReturnsCountOfPending()
        {
            _context.CorporateClients.AddRange(
                new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Pending 1", Address = "Addr 1", UserId = Guid.NewGuid(), Status = VerificationStatus.Pending },
                new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Approved 2", Address = "Addr 2", UserId = Guid.NewGuid(), Status = VerificationStatus.Approved },
                new CorporateClient { Id = Guid.NewGuid(), CompanyName = "Pending 3", Address = "Addr 3", UserId = Guid.NewGuid(), Status = VerificationStatus.Pending }
            );
            await _context.SaveChangesAsync();

            var result = await _repository.CountPendingAsync();

            Assert.Equal(2, result);
        }
    }
}
