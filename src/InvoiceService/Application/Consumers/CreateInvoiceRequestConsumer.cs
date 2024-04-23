using Invoice.Service.Persistence.Context.Abstract;
namespace Invoice.Service.Application.Consumers;

public class CreateInvoiceRequestConsumer : IConsumer<CreateInvoiceRequestEvent>
{
    private readonly IMongoDbContext _dbContext;

    public CreateInvoiceRequestConsumer(IMongoDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CreateInvoiceRequestEvent> context)
    {
        var @event = context.Message;
        var invoiceModel = new Domain.Concrete.Invoice
        {
            Address = @event.Address,
            Coupon = @event.Coupon,
            Product = @event.Product,
            Transaction = @event.Transaction,
            Vendor = @event.Vendor,
            BuyerEmail = @event.BuyerEmail,
        };

        await _dbContext.Invoices.InsertOneAsync(invoiceModel);
    }
}