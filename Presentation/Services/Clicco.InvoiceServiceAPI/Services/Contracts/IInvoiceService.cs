using Clicco.Domain.Shared.Models.Invoice;
using Clicco.InvoiceServiceAPI.Data.Models;

namespace Clicco.InvoiceServiceAPI.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<InvoiceResult> CreateAsync(CreateInvoiceRequest invoice);
        //<InvoiceResult> GetByTransactionIdAsync(int transactionId);
        Task<InvoiceResult> SendInvoiceEmail(int transactionId);
    }
}
