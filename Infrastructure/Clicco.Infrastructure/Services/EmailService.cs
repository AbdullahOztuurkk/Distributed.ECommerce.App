using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Shared.Models.Email;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IQueueService rabbitMqService;
        public EmailService(IQueueService rabbitMqService)
        {
            this.rabbitMqService = rabbitMqService;
        }

        public async Task SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EmailExchange, request, EventNames.PaymentFailed);
        }

        public async Task SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request)
        {
            await rabbitMqService.PushMessage(ExchangeNames.EmailExchange, request, EventNames.PaymentSucceed);
        }
    }
}
