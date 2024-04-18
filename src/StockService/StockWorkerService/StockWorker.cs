namespace StockWorkerService;

public class StockWorker : BackgroundService
{
    private readonly ILogger<StockWorker> _logger;

    public StockWorker(ILogger<StockWorker> logger)
    {
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stock WorkerService has been started!");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stock WorkerService has been stopped!");
    }
}
