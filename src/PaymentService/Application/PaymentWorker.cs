namespace PaymentService.API.Application;

public class PaymentWorker : BackgroundService
{
    private readonly ILogger<PaymentWorker> _logger;

    public PaymentWorker(ILogger<PaymentWorker> logger)
    {
        this._logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Payment WorkerService has been started!");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Payment WorkerService has been stopped!");
    }
}
