namespace Clicco.Application.Interfaces.CacheManager
{
    public interface ICacheManager
    {
        bool SearchInArray<T>(string key, T value);
        T GetOrSet<T>(string key, Func<T> getValueFunc, int expirationDate = 60);
        T Get<T>(string key);
        bool Exist(string key);
        void Remove(string key);
    }
}
