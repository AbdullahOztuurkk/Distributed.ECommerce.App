using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InvoiceWorkerService.Persistence.Context;

public class MongoDbContext : IDbContext
{
    public override IDbCollection<Invoice> Invoices { get; }

    private readonly MongoClient client;
    private readonly IMongoDatabase database;
    private readonly MongoDbSettings settings;
    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        settings = options.Value;
        var mongoUrl = new MongoUrl(settings.ConnectionString);

        client = new MongoClient(mongoUrl);
        database = client.GetDatabase(settings.DatabaseName);
        Invoices = new InvoiceCollection(database.GetCollection<Invoice>(settings.InvoiceCollectionName));
    }
}
