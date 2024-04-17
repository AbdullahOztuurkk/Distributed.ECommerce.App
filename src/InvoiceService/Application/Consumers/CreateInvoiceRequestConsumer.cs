namespace InvoiceWorkerService.Application.Consumers;

public class CreateInvoiceRequestConsumer : IConsumer<CreateInvoiceRequestEvent>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public CreateInvoiceRequestConsumer(IInvoiceRepository invoiceRepository)
    {
        this._invoiceRepository = invoiceRepository;
    }

    public async Task Consume(ConsumeContext<CreateInvoiceRequestEvent> context)
    {
        var @event = context.Message;
        var invoiceModel = new Invoice
        {
            Address = @event.Address,
            Coupon = @event.Coupon,
            Product = @event.Product,
            Transaction = @event.Transaction,
            Vendor = @event.Vendor,
            BuyerEmail = @event.BuyerEmail,
        };

        await _invoiceRepository.CreateAsync(invoiceModel);
    }
}