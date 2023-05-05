using Clicco.Domain.Shared.Models.Payment;
using Clicco.PaymentServiceAPI.Models.Contracts;


namespace Clicco.PaymentServiceAPI.Models
{
    public class Global
    {
        public class BaseBank : IBaseBank
        {

            public virtual async Task<PaymentResult> Cancel(PaymentRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Pay(PaymentRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Provision(PaymentRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Refund(PaymentRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }
        }

        public enum BankServices{
            Ziraat = 10,
            Akbank = 20,
            Garanti = 30,
            Albaraka = 40,
        }
    }
}
