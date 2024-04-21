namespace PaymentService.API.Application.Services.Concrete;

public class ZiraatService : BankServiceBase
{
    public override async Task<BaseResponse<PaymentBankResponse>> Pay(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request, "Ziraat payment operation has been declined!") };
    }

    public override async Task<BaseResponse<PaymentBankResponse>> Provision(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request, "Ziraat provision operation has been declined!") };
    }
}
