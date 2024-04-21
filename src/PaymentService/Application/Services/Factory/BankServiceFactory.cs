using PaymentService.API.Application.Services.Abstract;

namespace PaymentService.API.Application.Services.Factory;

public class BankServiceFactory : IBankServiceFactory
{
    public IBankService? CreateBankService(int bankId)
    {
        var bank = (Bank)bankId;
        IBankService bankService = null;

        if (bank != null)
        {
            bankService = bank switch
            {
                Bank.Akbank => new AkbankService(),
                Bank.Ziraat => new ZiraatService(),
                Bank.Albaraka => new AlbarakaService(),
                Bank.Garanti => new GarantiService(),
                _ => null
            };
        }
        return bankService;
    }
}
