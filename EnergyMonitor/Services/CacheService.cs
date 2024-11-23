using StackExchange.Redis;
using System.Text.Json;

namespace EnergyMonitor.Services;

public class CacheService
{
    private readonly IDatabase _cache;

    public CacheService(IConnectionMultiplexer redis)
    {
        _cache = redis.GetDatabase();
    }

    public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiry)
    {
        string jsonData = JsonSerializer.Serialize(value);
        await _cache.StringSetAsync(key, jsonData, expiry);
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        string? jsonData = await _cache.StringGetAsync(key);
        return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
    }
}

