using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services.External
{
    public interface IInvoiceService
    {
        Task<bool> CreateInvoice(Transaction transaction, Product product, Address address);
    }
}
