using Invoice.Service.Persistence.Context.Abstract;
using MongoDB.Driver;
using Shared.Domain.Constant;
using Shared.Events.Invoice;
using Shared.Events.Mail;

namespace Invoice.Service.Application.Consumers;

public class SendInvoiceDetailEmailRequestConsumer : IConsumer<SendInvoiceAsEmailRequestEvent>
{
    private readonly IMongoDbContext _dbContext;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<SendInvoiceDetailEmailRequestConsumer> _logger;

    public SendInvoiceDetailEmailRequestConsumer(IMongoDbContext dbContext,
                                                 ISendEndpointProvider sendEndpointProvider,
                                                 ILogger<SendInvoiceDetailEmailRequestConsumer> logger)
    {
        this._dbContext = dbContext;
        this._sendEndpointProvider = sendEndpointProvider;
        this._logger = logger;
    }

    public async Task Consume(ConsumeContext<SendInvoiceAsEmailRequestEvent> context)
    {
        var @event = context.Message;
        Domain.Concrete.Invoice result = await _dbContext.Invoices.Find(x => x.Transaction.Id == @event.TransactionId).FirstOrDefaultAsync();

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
