using Clicco.Application.Interfaces.Services.External;
using Microsoft.Extensions.Configuration;

namespace Clicco.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly HttpRestClientHelper httpClient;
        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
            httpClient = new HttpRestClientHelper(configuration["URLS:AUTH_API"]);
        }
        public async Task<bool> IsExistAsync(int UserId)
        {
            var result = await httpClient.GetAsync<string>($"/api/users/{UserId}");
            return result != null && result.Length > 0;
        }
    }
}
