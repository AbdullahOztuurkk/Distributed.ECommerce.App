using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Invoice;
using Clicco.Domain.Shared.Models.Transaction;
using Clicco.InvoiceServiceAPI.Data.Models;
using Clicco.InvoiceServiceAPI.Data.Repositories.Contracts;
using Clicco.InvoiceServiceAPI.Services.Contracts;
using static Clicco.Domain.Shared.Global;

namespace Clicco.InvoiceServiceAPI
{
    public class InvoiceWorker : BackgroundService
    {
        private readonly IQueueService queueService;
        private readonly ILogger<InvoiceWorker> logger;
        private readonly IInvoiceRepository invoiceRepository;
        public InvoiceWorker(IServiceProvider serviceProvider, ILogger<InvoiceWorker> logger)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            this.queueService = scope.GetRequiredService<IQueueService>();
            this.logger = logger;
            this.invoiceRepository = scope.GetRequiredService<IInvoiceRepository>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(
                queueService.ReceiveMessages<DeletedTransactionModel>(
                    ExchangeNames.EventExchange, QueueNames.InvoiceOperationsQueue, EventNames.DeletedTransaction, async (model) =>
                {
                    var invoice = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == model.Id);
                    if (invoice != null)
                    {
                        await invoiceRepository.RemoveAsync(invoice.Id);

                        logger.LogInformation($"The invoice with transaction number {model.Id} has been deleted.");
                    }
                }),
                queueService.ReceiveMessages<InvoiceTransaction>(
                    ExchangeNames.EventExchange, QueueNames.InvoiceOperationsQueue, EventNames.UpdatedTransaction, async (invoiceTransaction) =>
                {
                    var invoice = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == invoiceTransaction.Id);
                    if (invoice != null)
                    {
                        invoice.Transaction = invoiceTransaction;

                        await invoiceRepository.UpdateAsync(invoice.Id, invoice);

                        logger.LogInformation($"The invoice with transaction number {invoiceTransaction.Id} has been updated.");
                    }
                }),
                queueService.ReceiveMessages<CreateInvoiceRequest>(
                    ExchangeNames.EventExchange, QueueNames.InvoiceOperationsQueue, EventNames.CreateInvoice, async (model) =>
                {
                    var invoiceModel = new Invoice
                    {
                        Address = model.Address,
                        Coupon = model.Coupon,
                        Product = model.Product,
                        Transaction = model.Transaction,
                        Vendor = model.Vendor,
                        BuyerEmail = model.BuyerEmail,
                    };

                    await invoiceRepository.CreateAsync(invoiceModel);
                }),
                queueService.ReceiveMessages<SendInvoiceDetailsEmailModel>(ExchangeNames.EmailExchange,
                    QueueNames.InvoiceOperationsQueue, EventNames.InvoiceMailRequest, async (id) =>
                    {
                        Invoice result = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == Convert.ToInt32(id));
                        if (result != null)
                        {
                            var invoiceRequestModel = new InvoiceEmailRequest
                            {
                                Address = result.Address,
                                Coupon = result.Coupon,
                                Product = result.Product,
                                Transaction = result.Transaction,
                                Vendor = result.Vendor,
                                To = result.BuyerEmail
                            };

                            await queueService.PushMessage(ExchangeNames.EmailExchange, invoiceRequestModel, EventNames.InvoiceMail);
                        }
                    })
            );
        }
    }
}
