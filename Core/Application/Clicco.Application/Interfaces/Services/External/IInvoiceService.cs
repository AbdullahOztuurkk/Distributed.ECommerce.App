using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IInvoiceService
    {
        Task CreateInvoice(string BuyerEmail, Transaction transaction, Product product, Address address);
        Task SendEmailByTransactionId(int transactionId);
    }
}
