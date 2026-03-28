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

        // [Authorize(Roles = "Admin")]
        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin(AdminRegisterRequest request)
        {
            var result = await _authService.RegisterAdmin(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            //throw new Exception("MANUAL TEST: Simulating a massive database deadlock 500 error!");
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
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            await _authService.ChangePassword(userId, req.NewPassword);
            return Ok("Password changed successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest req)
        {
            await _authService.ForgotPassword(req);
            return Ok("If an account exists with this email, a reset token has been sent.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest req)
        {
            await _authService.ResetPassword(req);
            return Ok("Password has been reset successfully.");
        }
    }
}
