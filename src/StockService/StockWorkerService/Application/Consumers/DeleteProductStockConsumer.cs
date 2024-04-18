using MassTransit;
using Shared.Events.Stock;
using StockWorkerService.Application.Services.Abstract;

namespace StockWorkerService.Application.Consumers;
public class DeleteProductStockConsumer : IConsumer<DeleteProductStockEvent>
{
    private readonly IStockService _stockService;
    private readonly ILogger<DeleteProductStockEvent> _logger;

    public DeleteProductStockConsumer(IStockService stockService, ILogger<DeleteProductStockEvent> logger)
    {
        this._stockService = stockService;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<DeleteProductStockEvent> context)
    {
        var stockResponse = await _stockService.Delete(context.Message.ProductId);
        if(stockResponse.IsSuccess)
        {
            _logger.LogInformation($"Stock has been deleted. ProductId = {context.Message.ProductId}");
        }
        else
        {
            _logger.LogError($"Stock couldn't deleted. Product Id {context.Message.ProductId}");
        }

    }
}
