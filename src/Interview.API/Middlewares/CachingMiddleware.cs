using Interview.Domain.Cache;

namespace Interview.API.Middlewares;

public class CachingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheableMediatrQuery
{
    private readonly ICacheService _cache;
    public CachingMiddleware(ICacheService cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (request.BypassCache)
            return await next();

        var response = await _cache.GetAsync(request.CacheKey, next, TimeSpan.FromHours(10), cancellationToken);
        return response;
    }
}
