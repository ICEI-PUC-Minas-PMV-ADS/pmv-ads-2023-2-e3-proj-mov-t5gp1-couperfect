using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CouperfectServer.Application.Extensions.Caching;

public static class IDistributedCachingExtensions
{
    public static Task SetAsync<TValue>(this IDistributedCache cache, string cacheKey, TValue value, DistributedCacheEntryOptions? cacheEntryOptions = default, CancellationToken cancellationToken = default)
    {
        cacheEntryOptions ??= new DistributedCacheEntryOptions();
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(value);
        return cache.SetAsync(cacheKey, jsonBytes, cacheEntryOptions, cancellationToken);
    }

    public static void Set<TValue>(this IDistributedCache cache, string cacheKey, TValue value, DistributedCacheEntryOptions? cacheEntryOptions = default)
    {
        cacheEntryOptions ??= new DistributedCacheEntryOptions();
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(value);
        cache.Set(cacheKey, jsonBytes, cacheEntryOptions);
    }

    public static async Task<TValue?> GetAsync<TValue>(this IDistributedCache cache, string cacheKey, CancellationToken cancellationToken = default)
    {
        var cacheValueBytes = await cache.GetAsync(cacheKey, cancellationToken);

        if (cacheValueBytes is null or { Length: 0 })
            return default;

        return (TValue?)JsonSerializer.Deserialize(cacheValueBytes, typeof(TValue));
    }

    public static TValue? Get<TValue>(this IDistributedCache cache, string cacheKey)
    {
        var cacheValueBytes = cache.Get(cacheKey);

        if (cacheValueBytes is null or { Length: 0 })
            return default;

        return (TValue?)JsonSerializer.Deserialize(cacheValueBytes, typeof(TValue));
    }

    public static async Task<TValue?> GetOrSetAsync<TValue>(this IDistributedCache cache, string cacheKey, Func<DistributedCacheEntryOptions, Task<TValue>> setterTask, CancellationToken cancellationToken = default)
    {
        var cacheValueBytes = await cache.GetAsync(cacheKey, cancellationToken);

        if (cacheValueBytes is null or { Length: 0 })
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions();
            var cacheSetValue = await setterTask(cacheEntryOptions);
            await cache.SetAsync(cacheKey, cacheSetValue, cacheEntryOptions, cancellationToken);

            return cacheSetValue;
        }

        return (TValue?)JsonSerializer.Deserialize(cacheValueBytes, typeof(TValue));
    }

    public static async Task<TValue?> GetOrSetAsync<TValue>(this IDistributedCache cache, string cacheKey, Func<DistributedCacheEntryOptions, TValue> setterTask, CancellationToken cancellationToken = default)
    {
        var cacheValueBytes = await cache.GetAsync(cacheKey, cancellationToken);

        if (cacheValueBytes is null or { Length: 0 })
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions();
            var cacheSetValue = setterTask(cacheEntryOptions);
            await cache.SetAsync(cacheKey, cacheSetValue, cacheEntryOptions, cancellationToken);

            return cacheSetValue;
        }

        return (TValue?)JsonSerializer.Deserialize(cacheValueBytes, typeof(TValue));
    }
}
