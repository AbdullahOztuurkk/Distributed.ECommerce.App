using Clicco.Domain.Shared.Models.Email;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IEmailService
    {
        Task<bool> SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request);
        Task<bool> SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request);
        Task<bool> SendInvoiceEmailAsync(object request);
    }
}
