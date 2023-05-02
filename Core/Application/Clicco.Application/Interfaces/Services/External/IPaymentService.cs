using Clicco.Domain.Shared.Models.Payment;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IPaymentService
    {
        Task<PaymentResult> Pay(PaymentBankRequest paymentRequest);
        Task<PaymentResult> Provision(PaymentBankRequest paymentRequest);
        Task<PaymentResult> Cancel(PaymentBankRequest paymentRequest);
        Task<PaymentResult> Refund(PaymentBankRequest paymentRequest);

    }
}
