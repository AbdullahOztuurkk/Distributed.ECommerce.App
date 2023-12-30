using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Application.Services.Abstract.External;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Shared.Models.Email;
using Clicco.Domain.Shared.Models.Payment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.HostedServices
{
    internal class TransactionWorker : BackgroundService
    {
        private readonly IQueueService _bus;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionWorker(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            _bus = scope.GetRequiredService<IQueueService>();
            _unitOfWork = scope.GetRequiredService<IUnitOfWork>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll(

                //Send email for success transaction to buyer
                _bus.ReceiveMessages<PaymentBankResponse>(ExchangeNames.EventExchange, QueueNames.PaidSucceedTransactionsQueue, EventNames.PaymentSucceed, async (model) =>
                {
                    var transaction = await _unitOfWork.GetRepository<Transaction>().GetByIdAsync(model.TransactionId);

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

                    await _bus.PushMessage(ExchangeNames.EmailExchange, emailRequest, EventNames.PaymentSucceedMailRequest);
                }),

                //Send email for failed transaction to buyer
                _bus.ReceiveMessages<PaymentBankResponse>(ExchangeNames.EventExchange, QueueNames.PaidFailedTransactionsQueue, EventNames.PaymentFailed, async (model) =>
                {
                    var transaction = await _unitOfWork.GetRepository<Transaction>().GetByIdAsync(model.TransactionId);

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

                    await _bus.PushMessage(ExchangeNames.EmailExchange, emailRequest, EventNames.PaymentFailedMailRequest);
                })
            );
        }
    }
}
