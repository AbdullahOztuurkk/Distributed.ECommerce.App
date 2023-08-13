using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task SendRegistrationEmailAsync(RegistrationEmailRequest request);
        Task SendForgotPasswordEmailAsync(ForgotPasswordEmailRequest request);
        Task SendResetPasswordEmailAsync(ResetPasswordEmailRequest request);
    }
}
