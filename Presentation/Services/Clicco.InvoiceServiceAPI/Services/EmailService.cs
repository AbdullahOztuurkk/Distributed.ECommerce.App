using Clicco.InvoiceServiceAPI.Services.Contracts;

namespace Clicco.InvoiceServiceAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly string baseUri;

        public EmailService(HttpClient httpClient, IConfiguration configuration)
        {
            this.configuration = configuration;
            baseUri = configuration["URLS:EMAIL_SERVICE_API"];
            this.httpClient = httpClient;
        }
        public async Task<bool> SendInvoiceEmailAsync(object request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendInvoiceEmail", request);
            return response.IsSuccessStatusCode;
        }
    }
}
