using Clicco.InvoiceServiceAPI.Data.Models;

namespace Clicco.InvoiceServiceAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendInvoiceEmailAsync(Invoice request);
    }
}
