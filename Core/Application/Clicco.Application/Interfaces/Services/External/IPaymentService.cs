using Clicco.Application.ExternalModels.Request;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IPaymentService
    {
        Task<bool> Pay(PaymentRequest paymentRequest);
        Task<bool> Provision(PaymentRequest paymentRequest);
        Task<bool> Cancel(PaymentRequest paymentRequest);
        Task<bool> Refund(PaymentRequest paymentRequest);

    }
}
