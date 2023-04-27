using Clicco.Application.ExternalModels.Email;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IEmailService
    {
        Task<bool> SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request);
        Task<bool> SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request);
    }
}
