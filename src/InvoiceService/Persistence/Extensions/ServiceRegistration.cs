namespace InvoiceWorkerService.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        return services.AddSingleton<IDbContext, MongoDbContext>();
    }

    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        return services;
    }
}
