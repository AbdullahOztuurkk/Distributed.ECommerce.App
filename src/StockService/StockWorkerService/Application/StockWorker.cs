using Consul;

namespace StockWorkerService.Application;

public class StockWorker : BackgroundService
{
    private readonly ILogger<StockWorker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IConsulClient _consulClient;
    private readonly AgentServiceRegistration registration;
    public StockWorker(ILogger<StockWorker> logger,
                       IConfiguration configuration,
                       IConsulClient consulClient)
    {
        _logger = logger;
        this._configuration = configuration;
        this._consulClient = consulClient;

        var uri = _configuration.GetValue<Uri>("ConsulConfig:ServiceAddress");
        var serviceName = _configuration.GetValue<string>("ConsulConfig:ServiceName");
        var serviceId = _configuration.GetValue<string>("ConsulConfig:ServiceId");

        registration = new AgentServiceRegistration()
        {
            ID = serviceName,
            Name = serviceName,
            Address = $"{uri.Host}",
            Port = uri.Port,
            Tags = new[] { serviceName, serviceId, uri.Scheme }
        };
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stock WorkerService has been started!");
        _logger.LogInformation("Registering with Consul");

        _consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        _consulClient.Agent.ServiceRegister(registration).Wait();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deregistering from Consul");
        _consulClient.Agent.ServiceDeregister(registration.ID).Wait();
    }
}
