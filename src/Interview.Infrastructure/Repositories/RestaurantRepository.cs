using Interview.Domain.Dto;
using Interview.Domain.ViewModel;
using Mapster;

namespace Infrastructure.Data.Repositories;

public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(RestaurantEFContext dbContext) : base(dbContext)
    {
    }

    public async Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto args, CancellationToken cancellationToken)
    {
        RestaurantScheduleViewModel restaurantData = new();
        var query = base.Entity.AsQueryable();

        if (args.RestaurantId > 0)
            query = query.Where(r => r.Id == args.RestaurantId);

        if (!string.IsNullOrWhiteSpace(args.Name))
            query = query.Where(r => r.Name.Contains(args.Name, StringComparison.InvariantCultureIgnoreCase));

        if (args.DayId > 0)
            query = query.Where(r => r.Schedules.Any(s => s.DayId == args.DayId));

        if (!string.IsNullOrWhiteSpace(args.Start) && !string.IsNullOrWhiteSpace(args.End))
            query = query.Where(r => r.Schedules.Any(s => s.Start >= TimeSpan.Parse(args.Start) && s.End <= TimeSpan.Parse(args.End)));

        restaurantData.Count = await query.CountAsync();

        int pageIndex = args.PageIndex - 1;
        int pageSize = args.PageMaxSize;
        int skip = pageIndex * pageSize;

        query = query.Skip(skip).Take(pageSize);

        var resturants = await query.ProjectToType<RestaurantData>().ToArrayAsync(cancellationToken);
        restaurantData.Data = resturants;

        return restaurantData;
    }
}