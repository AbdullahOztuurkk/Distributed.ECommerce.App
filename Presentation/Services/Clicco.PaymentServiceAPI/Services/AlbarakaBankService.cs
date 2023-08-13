using Clicco.Domain.Shared.Models.Payment;
using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services
{
    public class AlbarakaBankService : BaseBank
    {
        public override async Task<PaymentResult> Refund(PaymentBankRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }

        public override async Task<PaymentResult> Cancel(PaymentBankRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }

        public override async Task<PaymentResult> Pay(PaymentBankRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }

        public override async Task<PaymentResult> Provision(PaymentBankRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }
    }
}
