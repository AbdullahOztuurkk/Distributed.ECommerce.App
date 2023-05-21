namespace Clicco.Application.Interfaces.CacheManager
{
    public interface ICacheManager
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getValueFunc, int expirationDate = 60);
        Task SetAsync(string key, string value, int expirationDate = 60);
        Task SetAsync<T>(string key, T value, int expirationDate = 60);
        Task<T> GetAsync<T>(string key);
        Task<bool> ExistAsync(string key);
        Task RemoveAsync(string key);
    }
}
