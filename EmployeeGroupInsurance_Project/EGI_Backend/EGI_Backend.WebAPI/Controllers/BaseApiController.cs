using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EGI_Backend.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected Guid CurrentUserId
        {
            get
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                {
                    throw new UnauthorizedAccessException("User is not authenticated or ID is invalid.");
                }
                return userId;
            }
        }

        protected string? CurrentUserRole => User.FindFirstValue(ClaimTypes.Role);
    }
}
