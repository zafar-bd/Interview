namespace Interview.Domain.Cache
{
    public interface ICacheableMediatrQuery
    {
        bool BypassCache { get; set; }
        string CacheKey { get; set; }
        TimeSpan? SlidingExpiration { get; set; }
    }
}
