using PaymentService.API.Application.Services.Abstract;

namespace PaymentService.API.Application.Services.Concrete;

public class BankServiceBase : IBankService
{

    public virtual async Task<BaseResponse<PaymentBankResponse>> Cancel(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request) };
    }

    public virtual async Task<BaseResponse<PaymentBankResponse>> Pay(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request) };
    }

    public virtual async Task<BaseResponse<PaymentBankResponse>> Provision(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request) };
    }

    public virtual async Task<BaseResponse<PaymentBankResponse>> Refund(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request) };
    }
}