using CommerceService.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CommerceService.Application.Services.Concrete;

public class UserSessionService : IUserSessionService
{
    private readonly IHttpContextAccessor contextAccessor;
    public UserSessionService(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }
    public string GetUserEmail()
    {
        return contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
    }

    public int GetUserId()
    {
        return Convert.ToInt32(contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }

    public string GetUserName()
    {
        return contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
    }
}
