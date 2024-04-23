namespace PaymentService.API.Application.Services.Abstract;

public interface IBankService
{
    Task<BaseResponse<PaymentBankResponse>> Pay(PaymentBankRequestDto request);
    Task<BaseResponse<PaymentBankResponse>> Provision(PaymentBankRequestDto request);
    Task<BaseResponse<PaymentBankResponse>> Cancel(PaymentBankRequestDto request);
    Task<BaseResponse<PaymentBankResponse>> Refund(PaymentBankRequestDto request);
}
