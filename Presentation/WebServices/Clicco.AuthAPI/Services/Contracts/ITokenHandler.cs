namespace Clicco.AuthAPI.Services.Contracts
{
    public interface ITokenHandler<T>
    {   
        string CreateAccessToken(T entity);
    }
}
