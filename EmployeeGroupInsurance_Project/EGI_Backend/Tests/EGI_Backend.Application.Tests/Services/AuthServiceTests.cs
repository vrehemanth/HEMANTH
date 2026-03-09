using System;
using System.Threading.Tasks;
using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Services;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Moq;
using Xunit;
using AutoMapper;

namespace EGI_Backend.Application.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<ICorporateClientRepository> _mockClientRepo;
        private readonly Mock<IJwtTokenGenerator> _mockJwt;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IEmailService> _mockEmailSvc;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockClientRepo = new Mock<ICorporateClientRepository>();
            _mockJwt = new Mock<IJwtTokenGenerator>();
            _mockMapper = new Mock<IMapper>();
            _mockEmailSvc = new Mock<IEmailService>();

            _authService = new AuthService(
                _mockUserRepo.Object,
                _mockJwt.Object,
                _mockMapper.Object,
                _mockEmailSvc.Object,
                _mockClientRepo.Object
            );
        }

        [Fact]
        public async Task Login_InvalidEmail_ThrowsUnauthorizedException()
        {
            var req = new LoginRequest { Email = "test@test.com", Password = "Pass" };
            _mockUserRepo.Setup(x => x.GetByEmailAsync(req.Email)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UnauthorizedException>(() => _authService.Login(req));
        }

        [Fact]
        public async Task Login_InvalidPassword_ThrowsUnauthorizedException()
        {
            var req = new LoginRequest { Email = "test@test.com", Password = "Wrong" };
            var user = new User { Email = "test@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Correct") };
            
            _mockUserRepo.Setup(x => x.GetByEmailAsync(req.Email)).ReturnsAsync(user);

            await Assert.ThrowsAsync<UnauthorizedException>(() => _authService.Login(req));
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsAuthResponse()
        {
            var req = new LoginRequest { Email = "test@test.com", Password = "Password123" };
            var user = new User { Id = Guid.NewGuid(), Email = "test@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password) };
            var expectedResponse = new AuthResponse { Token = "token123" };

            _mockUserRepo.Setup(x => x.GetByEmailAsync(req.Email)).ReturnsAsync(user);
            _mockJwt.Setup(x => x.GenerateToken(user)).Returns(expectedResponse);

            var result = await _authService.Login(req);

            Assert.NotNull(result);
            Assert.Equal("token123", result.Token);
        }

        [Fact]
        public async Task RegisterAdmin_EmailAlreadyExists_ThrowsConflictException()
        {
            var req = new AdminRegisterRequest { Email = "admin@test.com" };
            _mockUserRepo.Setup(x => x.GetByEmailAsync(req.Email)).ReturnsAsync(new User());

            await Assert.ThrowsAsync<ConflictException>(() => _authService.RegisterAdmin(req));
        }

        [Fact]
        public async Task ChangePassword_UserNotFound_ThrowsNotFoundException()
        {
            var userId = Guid.NewGuid();
            _mockUserRepo.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _authService.ChangePassword(userId, "newPass"));
        }
    }
}
