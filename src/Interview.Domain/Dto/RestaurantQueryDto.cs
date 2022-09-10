using Interview.Domain.Cache;
using MediatR;

namespace Interview.Domain.Dto
{
    public class RestaurantQueryDto : IRequest<RestaurantScheduleViewModel>, ICacheableMediatrQuery
    {
        public RestaurantQueryDto()
        {
            if (PageIndex == 0)
                PageIndex = 1;

            if (PageMaxSize == 0)
                PageMaxSize = 10;
        }
        public string? Name { get; set; }
        public int? RestaurantId { get; set; }
        public int? DayId { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageMaxSize { get; set; } = 10;
        public bool BypassCache { get; set; }
        public string? CacheKey { get; set; }
    }
}
