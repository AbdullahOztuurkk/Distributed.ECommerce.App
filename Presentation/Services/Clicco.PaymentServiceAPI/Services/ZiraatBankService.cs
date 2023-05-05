using Clicco.Domain.Shared.Models.Payment;
using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services
{
    public class ZiraatBankService : BaseBank
    {
        public override async Task<PaymentResult> Pay(PaymentRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }

        public override async Task<PaymentResult> Provision(PaymentRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }
    }
}
