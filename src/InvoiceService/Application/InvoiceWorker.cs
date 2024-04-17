
namespace InvoiceWorkerService.Application;

public class InvoiceWorker : BackgroundService
{
    //private readonly IQueueService queueService;
    private readonly ILogger<InvoiceWorker> logger;
    private readonly IInvoiceRepository invoiceRepository;
    public InvoiceWorker(IServiceProvider serviceProvider, ILogger<InvoiceWorker> logger)
    {
        var scope = serviceProvider.CreateScope().ServiceProvider;
        //this.queueService = scope.GetRequiredService<IQueueService>();
        this.logger = logger;
        invoiceRepository = scope.GetRequiredService<IInvoiceRepository>();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //await Task.WhenAll(
        //    queueService.ReceiveMessages<CreateInvoiceRequest>(
        //        ExchangeNames.EventExchange, QueueNames.InvoiceOperationsQueue, EventNames.CreateInvoice, async (model) =>
        //    {
        //        var invoiceModel = new Invoice
        //        {
        //            Address = model.Address,
        //            Coupon = model.Coupon,
        //            Product = model.Product,
        //            Transaction = model.Transaction,
        //            Vendor = model.Vendor,
        //            BuyerEmail = model.BuyerEmail,
        //        };

        //        await _invoiceRepository.CreateAsync(invoiceModel);
        //    }),
        //    queueService.ReceiveMessages<SendInvoiceDetailsEmailModel>(ExchangeNames.EmailExchange,
        //        QueueNames.InvoiceOperationsQueue, EventNames.InvoiceMailRequest, async (id) =>
        //        {
        //            
        //        })
        //);
    }
}
