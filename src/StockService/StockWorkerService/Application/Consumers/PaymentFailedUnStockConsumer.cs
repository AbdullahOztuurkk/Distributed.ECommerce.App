using Shared.Events.Payment;

namespace StockWorkerService.Application.Consumers;
public class PaymentFailedUnStockConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly IStockService _stockService;
    private readonly ILogger<PaymentFailedUnStockConsumer> _logger;

    public PaymentFailedUnStockConsumer(IStockService stockService,
                                        ILogger<PaymentFailedUnStockConsumer> logger)
    {
        this._stockService = stockService;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var @event = context.Message;

        var stock = (await _stockService.GetByProductId(@event.OrderItem.ProductId)).Data;
        if (stock != null)
        {
            stock.Count += @event.OrderItem.Count;
            await _stockService.Update(new() { Count = stock.Count, ProductId = @event.OrderItem.ProductId });
        }

        _logger.LogInformation($"Stock was released for Order Id ({@event.TransactionId})");
    }
}
