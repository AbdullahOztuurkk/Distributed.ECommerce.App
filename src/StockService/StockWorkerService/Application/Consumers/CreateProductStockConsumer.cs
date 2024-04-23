namespace StockWorkerService.Application.Consumers;
public class CreateProductStockConsumer : IConsumer<CreateProductStockEvent>
{
    private readonly IStockService _stockService;
    private readonly ILogger<CreateProductStockConsumer> _logger;

    public CreateProductStockConsumer(IStockService stockService, ILogger<CreateProductStockConsumer> logger)
    {
        this._stockService = stockService;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateProductStockEvent> context)
    {
        var @event = context.Message;

        await _stockService.Insert(new() { ProductId = @event.ProductId, Count = @event.Count});

        _logger.LogInformation($"Stock has been added. ProductId = {@event.ProductId} Amount: {@event.Count}");
    }
}
