using Shared.Domain.Constant;
using Shared.Events.Mail;

namespace InvoiceWorkerService.Application.Consumers;

public class SendInvoiceDetailEmailRequestConsumer : IConsumer<SendInvoiceAsEmailRequestEvent>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<SendInvoiceDetailEmailRequestConsumer> _logger;

    public SendInvoiceDetailEmailRequestConsumer(IInvoiceRepository invoiceRepository,
                                                 ISendEndpointProvider sendEndpointProvider,
                                                 ILogger<SendInvoiceDetailEmailRequestConsumer> logger)
    {
        this._invoiceRepository = invoiceRepository;
        this._sendEndpointProvider = sendEndpointProvider;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<SendInvoiceAsEmailRequestEvent> context)
    {
        var @event = context.Message;
        Invoice result = await _invoiceRepository.FindOneAsync(x => x.Transaction.Id == @event.TransactionId);
        
        if (result == null)
        {
            _logger.LogError($"Transaction not Found. Transaction.Id : {@event.TransactionId}");
            return;
        }

        var invoiceRequestModel = new CreateInvoiceRequestEvent
        {
            Address = result.Address,
            Coupon = result.Coupon,
            Product = result.Product,
            Transaction = result.Transaction,
            Vendor = result.Vendor,
            BuyerEmail = result.BuyerEmail
        };

        var emailEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.InvoiceEmailRequestQueue}"));
        await emailEndpoint.Send(invoiceRequestModel);
    }
}
