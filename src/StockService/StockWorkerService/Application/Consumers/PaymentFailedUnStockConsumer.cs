using MassTransit;
using Shared.Events.Payment;
using StockWorkerService.Application.Services.Abstract;

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
        for (int index = 0; index < @event.OrderItems.Count; index++)
        {
            var stock = (await _stockService.GetByProductId(@event.OrderItems[index].ProductId)).Data;
            if (stock != null)
            {
                stock.Count += @event.OrderItems[index].Count;
                await _stockService.Update(new() { Count = stock.Count, ProductId = @event.OrderItems[index].ProductId });
            }
        }

        _logger.LogInformation($"Stock was released for Order Id ({@event.TransactionId})");
    }
}
