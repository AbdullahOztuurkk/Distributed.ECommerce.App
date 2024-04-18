using CommerceService.Application.Services.Concrete;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Events.Mail;

namespace CommerceService.Application.Consumers;
public class FailedPaymentEmailConsumer : BaseService,  IConsumer<PaymentBankResponse>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public FailedPaymentEmailConsumer(ISendEndpointProvider endpointProvider)
    {
        this._sendEndpointProvider = endpointProvider;
    }
    public async Task Consume(ConsumeContext<PaymentBankResponse> context)
    {
        var @event = context.Message;
        var transaction = await Db.GetDefaultRepo<Transaction>().GetAsync(x => x.Id == @event.TransactionId);

        transaction.TransactionStatus = TransactionStatus.Success;

        var emailRequest = new FailedPaymentEmailRequestEvent
        {
            Error = @event.ErrorMessage,
            Amount = transaction.DiscountedAmount < transaction.TotalAmount
                                ? string.Format("{0:N2}", transaction.DiscountedAmount)
                                : string.Format("{0:N2}", transaction.TotalAmount),
            FullName = @event.FullName,
            OrderNumber = transaction.Code,
            PaymentMethod = "Credit / Bank Card",
            ProductName = @event.ProductName,
            To = @event.To,
        };

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.SuccessPaymentEmailRequestEvent}"));

        await sendEndpoint.Send(emailRequest);
    }
}
