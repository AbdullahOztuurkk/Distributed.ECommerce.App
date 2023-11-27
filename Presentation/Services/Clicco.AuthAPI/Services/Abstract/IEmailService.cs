using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services.Abstract
{
    public interface IEmailService
    {
        Task SendRegistrationEmailAsync(RegistrationEmailRequestDto request);
        Task SendForgotPasswordEmailAsync(ForgotPasswordEmailRequestDto request);
        Task SendResetPasswordEmailAsync(ResetPasswordEmailRequestDto request);
    }
}
