using Clicco.AuthAPI.Models;
using Clicco.AuthServiceAPI.Models.Request;
using Clicco.Domain.Core.ResponseModel;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(User user, string password);
        Task<ResponseDto> LoginAsync(string email, string password);
        Task<ResponseDto> ForgotPasswordAsync(string email);
        Task<ResponseDto> UserExistsAsync(string email);
        Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto dtoModel);
    }
}
