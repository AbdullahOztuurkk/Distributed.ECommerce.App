using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Model;
using System.Net;

namespace Clicco.Persistence.Services
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        private readonly IUserService userService;
        public AddressService(IUserService userService)
        {
            this.userService = userService;
        }
        public async void CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new Exception("User not found!") { HResult = (int)HttpStatusCode.BadRequest };
        }
    }
}
