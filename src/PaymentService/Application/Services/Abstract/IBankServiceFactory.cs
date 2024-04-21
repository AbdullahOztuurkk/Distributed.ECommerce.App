namespace PaymentService.API.Application.Services.Abstract;

public interface IBankServiceFactory
{
    IBankService CreateBankService(int bankCode);
}
