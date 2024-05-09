using Consul;

namespace Payment.Service.Extensions;

public static class ConsulRegistration
{
    public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {
            var address = configuration["ConsulConfig:Address"];
            consulConfig.Address = new Uri(address);
        }));

        return services;
    }

    public static IApplicationBuilder RegisterToConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
    {
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

        var loggingFactory = LoggerFactory.Create(opt => { opt.AddConsole(); });

        var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

        var uri = configuration.GetValue<Uri>("ConsulConfig:ServiceAddress");
        var serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");
        var serviceId = configuration.GetValue<string>("ConsulConfig:ServiceId");

        var registration = new AgentServiceRegistration()
        {
            ID = serviceName,
            Name = serviceName,
            Address = $"{uri.Host}",
            Port = uri.Port,
            Tags = new[] { serviceName, serviceId, uri.Scheme }
        };

        logger.LogInformation("Registering with Consul");
        consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        consulClient.Agent.ServiceRegister(registration).Wait();

        lifetime.ApplicationStopping.Register(() =>
        {
            logger.LogInformation("Deregistering from Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });

        return app;
    }
}