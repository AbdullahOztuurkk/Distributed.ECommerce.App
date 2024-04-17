using InvoiceWorkerService.Domain.Concrete;
using MongoDB.Driver;

namespace InvoiceWorkerService.Persistence.Context;

public class InvoiceCollection : DbCollection<Invoice>
{
    public InvoiceCollection(IMongoCollection<Invoice> collection) : base(collection)
    {

    }
}
