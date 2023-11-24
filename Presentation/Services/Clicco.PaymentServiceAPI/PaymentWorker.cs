using Clicco.Domain.Shared.Models.Payment;
using Clicco.PaymentServiceAPI.Services.Contracts;
using static Clicco.Domain.Shared.Global;

namespace Clicco.PaymentServiceAPI
{
    public class PaymentWorker : BackgroundService
    {
        private readonly IBankServiceFactory bankServiceFactory;
        private readonly IQueueService queueService;
        public PaymentWorker(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            queueService = scope.GetRequiredService<IQueueService>();
            this.bankServiceFactory = scope.GetRequiredService<IBankServiceFactory>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll
            (
                ProcessPayOperationAsync(QueueNames.BankPayOperationQueue, EventNames.PaymentRequest)
            //ProcessProvisionOperationAsync(QueueNames.BankProvisionOperationQueue, EventNames.PaymentProvisionRequest),
            //ProcessRefundOperationAsync(QueueNames.BankRefundOperationQueue, EventNames.PaymentRefundRequest),
            //ProcessCancelOperationAsync(QueueNames.BankCancelOperationQueue, EventNames.PaymentCancelRequest)
            );
        }

        private async Task ProcessPayOperationAsync(string queueName, string routingKey)
        {
            await queueService.ReceiveMessages<PaymentBankRequest>(ExchangeNames.EventExchange, queueName, routingKey, async model =>
            {
                var bank = bankServiceFactory.CreateBankService(model.BankId);
                var result = await bank.Pay(model);
                var responseModel = new PaymentBankResponse
                {
                    FullName = model.FullName,
                    OrderNumber = model.OrderNumber,
                    PaymentMethod = model.PaymentMethod,
                    ProductName = model.ProductName,
                    To = model.To,
                    TotalAmount = model.TotalAmount,
                    TransactionId = model.TransactionId,
                };

                if (result.IsSuccess)
                {
                    await queueService.PushMessage(ExchangeNames.EventExchange, responseModel, EventNames.PaymentSucceed);
                }
                else
                {
                    responseModel.ErrorMessage = result.Message;
                    await queueService.PushMessage(ExchangeNames.EventExchange, responseModel, EventNames.PaymentFailed);
                }
            });
        }
    }
}
