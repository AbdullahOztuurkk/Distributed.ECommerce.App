using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Shared.Models.Payment;
using System.Net.Http.Json;

namespace Clicco.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public PaymentService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient(nameof(PaymentService));
        }

        public async Task<PaymentResult> Cancel(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"payments/cancel", paymentRequest);
        }

        public async Task<PaymentResult> Pay(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"payments/pay", paymentRequest);
        }

        public async Task<PaymentResult> Provision(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"payments/provision", paymentRequest);
        }

        public async Task<PaymentResult> Refund(PaymentBankRequest paymentRequest)
        {
            return await SendRequest($"payments/refund", paymentRequest);
        }

        private async Task<PaymentResult> SendRequest(string url,PaymentBankRequest paymentRequest)
        {
            var response = await httpClient.PostAsJsonAsync(url, paymentRequest);
            var result = await response.Content.ReadAsAsync<PaymentResult>();
            return result;
        }
    }
}
