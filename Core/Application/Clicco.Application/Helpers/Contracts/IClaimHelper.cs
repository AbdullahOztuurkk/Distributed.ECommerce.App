using System.Security.Claims;

namespace Clicco.Application.Helpers.Contracts
{
    public interface IClaimHelper
    {
        int GetUserId();
        string GetUserEmail();
        string GetUserName();
    }
}
