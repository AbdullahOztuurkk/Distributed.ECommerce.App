namespace Invoice.Service.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDbContext, MongoDbContext>();
    }
}
