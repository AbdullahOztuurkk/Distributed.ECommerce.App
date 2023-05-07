﻿using Clicco.Domain.Shared.Models.Email;
using Clicco.InvoiceServiceAPI.Data.Models;
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

            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendInvoiceEmail", invoiceModel);
            return response.IsSuccessStatusCode;
        }
    }
}
