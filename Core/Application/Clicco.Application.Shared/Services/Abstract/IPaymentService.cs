using Clicco.Domain.Shared.Models.Payment;

namespace Clicco.Application.Shared.Services.Abstract
{
    public interface IPaymentService
    {
        Task Pay(PaymentBankRequest paymentRequest);
        Task Provision(PaymentBankRequest paymentRequest);
        Task Cancel(PaymentBankRequest paymentRequest);
        Task Refund(PaymentBankRequest paymentRequest);
    }
}
