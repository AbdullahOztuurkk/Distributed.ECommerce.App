using Clicco.AuthAPI.Services.Abstract;
using Clicco.AuthServiceAPI.Services.Contracts;
using Clicco.Domain.Shared.Models.Email;
using static Clicco.Domain.Shared.Global;

namespace Clicco.AuthServiceAPI.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IQueueService _bus;

        public EmailService(IQueueService bus)
        {
            this._bus = bus;
        }

        public async Task SendForgotPasswordEmailAsync(ForgotPasswordEmailRequestDto request)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, request, EventNames.ForgotPasswordMailRequest);
        }

        public async Task SendRegistrationEmailAsync(RegistrationEmailRequestDto request)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, request, EventNames.RegistrationMailRequest);
        }

        public async Task SendResetPasswordEmailAsync(ResetPasswordEmailRequestDto request)
        {
            await _bus.PushMessage(ExchangeNames.EmailExchange, request, EventNames.ResetPasswordMailRequest);

        }
    }
}
