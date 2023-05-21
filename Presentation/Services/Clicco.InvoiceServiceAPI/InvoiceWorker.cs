using Clicco.Domain.Shared.Models.Invoice;
using Clicco.InvoiceServiceAPI.Data.Repositories.Contracts;
using Clicco.InvoiceServiceAPI.Services.Contracts;
using static Clicco.Domain.Shared.Global;

namespace Clicco.InvoiceServiceAPI
{
    public class InvoiceWorker : BackgroundService
    {
        private readonly IRabbitMqService rabbitMqService;
        private readonly ILogger<InvoiceWorker> logger;
        private readonly IInvoiceRepository invoiceRepository;
        public InvoiceWorker(IServiceProvider serviceProvider, ILogger<InvoiceWorker> logger)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            this.rabbitMqService = scope.GetRequiredService<IRabbitMqService>();
            this.logger = logger;
            this.invoiceRepository = scope.GetRequiredService<IInvoiceRepository>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(
                rabbitMqService.ReceiveMessages<int>(QueueNames.DeletedTransactionQueue, async (id) =>
                {
                    var invoice = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == id);
                    if (invoice != null)
                    {
                        await invoiceRepository.RemoveAsync(invoice.Id);

                        logger.LogInformation($"The invoice with transaction number {id} has been deleted.");
                    }
                }),
                rabbitMqService.ReceiveMessages<InvoiceTransaction>(QueueNames.UpdatedTransactionQueue, async (invoiceTransaction) =>
                {
                    var invoice = await invoiceRepository.FindOneAsync(x => x.Transaction.Id == invoiceTransaction.Id);
                    if (invoice != null)
                    {
                        invoice.Transaction = invoiceTransaction;

                        await invoiceRepository.UpdateAsync(invoice.Id, invoice);

                        logger.LogInformation($"The invoice with transaction number {invoiceTransaction.Id} has been updated.");
                    }
                })
            );
        }
    }
}
