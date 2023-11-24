using Clicco.PaymentServiceAPI.Services.Contracts;
using static Clicco.PaymentServiceAPI.Models.Global;

namespace Clicco.PaymentServiceAPI.Services.Factory
{
    public class BankServiceFactory : IBankServiceFactory
    {
        public BaseBank CreateBankService(int bankId)
        {
            var bankService = (BankServices)Enum.ToObject(typeof(BankServices), bankId);

            if (bankService != null)
            {
                switch (bankService)
                {
                    case BankServices.Ziraat:
                        return new ZiraatBankService();
                    case BankServices.Garanti:
                        return new GarantiBankService();
                    case BankServices.Akbank:
                        return new AkbankService();
                    case BankServices.Albaraka:
                        return new AlbarakaBankService();
                }
            }
            else
            {
                throw new ArgumentException("Invalid Bank!");
            }
            return null;
        }
    }
}
