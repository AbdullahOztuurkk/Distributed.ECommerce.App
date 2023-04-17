using Clicco.AuthAPI.Models.Email;
using Clicco.AuthAPI.Services.Contracts;

namespace Clicco.AuthAPI.Services
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

        public async Task<bool> SendForgotPasswordEmailAsync(ForgotPasswordEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendForgotPasswordEmail",request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendRegistrationEmailAsync(RegistrationEmailRequest request)
        {
            var response = await httpClient.PostAsJsonAsync($"{baseUri}/api/Email/SendRegistrationEmail", request);
            return response.IsSuccessStatusCode;
        }
    }
}
