using Clicco.Application.ExternalModels.Payment.Request;
using Clicco.Application.ExternalModels.Payment.Response;
using Clicco.Application.Interfaces.Services.External;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

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

        public async Task<PaymentResult> Cancel(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/cancel", paymentRequest);
        }

        public async Task<PaymentResult> Pay(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/pay", paymentRequest);
        }

        public async Task<PaymentResult> Provision(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/provision", paymentRequest);
        }

        public async Task<PaymentResult> Refund(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"{baseUri}/api/payments/refund", paymentRequest);
        }

        private async Task<PaymentResult> SendRequest(string url,PaymentBankRequest paymentRequest)
        {
            var response = await httpClient.PostAsJsonAsync(url, paymentRequest);
            var result = await response.Content.ReadAsAsync<PaymentResult>();
            return result;
        }
    }
}
