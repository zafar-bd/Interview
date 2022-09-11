namespace Interview.Domain.Cache;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key, RequestHandlerDelegate<T>? valueProvider = null, TimeSpan? expire = null, CancellationToken? cancellationToken = default);
    
    Task<T> GetAsync<T>(string key, Func<T>? valueProvider = null, TimeSpan? expire = null, CancellationToken? cancellationToken = default);

    Task<T> GetAsync<T>(string key, CancellationToken? cancellationToken = default);

    Task SetAsync<T>(string key, T value, TimeSpan? expire = null, CancellationToken? cancellationToken = default);

    Task SetStringAsync(string key, string value, TimeSpan? expire = null, CancellationToken? cancellationToken = default);

    Task RemoveCacheAsync(string key, CancellationToken? cancellationToken = default);

    Task RemoveAllCacheAsync();
}
