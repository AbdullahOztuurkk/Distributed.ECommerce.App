using Clicco.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Clicco.Application.Services.Concrete
{
    public class UserSessionService : IUserSessionService
    {
        private readonly ClaimsPrincipal _user;
        private readonly IHttpContextAccessor contextAccessor;
        public UserSessionService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            _user = contextAccessor.HttpContext.User;
        }
        public string GetUserEmail()
        {
            return _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }

        public int GetUserId()
        {
            return Convert.ToInt32(_user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        public string GetUserName()
        {
            return _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}
