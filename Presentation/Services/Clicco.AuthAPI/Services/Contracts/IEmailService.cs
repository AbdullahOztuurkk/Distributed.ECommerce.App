using Clicco.AuthAPI.Models.Email;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendRegistrationEmailAsync(RegistrationEmailRequest request);
        Task<bool> SendForgotPasswordEmailAsync(ForgotPasswordEmailRequest request);
    }
}
