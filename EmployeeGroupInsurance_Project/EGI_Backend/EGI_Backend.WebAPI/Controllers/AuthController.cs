using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.Services;
using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EGI_Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var result = await _authService.Register(req);
            return Ok(result);
        }

        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin(AdminRegisterRequest request)
        {
            var result = await _authService.RegisterAdmin(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var result = await _authService.Login(req);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin/create-staff")]
        public async Task<IActionResult> CreateStaff(AdminCreateStaffRequest req)
        {
            await _authService.CreateStaffByAdmin(req);
            return Ok("Staff created and credentials sent.");
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest req)
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _authService.ChangePassword(userId, req.NewPassword);

            return Ok("Password changed successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest req)
        {
            await _authService.ForgotPassword(req);
            return Ok("If an account exists with this email, a temporary password has been sent.");
        }
    }
}
