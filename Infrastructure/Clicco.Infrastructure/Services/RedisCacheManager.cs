﻿using Clicco.Application.Interfaces.CacheManager;
using StackExchange.Redis;
using System.Text.Json;

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

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getValueFunc, int expirationDate = 60)
        {
            var cachedValue = db.StringGet(key);
            if (!cachedValue.IsNull)
            {
                // return value if value exists on redis
                return JsonSerializer.Deserialize<T>(cachedValue);
            }

            // Execute getValueFunc for get value if key doesnt exist on redis
            var result = await getValueFunc();

            // Caching new value
            await db.StringSetAsync(key, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(expirationDate));

            return result;
        }

        public async Task SetAsync(string key, string value, int expirationDate = 60)
        {
            await db.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromMinutes(expirationDate));
        }
        public async Task SetAsync<T>(string key, T value, int expirationDate = 60)
        {
            await db.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromMinutes(expirationDate));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cachedValue = await db.StringGetAsync(key);
            return !cachedValue.IsNull ? JsonSerializer.Deserialize<T>(cachedValue) : default(T);
        }

        public async Task RemoveAsync(string key)
        {
            await db.KeyDeleteAsync(key);
        }
    }
}
