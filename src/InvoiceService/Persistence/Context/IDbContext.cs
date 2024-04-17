using InvoiceWorkerService.Domain.Concrete;

namespace InvoiceWorkerService.Persistence.Context;

public abstract class IDbContext
{
    public abstract IDbCollection<Invoice> Invoices { get; }
}
