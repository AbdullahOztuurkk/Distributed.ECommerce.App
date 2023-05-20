using Clicco.InvoiceServiceAPI.Configurations;
using Clicco.InvoiceServiceAPI.Data.Common;
using Clicco.InvoiceServiceAPI.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Clicco.InvoiceServiceAPI.Data.Context
{
    public class MongoDbContext : DbContext
    {
        public override IDbCollection<Invoice> Invoices { get; }

        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            var mongoUrl = new MongoUrl(options.Value.ConnectionString);

            client = new MongoClient(mongoUrl);
            database = client.GetDatabase(options.Value.DatabaseName);
            Invoices = new InvoiceCollection(database.GetCollection<Invoice>(options.Value.InvoiceCollectionName));
        }
    }
}
