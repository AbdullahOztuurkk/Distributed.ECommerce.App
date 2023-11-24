namespace Clicco.Application.Services.Abstract
{
    public interface IUserSessionService
    {
        int GetUserId();
        string GetUserEmail();
        string GetUserName();
    }
}
