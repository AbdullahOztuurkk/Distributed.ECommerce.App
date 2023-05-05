using Clicco.Domain.Shared.Models.Payment;

namespace Clicco.PaymentServiceAPI.Models.Contracts
{
    public interface IBaseBank
    {
        Task<PaymentResult> Pay(PaymentRequest request);
        Task<PaymentResult> Provision(PaymentRequest request);
        Task<PaymentResult> Cancel(PaymentRequest request);
        Task<PaymentResult> Refund(PaymentRequest request);
    }
}
