using Clicco.Application.Interfaces.Services.External;
using Microsoft.Extensions.Configuration;

namespace Clicco.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly string baseUri;
        private readonly HttpClient httpClient;
        public UserService(IConfiguration configuration, HttpClient httpClient)
        {
            this.configuration = configuration;
            baseUri = configuration["URLS:AUTH_API"];
            this.httpClient = httpClient;
        }
        public async Task<bool> IsExistAsync(int UserId)
        {
            var response = await httpClient.GetAsync($"{baseUri}/api/users/{UserId}");
            return response.IsSuccessStatusCode;
        }
    }
}
