using Clicco.PaymentServiceAPI.Models.Request;
using Clicco.PaymentServiceAPI.Models.Response;

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
