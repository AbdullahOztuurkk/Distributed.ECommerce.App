using Clicco.Application.ExternalModels.Payment.Request;
using Clicco.Application.ExternalModels.Payment.Response;

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
