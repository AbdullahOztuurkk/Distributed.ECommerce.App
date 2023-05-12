using Clicco.AuthAPI.Services.Contracts;
using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient(nameof(EmailService));
        }

        public async Task<bool> SendForgotPasswordEmailAsync(ForgotPasswordEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("Email/SendForgotPasswordEmail",request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendRegistrationEmailAsync(RegistrationEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("Email/SendRegistrationEmail", request);
            return response.IsSuccessStatusCode;
        }
    }
}
