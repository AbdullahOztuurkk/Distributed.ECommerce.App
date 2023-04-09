using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Models.Request;
using Clicco.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authRepository;
        private readonly IConfiguration configuration;

        public AuthController(IAuthService authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await authRepository.UserExists(request.Email))
            {
                ModelState.AddModelError("UserName", "User already exists");
            }
            if (!ModelState.IsValid)
            {
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
            var createdUser = await authRepository.Register(CreateToUser, request.Password);
            return StatusCode(201, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dtoModel)
        {
            var user = await authRepository.Login(dtoModel.Email, dtoModel.Password);
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
