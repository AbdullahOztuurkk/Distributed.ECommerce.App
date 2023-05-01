using Clicco.InvoiceServiceAPI.Data.Models;

namespace Clicco.InvoiceServiceAPI.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<InvoiceResult> CreateAsync(Invoice invoice);
        //<InvoiceResult> GetByTransactionIdAsync(int transactionId);
        Task<InvoiceResult> SendInvoiceEmail(int transactionId);
    }
}
