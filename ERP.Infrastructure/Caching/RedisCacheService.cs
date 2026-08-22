using ERP.Application.Common.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace ERP.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var value = await _db.StringGetAsync(key);

            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value!, _jsonOptions);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(value, _jsonOptions);
            await _db.StringSetAsync(key, json, expiration ?? TimeSpan.FromMinutes(10));
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            var endpoint = _redis.GetEndPoints().First();
            var server = _redis.GetServer(endpoint);
            var keys = server.Keys(pattern: $"{prefix}*").ToArray();

            if (keys.Length > 0)
                await _db.KeyDeleteAsync(keys);
        }
    }
}