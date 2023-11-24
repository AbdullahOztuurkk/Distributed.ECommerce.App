namespace Clicco.Application.Shared.Services.Abstract
{
    public interface IEmailService
    {
        Task SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequestDto request);
        Task SendFailedPaymentEmailAsync(PaymentFailedEmailRequestDto request);
    }
}
