namespace InvoiceWorkerService.Configurations;

public record MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string InvoiceCollectionName { get; set; }
}