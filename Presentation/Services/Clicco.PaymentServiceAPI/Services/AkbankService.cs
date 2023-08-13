using Clicco.Domain.Shared.Models.Payment;
using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services
{
    public class AkbankService : BaseBank
    {

        public override async Task<PaymentResult> Pay(PaymentBankRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }
    }
}
