using InvoiceWorkerService.Domain.Concrete;
using InvoiceWorkerService.Persistence.Context;

namespace InvoiceWorkerService.Persistence.Repositories.Contracts;

public interface IInvoiceRepository : IAsyncRepository<Invoice>
{

}
