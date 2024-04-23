using Shared.Events.Order;

namespace StockWorkerService.Application.Consumers;

public class StockCheckRequestEventConsumer : IConsumer<OrderCreatedStockCheckRequestEvent>
{
    private readonly IStockService _stockService;
    private readonly ILogger<StockCheckRequestEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public StockCheckRequestEventConsumer(IStockService stockService,
                                        ILogger<StockCheckRequestEventConsumer> logger,
                                        IPublishEndpoint publishEndpoint)
    {
        this._stockService = stockService;
        this._logger = logger;
        this._publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<OrderCreatedStockCheckRequestEvent> context)
    {
        var @event = context.Message;
        bool isStockReserved = false;
        var stock = (await _stockService.GetByProductId(@event.OrderItem.ProductId)).Data;
        if (stock != null)
        {
            if (stock.Count >= @event.OrderItem.Count)
            {
                stock.Count -= @event.OrderItem.Count;
                await _stockService.Update(new() { Count = stock.Count, ProductId = @event.OrderItem.ProductId });
                isStockReserved = true;
            }
        }
        if (isStockReserved)
        {
            _logger.LogInformation($"Stock was reserved for Buyer Id: {@event.BuyerEmail}");

            var stockReservedEvent = new StockReservedEvent
            {
                BuyerEmail = @event.BuyerEmail,
                TransactionId = @event.TransactionId,
                OrderItem = @event.OrderItem,
                FullName = @event.FullName,
                Payment = @event.Payment,
            };

            await _publishEndpoint.Publish(stockReservedEvent);
        }
        else
        {
            _logger.LogError($"Stock was not reserved for Order Id: {@event.BuyerEmail}");

            var stockNotReserved = new StockNotReservedEvent
            {
                Message = "Not enough stock!",
                TransactionId = @event.TransactionId,
                ProductName = @event.OrderItem.Name,
                FullName = @event.FullName,
                To = @event.BuyerEmail
            };

            await _publishEndpoint.Publish(stockNotReserved);
        }
    }
}