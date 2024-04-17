namespace CommerceService.Application.Services.Abstract.External;

public interface IPaymentService
{
    Task Pay(PaymentBankRequestDto paymentRequest);
    Task Provision(PaymentBankRequestDto paymentRequest);
    Task Cancel(PaymentBankRequestDto paymentRequest);
    Task Refund(PaymentBankRequestDto paymentRequest);
}
