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

        public async Task<bool> ExistAsync(string key)
        {
            return await db.KeyExistsAsync(key);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<T> getValueFunc, int expirationDate = 60)
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
            await db.StringSetAsync(key, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(expirationDate));

            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cachedValue = await db.StringGetAsync(key);
            return !cachedValue.IsNull ? JsonConvert.DeserializeObject<T>(cachedValue) : default(T);

        }

        public async void RemoveAsync(string key)
        {
            await db.KeyDeleteAsync(key);
        }

        public bool SearchInArray<T>(string key, T value)
        {
            long length = db.ListLength(key);
            for (long i = 0; i < length; i++)
            {
                RedisValue redisValue = db.ListGetByIndex(key, i);
                if (db.ListGetByIndex(key, i).ToString() == value.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<string>> GetListAsync(string key)
        {
            var list = new List<string>();

            var redisValues = await db.ListRangeAsync(key);

            foreach (var value in redisValues)
            {
                list.Add(value);
            }

            return list;
        }

        public async Task AddToListAsync(string key, string value)
        {
            await db.ListRightPushAsync(key, value);
        }

        public async Task<List<string>> SearchInListAsync(string key, string value)
        {
            var list = new List<string>();

            var redisValues = await db.ListRangeAsync(key);

            foreach (var redisValue in redisValues)
            {
                if (redisValue.ToString().Contains(value))
                {
                    list.Add(redisValue);
                }
            }

            return list;
        }
    }
}
