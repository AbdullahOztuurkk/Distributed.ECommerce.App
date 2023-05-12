using Clicco.Domain.Shared.Models.Email;
using Clicco.InvoiceServiceAPI.Data.Models;
using Clicco.InvoiceServiceAPI.Services.Contracts;

namespace Clicco.InvoiceServiceAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient(nameof(EmailService));
        }
        public async Task<bool> SendInvoiceEmailAsync(Invoice request)
        {
            var invoiceModel = new InvoiceEmailRequest
            {
                Address = request.Address,
                Coupon = request.Coupon,
                Product = request.Product,
                Transaction = request.Transaction,
                Vendor = request.Vendor,
                To = request.BuyerEmail
            };

            var response = await httpClient.PostAsJsonAsync("Email/SendInvoiceEmail", invoiceModel);
            return response.IsSuccessStatusCode;
        }
    }
}
