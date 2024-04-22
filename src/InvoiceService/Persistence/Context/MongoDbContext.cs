namespace Invoice.Service.Persistence.Context;

public class MongoDbContext : IMongoDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase database;
    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var connectionString = _configuration.GetValue<string>("MongoDbConfiguration:ConnectionString");
        var db = _configuration.GetValue<string>("MongoDbConfiguration:Database");

        var client = new MongoClient(connectionString);
        database = client.GetDatabase(db);
    }
    public IMongoCollection<Domain.Concrete.Invoice> Invoices => database.GetCollection<Domain.Concrete.Invoice>(nameof(Invoices));
}
