using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IInvoiceService
    {
        Task<bool> CreateInvoice(string BuyerEmail, Transaction transaction, Product product, Address address);
        Task<bool> SendEmailByTransactionId(int transactionId);
    }
}
