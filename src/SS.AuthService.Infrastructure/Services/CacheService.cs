using Microsoft.Extensions.Caching.Distributed;
using SS.AuthService.Application.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SS.AuthService.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
        };

        var bytes = JsonSerializer.SerializeToUtf8Bytes(value);
        await _distributedCache.SetAsync(key, bytes, options);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var bytes = await _distributedCache.GetAsync(key);
        if (bytes == null) return default;

        return JsonSerializer.Deserialize<T>(bytes);
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }
}
