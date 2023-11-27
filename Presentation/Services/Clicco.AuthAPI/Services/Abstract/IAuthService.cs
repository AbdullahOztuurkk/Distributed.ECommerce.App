using Clicco.AuthAPI.Models;
using Clicco.AuthServiceAPI.Models.Dtos;
using Clicco.Domain.Core.ResponseModel;

namespace Clicco.AuthAPI.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegisterDto dto, string password);
        Task<ResponseDto> LoginAsync(string email, string password);
        Task<ResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<ResponseDto> UserExistsAsync(string email);
        Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto dtoModel);
    }
}
