using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EGI_Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PublicController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly IInsurancePlanService _planService;

        public PublicController(IInsurancePlanService planService, IClaimService claimService)
        {
            _planService = planService;
            _claimService = claimService;
        }

        [HttpGet("insurance-plans")]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _planService.GetActivePlansAsync();
            return Ok(plans);
        }

        [Authorize]
        [HttpGet("documents/{id}")]
        public async Task<IActionResult> GetDocument(Guid id)
        {
            var userIdStr = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role) ?? 
                       User.FindFirstValue("role") ?? 
                       User.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var (content, contentType, fileName) = await _claimService.GetSecureDocumentAsync(userId, role, id);
            
            // Forces the browser to attempt to OPEN the file (inline) rather than downloading it immediately.
            Response.Headers.Append("Content-Disposition", $"inline; filename=\"{fileName}\"");
            return File(content, contentType);
        }
    }
}
