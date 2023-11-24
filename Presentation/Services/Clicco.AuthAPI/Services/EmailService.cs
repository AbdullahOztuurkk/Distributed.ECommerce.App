using Clicco.AuthAPI.Services.Contracts;
using Clicco.AuthServiceAPI.Services.Contracts;
using Clicco.Domain.Shared.Models.Email;
using static Clicco.Domain.Shared.Global;

namespace Clicco.AuthAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IQueueService queueService;

        public EmailService(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        public async Task SendForgotPasswordEmailAsync(ForgotPasswordEmailRequestDto request)
        {
            await queueService.PushMessage(ExchangeNames.EmailExchange, request, EventNames.ForgotPasswordMailRequest);
        }

        public async Task SendRegistrationEmailAsync(RegistrationEmailRequestDto request)
        {
            await queueService.PushMessage(ExchangeNames.EmailExchange, request, EventNames.RegistrationMailRequest);
        }

        public async Task SendResetPasswordEmailAsync(ResetPasswordEmailRequestDto request)
        {
            await queueService.PushMessage(ExchangeNames.EmailExchange, request, EventNames.ResetPasswordMailRequest);

        }
    }
}
