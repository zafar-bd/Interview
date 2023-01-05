using AutoMapper;
using AutoMapper.QueryableExtensions;
using Interview.Domain.Dto;
using Interview.Domain.Restaurant;
using Interview.Domain.ViewModel;
using Mapster;
using System.Diagnostics;

namespace Infrastructure.Data.Repositories;

public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
{
    private readonly IAsyncRepository<RestaurantView> _resturantView;

    public RestaurantRepository(RestaurantEFContext dbContext, IAsyncRepository<RestaurantView> resturantView) : base(dbContext)
    {
        this._resturantView = resturantView;
    }

    public RestaurantScheduleViewModel GetRestaurantSchedules(RestaurantQueryDto args, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"started GetRestaurantSchedulesAsync: {Environment.CurrentManagedThreadId}");
        RestaurantScheduleViewModel restaurantData = new();
        var query = base.Entity.AsQueryable();

        if (args.RestaurantId > 0)
            query = query.Where(r => r.Id == args.RestaurantId);

        if (!string.IsNullOrWhiteSpace(args.Name))
            query = query.Where(r => r.Name.Contains(args.Name));

        if (args.DayId > 0)
            query = query.Where(r => r.Schedules.Any(s => s.DayId == args.DayId));

        if (!string.IsNullOrWhiteSpace(args.Start) && !string.IsNullOrWhiteSpace(args.End))
            query = query.Where(r => r.Schedules.Any(s => s.Start >= TimeSpan.Parse(args.Start) && s.End <= TimeSpan.Parse(args.End)));

        Debug.WriteLine($"before count: {Environment.CurrentManagedThreadId}");

        restaurantData.Count = query.Count();

        Debug.WriteLine($"after count: {Environment.CurrentManagedThreadId}");

        int pageIndex = args.PageIndex - 1;
        int pageSize = args.PageMaxSize;
        int skip = pageIndex * pageSize;

        query = query.Skip(skip).Take(pageSize);

        //var resturants = await query.ProjectToType<RestaurantData>(new TypeAdapterConfig()
        //    .NewConfig<Restaurant, RestaurantData>()
        //        .Config).ToArrayAsync(cancellationToken);

        var resturants = query.ProjectToType<RestaurantData>().ToArray();

        Debug.WriteLine($"after ToArrayAsync(): {Environment.CurrentManagedThreadId}");

        restaurantData.Data = resturants;

        return restaurantData;
    }

    public async Task<RestaurantScheduleViewModel> GetRestaurantSchedulesAsync(RestaurantQueryDto args, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"started GetRestaurantSchedulesAsync: {Environment.CurrentManagedThreadId}");
        RestaurantScheduleViewModel restaurantData = new();
        var query = base.Entity.AsQueryable();

        if (args.RestaurantId > 0)
            query = query.Where(r => r.Id == args.RestaurantId);

        if (!string.IsNullOrWhiteSpace(args.Name))
            query = query.Where(r => r.Name.Contains(args.Name));

        if (args.DayId > 0)
            query = query.Where(r => r.Schedules.Any(s => s.DayId == args.DayId));

        if (!string.IsNullOrWhiteSpace(args.Start) && !string.IsNullOrWhiteSpace(args.End))
            query = query.Where(r => r.Schedules.Any(s => s.Start >= TimeSpan.Parse(args.Start) && s.End <= TimeSpan.Parse(args.End)));

        Debug.WriteLine($"before count: {Environment.CurrentManagedThreadId}");

        restaurantData.Count = await query.CountAsync(cancellationToken);

        Debug.WriteLine($"after count: {Environment.CurrentManagedThreadId}");

        int pageIndex = args.PageIndex - 1;
        int pageSize = args.PageMaxSize;
        int skip = pageIndex * pageSize;

        query = query.Skip(skip).Take(pageSize);

        //var resturants = await query.ProjectToType<RestaurantData>(new TypeAdapterConfig()
        //    .NewConfig<Restaurant, RestaurantData>()
        //        .Config).ToArrayAsync(cancellationToken);

        var resturants = await query.ProjectToType<RestaurantData>().ToArrayAsync(cancellationToken);
        Debug.WriteLine($"after ToArrayAsync(): {Environment.CurrentManagedThreadId}");

        Debug.WriteLine($"before same action: {Environment.CurrentManagedThreadId}");
        var dd = await query.ProjectToType<RestaurantData>().ToArrayAsync(cancellationToken);
        Debug.WriteLine($"after same action: {Environment.CurrentManagedThreadId}");

        Debug.WriteLine($"after fetch all in repo(): {Environment.CurrentManagedThreadId}");

        restaurantData.Data = resturants;

        return restaurantData;
    }

    public IQueryable<Restaurant> OdataResturants()
    {
        var query = base.Entity.AsQueryable();
        return query;
    }

    public IQueryable<RestaurantView> OdataResturantsWithView()
    {
        var query = _resturantView.Entity;
        return query;
    }
    public IQueryable<RestaurantView1> OdataResturantsWithAutomapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Schedule, MyClass>().ForMember(dto => dto.Day, conf => conf.MapFrom(ol => ol.Day.Name));
            cfg.CreateProjection<Restaurant, RestaurantView1>();
        });

        return base.Entity.ProjectTo<RestaurantView1>(configuration).AsQueryable();
    }
    public IQueryable<RestaurantData> OdataResturantsWithViewModel()
    {

        var query = base.Entity.ProjectToType<RestaurantData>();
        return query;
    }
}