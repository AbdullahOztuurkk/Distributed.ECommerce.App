using Clicco.Application.Interfaces.Services.External;

namespace Clicco.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient(nameof(UserService));
        }
        public async Task<bool> IsExistAsync(int UserId)
        {
            var response = await httpClient.GetAsync($"/api/users/{UserId}");
            return response.IsSuccessStatusCode;
        }
    }
}
