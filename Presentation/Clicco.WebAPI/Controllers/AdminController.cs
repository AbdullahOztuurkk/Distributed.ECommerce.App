using Clicco.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.WebAPI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(SystemAdministratorFilter))]
    [ApiController]
    [Route("api/management")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Content("Pong!");
        }
    }
}
