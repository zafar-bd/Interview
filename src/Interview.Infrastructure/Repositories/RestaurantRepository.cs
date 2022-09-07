using Interview.Domain.Dto;
using Interview.Domain.ViewModel;
using Mapster;

namespace Infrastructure.Data.Repositories
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(EFContext dbContext) : base(dbContext)
        {
        }

        public async Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto args, CancellationToken cancellationToken)
        {
            RestaurantScheduleViewModel restaurantData = new();
            var query = base.Entity.AsQueryable();

            if (args.RestaurantId > 0)
                query = query.Where(r => r.Id == args.RestaurantId);

            if (string.IsNullOrWhiteSpace(args.Name))
                query = query.Where(r => r.Name.Contains(args.Name, StringComparison.InvariantCultureIgnoreCase));

            if (args.DayId > 0)
                query = query.Where(r => r.Schedules.Any(s => s.DayId == args.DayId));

            if (args.Start is not null && args.End is not null)
                query = query.Where(r => r.Schedules.Any(s => s.Start >= args.Start && s.End <= args.End));

            if (args.PageMaxSize > 0 && args.PageIndex > 0)
            {
                restaurantData.Count = await query.CountAsync();

                int pageIndex = (int)args.PageIndex - 1;
                int pageSize = (int)args.PageMaxSize;
                int skip = pageIndex * pageSize;

                query = query.Skip(skip).Take(pageSize);
            }

            var resturants = await query.ProjectToType<RestaurantData>().ToArrayAsync(cancellationToken);
            //var resturants = await query.Select(r => new RestaurantData
            //{
            //    Restaurant = new()
            //    {
            //        Name = r.Name,
            //        Id = r.Id
            //    },
            //    Schedules = r.Schedules.Select(s => new ScheduleViewModel
            //    {
            //        Day = new() { Name = s.Day.Name },
            //        Id = s.Id,
            //        Start = s.Start,
            //        End = s.End

            //    }).ToArray()
            //})
            //  .ToArrayAsync(cancellationToken);

            restaurantData.Data = resturants;

            return restaurantData;
        }
    }
}