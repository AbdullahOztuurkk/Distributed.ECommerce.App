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
        private readonly IMongoDbSettings settings;
        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            settings = options.Value;
            Console.WriteLine("-----------Message-----------");
            Console.WriteLine(settings.ConnectionString);
            try
            {
                var mongoUrl = new MongoUrl(settings.ConnectionString);

                client = new MongoClient(mongoUrl);
                database = client.GetDatabase(settings.DatabaseName);
                Console.WriteLine(database.DatabaseNamespace);
                Invoices = new InvoiceCollection(database.GetCollection<Invoice>(settings.InvoiceCollectionName));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message 2");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
