using Clicco.Application.Interfaces.CacheManager;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Clicco.Infrastructure.Services
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase db;
        public RedisCacheManager(ConnectionMultiplexer redis)
        {
            _redis = redis;
            db = _redis.GetDatabase();
        }

        public bool Exist(string key)
        {
            return db.KeyExists(key);
        }

        public T GetOrSet<T>(string key, Func<T> getValueFunc, int expirationDate = 60)
        {
            var cachedValue = db.StringGet(key);
            if (!cachedValue.IsNull)
            {
                // return value if value exists on redis
                return JsonConvert.DeserializeObject<T>(cachedValue);
            }

            // Execute getValueFunc for get value if key doesnt exist on redis
            var result = getValueFunc();

            // Caching new value
            db.StringSet(key, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(expirationDate));

            return result;
        }

        public T Get<T>(string key)
        {
            var cachedValue = db.StringGet(key);
            return !cachedValue.IsNull ? JsonConvert.DeserializeObject<T>(cachedValue) : default;

        }

        public async void Remove(string key)
        {
            await db.KeyDeleteAsync(key);
        }

        public bool SearchInArray<T>(string key, T value)
        {
            //Check number exist in List
            bool found = false;
            long length = db.ListLength(key);
            for (long i = 0; i < length; i++)
            {
                RedisValue redisValue = db.ListGetByIndex(key, i);
                if (redisValue.HasValue && redisValue.ToString() == value.ToString())
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }
}
