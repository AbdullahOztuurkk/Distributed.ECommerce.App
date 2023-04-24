using Clicco.Application.ExternalModels.Request;
using Clicco.Application.ExternalModels.Response;
using Clicco.Application.Interfaces.Services.External;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Clicco.Infrastructure.Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly string baseUri;
        private readonly HttpClient httpClient;

        public PaymentService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            baseUri = configuration["URLS:PAYMENT_SERVICE_API"];
            this.httpClient = httpClient;
        }

        public async Task<bool> Cancel(PaymentRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/cancel", paymentRequest);
        }

        public async Task<bool> Pay(PaymentRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/pay", paymentRequest);
        }

        public async Task<bool> Provision(PaymentRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/provision", paymentRequest);
        }

        public async Task<bool> Refund(PaymentRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/refund", paymentRequest);
        }

        private async Task<bool> SendRequest(string url,PaymentRequest paymentRequest)
        {
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(url, paymentRequest);
            PaymentResult paymentResult = await httpResponse.Content.ReadAsAsync<PaymentResult>();
            return paymentResult.IsSuccess;
        }
    }
}
