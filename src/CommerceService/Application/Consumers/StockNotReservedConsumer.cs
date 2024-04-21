using CommerceService.Application.Services.Concrete;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Events.Mail;
using Shared.Events.Stock;

namespace CommerceService.Application.Consumers;
public class StockNotReservedConsumer : BaseService,  IConsumer<StockNotReservedEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public StockNotReservedConsumer(ISendEndpointProvider endpointProvider)
    {
        this._sendEndpointProvider = endpointProvider;
    }
    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        var @event = context.Message;
        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == @event.TransactionId);

        transaction.TransactionStatus = TransactionStatus.Success;

        await Db.GetDefaultRepo<Transaction>().SaveChanges();

        var emailRequest = new FailedPaymentEmailRequestEvent
        {
            Error = @event.Message,
            Amount = transaction.DiscountedAmount,
            FullName = @event.FullName,
            OrderNumber = transaction.Code,
            PaymentMethod = "Credit / Bank Card",
            ProductName = @event.ProductName,
            To = @event.To,
        };

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.FailedPaymentEmailRequestQueue}"));

        await sendEndpoint.Send(emailRequest);
    }
}
