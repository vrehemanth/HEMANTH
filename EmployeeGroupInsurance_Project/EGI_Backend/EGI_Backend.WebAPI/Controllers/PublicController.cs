using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGI_Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PublicController : ControllerBase
    {
        private readonly IInsurancePlanService _planService;

        public PublicController(IInsurancePlanService planService)
        {
            _planService = planService;
        }

        [HttpGet("insurance-plans")]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _planService.GetActivePlansAsync();
            return Ok(plans);
        }
    }
}
