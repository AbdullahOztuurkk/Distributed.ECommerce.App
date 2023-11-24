using Clicco.Domain.Shared.Models.Payment;
using Clicco.PaymentServiceAPI.Models.Contracts;


namespace Clicco.PaymentServiceAPI.Models
{
    public class Global
    {
        public class BaseBank : IBaseBank
        {

            public virtual async Task<PaymentResult> Cancel(PaymentBankRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Pay(PaymentBankRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Provision(PaymentBankRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }

            public virtual async Task<PaymentResult> Refund(PaymentBankRequest request)
            {
                return await Task.FromResult(new SuccessPaymentResult());
            }
        }

        public enum BankServices
        {
            Ziraat = 10,
            Akbank = 20,
            Garanti = 30,
            Albaraka = 40,
        }
    }
}
