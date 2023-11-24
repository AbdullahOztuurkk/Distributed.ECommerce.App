namespace Clicco.Application.Services.Abstract.External
{
    public interface IPaymentService
    {
        Task Pay(PaymentBankRequest paymentRequest);
        Task Provision(PaymentBankRequest paymentRequest);
        Task Cancel(PaymentBankRequest paymentRequest);
        Task Refund(PaymentBankRequest paymentRequest);
    }
}
