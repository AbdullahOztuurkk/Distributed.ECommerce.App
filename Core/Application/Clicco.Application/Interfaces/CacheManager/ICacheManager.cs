namespace Clicco.Application.Interfaces.CacheManager
{
    public interface ICacheManager
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getValueFunc, int expirationDate = 60);
        Task<T> GetAsync<T>(string key);
        Task<bool> ExistAsync(string key);
        Task RemoveAsync(string key);
        Task<List<string>> GetListAsync(string key);
        Task AddToListAsync(string key, string value);
    }
}
