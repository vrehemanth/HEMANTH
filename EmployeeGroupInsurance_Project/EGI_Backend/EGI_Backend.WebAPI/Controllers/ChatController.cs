using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.DTOs;
using System.Linq;

namespace EGI_Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "guest";
            
            var role = User?.FindFirstValue(ClaimTypes.Role) ?? 
                       User?.FindFirstValue("role") ?? 
                       User?.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role") ?? 
                       "Guest";

            var response = await _chatService.ProcessMessage(userId, role, request.Message);

            return Ok(new { response });
        }
    }
}
