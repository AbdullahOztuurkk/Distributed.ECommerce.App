using CommerceService.Application.Services.Concrete;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Events.Mail;

namespace CommerceService.Application.Consumers;
public class OrderPaymentSuccessConsumer : BaseService,  IConsumer<PaymentSuccessEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public OrderPaymentSuccessConsumer(ISendEndpointProvider endpointProvider)
    {
        this._sendEndpointProvider = endpointProvider;
    }
    public async Task Consume(ConsumeContext<PaymentSuccessEvent> context)
    {
        var @event = context.Message;
        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == @event.TransactionId);

        transaction.TransactionStatus = TransactionStatus.Success;

        var emailRequest = new SuccessPaymentEmailRequestEvent
        {
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
