using Interview.Domain.Cache;
using MediatR;

namespace Interview.Domain.Dto
{
    public class RestaurantQueryDto : IRequest<RestaurantScheduleViewModel>, ICacheableMediatrQuery
    {
        public string? Name { get; set; }
        public int? RestaurantId { get; set; }
        public int? DayId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? PageIndex { get; set; }
        public int? PageMaxSize { get; set; }
        public bool BypassCache { get; set; }
        public string? CacheKey { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
