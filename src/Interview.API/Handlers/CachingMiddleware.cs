namespace Interview.Domain.Cache
{
    public class CachingMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheableMediatrQuery
    {
        private readonly ICacheService _cache;
        private readonly ILogger _logger;
        public CachingMiddleware(ICacheService cache, ILogger<TResponse> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;

            if (request.BypassCache)
                return await next();

            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                await _cache.SetAsync(request.CacheKey, response, TimeSpan.FromHours(10), cancellationToken);
                return response;
            }

            response = await _cache.GetAsync<TResponse>(request.CacheKey, cancellationToken);
            if (response != null)
            {
                _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
            }
            return response;
        }
    }
}
