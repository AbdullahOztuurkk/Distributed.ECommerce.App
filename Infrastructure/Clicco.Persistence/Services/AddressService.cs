using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        private readonly IUserService userService;
        public AddressService(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new CustomException(CustomErrors.UserNotFound);
        }
    }
}
