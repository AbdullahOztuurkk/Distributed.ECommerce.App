using Clicco.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await userService.IsExistAsync(userId);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }
    }
}
