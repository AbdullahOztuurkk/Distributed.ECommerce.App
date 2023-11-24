using Clicco.Application.Services.Abstract.External;
using Clicco.Domain.Shared.Models.Email;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IQueueService _bus;
        public EmailService(IQueueService rabbitMqService)
        {
            this._bus = rabbitMqService;
        }

        public async Task SendFailedPaymentEmailAsync(PaymentFailedEmailRequestDto request)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, request, EventNames.PaymentFailed);
        }

        public async Task SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequestDto request)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, request, EventNames.PaymentSucceed);
        }
    }
}
