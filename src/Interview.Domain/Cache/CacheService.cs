namespace Interview.Domain.Cache;

public sealed class CacheService : ICacheService
{
    private readonly ICacheClient _inMemoryCacheClient;
    private readonly TimeSpan? _cacheExpireTime = TimeSpan.FromSeconds(10);
    public CacheService(ICacheClient inMemoryCacheClient)
    {
        _inMemoryCacheClient = inMemoryCacheClient;
    }
    public async Task<T> GetAsync<T>(string key,
                                     Func<T>? valueProvider = null,
                                     TimeSpan? expire = null,
                                     CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return default;

        return await InMemoryCache(key, valueProvider, expire);
    }

    public async Task<T> GetAsync<T>(string key,
                                     RequestHandlerDelegate<T>? valueProvider = null,
                                     TimeSpan? expire = null,
                                     CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return default;

        return await InMemoryCache(key, valueProvider, expire);
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return default;

        return (await _inMemoryCacheClient.GetAsync<T>(key)).Value;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expire = null, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return;

        await _inMemoryCacheClient.SetAsync(key, value, expire ?? _cacheExpireTime);
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? expire = null, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return;

        await _inMemoryCacheClient.SetAsync(key, value, expire);
    }

    public async Task RemoveCacheAsync(string key, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return;

        await _inMemoryCacheClient.RemoveAsync(key);
    }

    public async Task RemoveAllCacheAsync()
    {
        await _inMemoryCacheClient.RemoveAllAsync();
    }

    private static T InvokeValueProvider<T>(Func<T> valueProvider)
    {
        if (valueProvider != null)
            return valueProvider();

        return default;
    }

    private async Task<T> InMemoryCache<T>(string key, Func<T> valueProvider, TimeSpan? expire, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return default;

        var data = await _inMemoryCacheClient.GetAsync<T>(key);
        if (data.HasValue)
            return data.Value;
        else
        {
            T value = default;
            value = InvokeValueProvider(valueProvider);
            await _inMemoryCacheClient.SetAsync(key, value, expire ?? _cacheExpireTime);
            return value;
        }
    }

    private async Task<T> InMemoryCache<T>(string key, RequestHandlerDelegate<T> nextvalueProvider, TimeSpan? expire, CancellationToken? cancellationToken = default)
    {
        if (cancellationToken?.IsCancellationRequested ?? false)
            return default;

        var data = await _inMemoryCacheClient.GetAsync<T>(key);
        if (data.HasValue)
            return data.Value;
        else
        {
            T value = default;
            value = await nextvalueProvider();
            await _inMemoryCacheClient.SetAsync(key, value, expire ?? _cacheExpireTime);
            return value;
        }
    }
}