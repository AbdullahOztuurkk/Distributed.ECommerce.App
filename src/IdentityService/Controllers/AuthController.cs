using CoreLib.ResponseModel;
using IdentityService.API.Application.Services.Abstract;
using IdentityService.API.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

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
    public async Task<BaseResponse> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.Register(request);
        return result;
    }

    [HttpPost("login")]
    public async Task<BaseResponse> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.Login(request);
        return result;
    }

    [HttpPost("forgot-password")]
    public async Task<BaseResponse> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        var result = await _authService.ForgotPassword(request);
        return result;
    }

    [HttpPost("reset-password")]
    public async Task<BaseResponse> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        var result = await _authService.ResetPassword(request);
        return result;
    }
}
