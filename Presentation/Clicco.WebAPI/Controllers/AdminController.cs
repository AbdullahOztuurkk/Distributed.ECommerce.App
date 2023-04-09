using Clicco.WebAPI.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.WebAPI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(SystemAdministratorFilter))]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Content("Pong!");
        }
    }
}
