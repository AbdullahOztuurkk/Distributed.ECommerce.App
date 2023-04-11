namespace Clicco.Application.Interfaces.Services.External
{
    public interface IUserService
    {
        Task<bool> IsExistAsync(int UserId);
    }
}
