using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Payment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.HostedServices
{
    internal class TransactionWorker : BackgroundService
    {
        private readonly IQueueService queueService;
        private readonly ITransactionRepository transactionRepository;
        public TransactionWorker(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            queueService = scope.GetRequiredService<IQueueService>();
            transactionRepository = scope.GetRequiredService<ITransactionRepository>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(

                //Send email for success transaction to buyer
                queueService.ReceiveMessages<PaymentBankResponse>(ExchangeNames.EventExchange, QueueNames.PaidSucceedTransactionsQueue, EventNames.PaymentSucceed, async (model) =>
                {
                    var transaction = await transactionRepository.GetByIdAsync(model.TransactionId);

                    transaction.TransactionStatus = TransactionStatus.Success;

                    var emailRequest = new PaymentSuccessEmailRequestDto
                    {
                        Amount = transaction.DiscountedAmount < transaction.TotalAmount
                            ? string.Format("{0:N2}", transaction.DiscountedAmount)
                            : string.Format("{0:N2}", transaction.TotalAmount),
                        FullName = model.FullName,
                        OrderNumber = transaction.Code,
                        PaymentMethod = "Credit / Bank Card",
                        ProductName = model.ProductName,
                        To = model.To,
                    };

                    await queueService.PushMessage(ExchangeNames.EmailExchange, emailRequest, EventNames.PaymentSucceedMailRequest);
                }),

                //Send email for failed transaction to buyer
                queueService.ReceiveMessages<PaymentBankResponse>(ExchangeNames.EventExchange, QueueNames.PaidFailedTransactionsQueue, EventNames.PaymentFailed, async (model) =>
                {
                    var transaction = await transactionRepository.GetByIdAsync(model.TransactionId);

                    transaction.TransactionStatus = TransactionStatus.Failed;

                    var emailRequest = new PaymentFailedEmailRequestDto
                    {
                        Amount = transaction.DiscountedAmount < transaction.TotalAmount
                            ? string.Format("{0:N2}", transaction.DiscountedAmount)
                            : string.Format("{0:N2}", transaction.TotalAmount),
                        FullName = model.FullName,
                        OrderNumber = transaction.Code,
                        PaymentMethod = "Credit / Bank Card",
                        ProductName = model.ProductName,
                        To = model.To,
                        Error = model.ErrorMessage,
                    };

                    await queueService.PushMessage(ExchangeNames.EmailExchange, emailRequest, EventNames.PaymentFailedMailRequest);
                })
            );
        }
    }
}
