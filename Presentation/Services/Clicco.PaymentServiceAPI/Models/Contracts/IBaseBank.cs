using Clicco.Domain.Shared.Models.Payment;

namespace Clicco.PaymentServiceAPI.Models.Contracts
{
    public interface IBaseBank
    {
        Task<PaymentResult> Pay(PaymentBankRequest request);
        Task<PaymentResult> Provision(PaymentBankRequest request);
        Task<PaymentResult> Cancel(PaymentBankRequest request);
        Task<PaymentResult> Refund(PaymentBankRequest request);
    }
}
