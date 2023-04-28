using Clicco.Application.Helpers.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Clicco.Application.Helpers
{
    public class ClaimHelper : IClaimHelper
    {
        private readonly ClaimsPrincipal _user;
        private readonly IHttpContextAccessor contextAccessor;
        public ClaimHelper(IHttpContextAccessor contextAccessor)
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
