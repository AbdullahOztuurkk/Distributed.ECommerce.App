using Clicco.Application.Services.Abstract.External;
using Clicco.Domain.Shared.Models.Payment;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IQueueService _bus;
        public PaymentService(IQueueService bus)
        {
            this._bus = bus;
        }

        public async Task Cancel(PaymentBankRequest paymentRequest)
        {
            await _bus.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentCancelRequest);
        }

        public async Task Pay(PaymentBankRequest paymentRequest)
        {
            await _bus.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentRequest);
        }

        public async Task Provision(PaymentBankRequest paymentRequest)
        {
            await _bus.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentProvisionRequest);
        }

        public async Task Refund(PaymentBankRequest paymentRequest)
        {
            await _bus.PushMessage(ExchangeNames.EventExchange, paymentRequest, EventNames.PaymentRefundRequest);
        }
    }
}
