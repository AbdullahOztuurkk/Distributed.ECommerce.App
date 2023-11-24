using Clicco.Domain.Shared.Models.Email;

namespace Clicco.Application.Services.Abstract.External
{
    public interface IEmailService
    {
        Task SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequestDto request);
        Task SendFailedPaymentEmailAsync(PaymentFailedEmailRequestDto request);
    }
}
