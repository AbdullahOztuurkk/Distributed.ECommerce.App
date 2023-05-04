using Clicco.AuthAPI.Models.Request;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Clicco.AuthAPI.Models.Response;
using Clicco.AuthAPI.Models.Extensions;
using Clicco.AuthAPI.Models.Email;
using System;

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
            var result = await authService.UserExistsAsync(request.Email);
            if (result)
            {
                return BadRequest("User already exists");
            }

            //Todo: Must be Validate
            var CreateToUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
            };
            var createdUser = await authService.RegisterAsync(CreateToUser, request.Password);
            return StatusCode(201, createdUser.AsViewModel());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dtoModel)
        {
            var user = await authService.LoginAsync(dtoModel.Email, dtoModel.Password);
            if (user == null)
            {   
                return Unauthorized();
            }

            var tokenHandler = new Services.TokenHandler(configuration);
            var token = tokenHandler.CreateAccessToken(user);
            LoggedUserViewModel model = new(token, dtoModel.Email);
            return Ok(model);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await authService.UserExistsAsync(email);
            if (result)
            {
                await authService.ForgotPasswordAsync(email);
                return Ok("Temporary password sent to email address!");
            }
            return BadRequest("User not found!");
        }
    }
}
