using Stock.Service.Extensions;
using StockWorkerService.Application.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((hostcontext ,services) =>
    {
        var configuration = hostcontext.Configuration;
        services.RegisterModule(new StockModule(configuration));
    })
    .ConfigureServices((context,services) =>
    {
        services.AddHostedService<StockWorker>();
        services.AddMasstransitWithConsumers();
        services.AddServiceDiscovery(context.Configuration);
    })
    .Build();

await host.RunAsync();