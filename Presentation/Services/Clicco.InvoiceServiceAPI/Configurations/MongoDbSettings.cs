namespace Clicco.InvoiceServiceAPI.Configurations
{
    public interface IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string InvoiceCollectionName { get; set; }
    }

    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string InvoiceCollectionName { get; set; }
    }
}
