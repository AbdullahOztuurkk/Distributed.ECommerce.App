using Clicco.PaymentServiceAPI.Models.Request;
using Clicco.PaymentServiceAPI.Models.Response;
using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services
{
    public class AkbankService : BaseBank
    {

        public override async Task<PaymentResult> Pay(PaymentRequest request)
        {
            return await Task.FromResult(new FailedPaymentResult($"Error at {GetType().Name}"));
        }
    }
}
