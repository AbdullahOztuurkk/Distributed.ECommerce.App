namespace PaymentService.API.Application.Services.Concrete;

public class AkbankService : BankServiceBase
{

    public override async Task<BaseResponse<PaymentBankResponse>> Pay(PaymentBankRequestDto request)
    {
        return new BaseResponse<PaymentBankResponse> { IsSuccess = false, Data = new PaymentBankResponse().Map(request, "Akbank payment operation has been declined!") };
    }
}
