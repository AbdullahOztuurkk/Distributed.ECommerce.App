using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Shared.Models.Payment;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IQueueService rabbitMqService;
        public PaymentService(IQueueService rabbitMqService)
        {
            this.rabbitMqService = rabbitMqService;
        }

        public async Task Cancel(PaymentBankRequest paymentRequest)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentCancelRequest);
        }

        public async Task Pay(PaymentBankRequest paymentRequest)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentRequest);
        }

        public async Task Provision(PaymentBankRequest paymentRequest)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentProvisionRequest);
        }

        public async Task Refund(PaymentBankRequest paymentRequest)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentRefundRequest);
        }
    }
}
