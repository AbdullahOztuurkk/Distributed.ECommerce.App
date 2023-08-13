using Clicco.Domain.Shared.Models.Email;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IEmailService
    {
        Task SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request);
        Task SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request);
    }
}
