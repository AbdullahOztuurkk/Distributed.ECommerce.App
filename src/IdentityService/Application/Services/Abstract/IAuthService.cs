using CoreLib.ResponseModel;
using IdentityService.API.Domain.Dtos;

namespace IdentityService.API.Application.Services.Abstract;

public interface IAuthService
{
    Task<BaseResponse> Register(RegisterRequestDto dto);
    Task<BaseResponse> Login(LoginRequestDto request);
    Task<BaseResponse> ForgotPassword(ForgotPasswordRequestDto dto);
    Task<BaseResponse> ResetPassword(ResetPasswordRequestDto dtoModel);
}
