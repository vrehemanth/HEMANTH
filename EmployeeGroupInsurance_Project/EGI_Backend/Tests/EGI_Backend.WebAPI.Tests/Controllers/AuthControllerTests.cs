using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EGI_Backend.WebAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<EGI_Backend.Application.Interfaces.IAuthService> _authSvc = new();
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _controller = new AuthController(_authSvc.Object);
        }

        private void SetupUser(Guid userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task RegisterAdmin_ValidRequest_ReturnsOk()
        {
            var req = new AdminRegisterRequest();
            var res = new AuthResponse();
            _authSvc.Setup(x => x.RegisterAdmin(req)).ReturnsAsync(res);

            var result = await _controller.RegisterAdmin(req);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(res, ok.Value);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            var req = new LoginRequest();
            var res = new AuthResponse();
            _authSvc.Setup(x => x.Login(req)).ReturnsAsync(res);

            var result = await _controller.Login(req);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(res, ok.Value);
        }

        [Fact]
        public async Task ChangePassword_AuthenticatedUser_ReturnsOk()
        {
            var id = Guid.NewGuid();
            SetupUser(id);
            var req = new ChangePasswordRequest { NewPassword = "new" };
            
            _authSvc.Setup(x => x.ChangePassword(id, req.NewPassword)).Returns(Task.CompletedTask);

            var result = await _controller.ChangePassword(req);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Password changed successfully.", ok.Value);
        }

        [Fact]
        public async Task ChangePassword_UnauthenticatedUser_ReturnsUnauthorized()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity()) }
            };
            
            var result = await _controller.ChangePassword(new ChangePasswordRequest());

            Assert.IsType<UnauthorizedResult>(result);
        }
        
        [Fact]
        public async Task Login_ServiceThrows_ShouldNotCatch()
        {
            var req = new LoginRequest();
            _authSvc.Setup(x => x.Login(req)).ThrowsAsync(new UnauthorizedException("Bad creds"));

            await Assert.ThrowsAsync<UnauthorizedException>(() => _controller.Login(req));
        }
    }
}
