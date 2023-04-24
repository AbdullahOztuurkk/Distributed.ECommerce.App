using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services.Contracts
{
    public interface IBankServiceFactory
    {
        BaseBank CreateBankService(int bankId);
    }
}
