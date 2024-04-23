using MongoDB.Driver;

namespace Invoice.Service.Persistence.Context.Abstract;

public interface IMongoDbContext
{
    public IMongoCollection<Domain.Concrete.Invoice> Invoices { get; }
}
