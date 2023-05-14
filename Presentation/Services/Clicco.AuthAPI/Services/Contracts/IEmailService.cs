using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendRegistrationEmailAsync(RegistrationEmailRequest request);
        Task<bool> SendForgotPasswordEmailAsync(ForgotPasswordEmailRequest request);
        Task<bool> SendResetPasswordEmailAsync(ResetPasswordEmailRequest request);

    }
}
