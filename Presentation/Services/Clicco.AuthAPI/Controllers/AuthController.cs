using Clicco.AuthAPI.Services.Abstract;
using Clicco.AuthServiceAPI.Models.Dtos;
using Clicco.Domain.Core.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace Clicco.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration configuration;

        public AuthController(IAuthService authRepository, IConfiguration configuration)
        {
            this._authService = authRepository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ResponseDto> Register([FromBody] RegisterDto request)
        {
            var result = await _authService.RegisterAsync(request, request.Password);
            return result;
        }

        [HttpPost("login")]
        public async Task<ResponseDto> Login([FromBody] LoginDto dtoModel)
        {
            var result = await _authService.LoginAsync(dtoModel.Email, dtoModel.Password);
            return result;
        }

        [HttpPost("forgot-password")]
        public async Task<ResponseDto> ForgotPassword([FromBody] ForgotPasswordDto dtoModel)
        {
            var result = await _authService.ForgotPasswordAsync(dtoModel);
            return result;
        }

        [HttpPost("reset-password")]
        public async Task<ResponseDto> ResetPassword([FromBody] ResetPasswordDto dtoModel)
        {
            var result = await _authService.ResetPasswordAsync(dtoModel);
            return result;
        }
    }
}
