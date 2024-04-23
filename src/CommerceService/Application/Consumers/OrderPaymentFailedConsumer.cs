using CommerceService.Application.Services.Concrete;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Events.Mail;
using Shared.Events.Payment;

namespace CommerceService.Application.Consumers;
public class OrderPaymentFailedConsumer : BaseService,  IConsumer<PaymentFailedEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public OrderPaymentFailedConsumer(ISendEndpointProvider endpointProvider)
    {
        this._sendEndpointProvider = endpointProvider;
    }
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var @event = context.Message;
        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == @event.TransactionId);

        transaction.TransactionStatus = TransactionStatus.Failed;

        var emailRequest = new FailedPaymentEmailRequestEvent
        {
            Error = @event.Message,
            Amount = transaction.DiscountedAmount,
            FullName = @event.FullName,
            OrderNumber = transaction.Code,
            PaymentMethod = "Credit / Bank Card",
            ProductName = @event.ProductName,
            To = @event.BuyerEmail,
        };

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.SuccessPaymentEmailRequestEvent}"));

        await sendEndpoint.Send(emailRequest);
    }
}
