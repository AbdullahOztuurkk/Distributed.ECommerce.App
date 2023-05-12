using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Shared.Models.Email;
using Microsoft.Extensions.Configuration;

namespace Clicco.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public EmailService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient(nameof(EmailService));
        }

        public async Task<bool> SendFailedPaymentEmailAsync(PaymentFailedEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("Email/SendFailedPaymentEmail", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendInvoiceEmailAsync(object request)
        {
            var response = await httpClient.PostAsJsonAsync("Email/SendInvoiceEmail", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendSuccessPaymentEmailAsync(PaymentSuccessEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("Email/SendSuccessPaymentEmail", request);
            return response.IsSuccessStatusCode;
        }
    }
}
