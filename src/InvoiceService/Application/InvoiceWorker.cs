namespace Invoice.Service.Application;

public class InvoiceWorker : BackgroundService
{
    private readonly ILogger<InvoiceWorker> _logger;

    public InvoiceWorker(ILogger<InvoiceWorker> logger)
    {
        this._logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Invoice WorkerService has been started!");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Invoice WorkerService has been stopped!");
    }
}
