using Clicco.AuthAPI.Models.Request;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;

        public AuthController(IAuthService authRepository, IConfiguration configuration)
        {
            this.authService = authRepository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await authService.UserExists(request.Email);
            if (result)
            {
                ModelState.AddModelError("UserName", "User already exists");
                return BadRequest(request);
            }

            var CreateToUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
            };
            var createdUser = await authService.Register(CreateToUser, request.Password);
            return StatusCode(201, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dtoModel)
        {
            var user = await authService.Login(dtoModel.Email, dtoModel.Password);
            if (user == null)
            {   
                return Unauthorized();
            }

            var tokenHandler = new Services.TokenHandler(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            return Ok(token);
        }
    }
}
