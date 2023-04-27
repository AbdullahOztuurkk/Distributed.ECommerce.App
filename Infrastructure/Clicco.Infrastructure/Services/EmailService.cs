using Clicco.Application.ExternalModels.Email;
using Clicco.Application.Interfaces.Services.External;
using Microsoft.Extensions.Configuration;

namespace Clicco.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly string baseUri;
        private readonly HttpClient httpClient;
        public EmailService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            baseUri = configuration["URLS:EMAIL_SERVICE_API"];
            this.httpClient = httpClient;
        }

        public async Task<bool> SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendFailedPaymentEmail", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendSuccessPaymentEmail", request);
            return response.IsSuccessStatusCode;
        }
    }
}
