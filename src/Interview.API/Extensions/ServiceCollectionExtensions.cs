using Foundatio.Caching;
using Interview.Domain.Cache;
using Interview.Domain.Shared;

namespace Interview.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
            .AddScoped<IRestaurantRepository, RestaurantRepository>();
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services
        , IConfiguration configuration)
    {
        return services.AddDbContext<RestaurantEFContext>(options =>
        {
            var conStr = configuration.GetConnectionString(AppConstants.LocalDbConnectionStringName);
            options
            .UseSqlServer(conStr)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddSingleton<ICacheClient, InMemoryCacheClient>();
        //services.AddTransient<ServiceResolver>(serviceProvider => key =>
        //{
        //    switch (key)
        //    {
        //        case "A":
        //             return serviceProvider.GetService<InMemoryCacheClient>();
        //        case "B":
        //            return serviceProvider.GetService<InMemoryCacheClient>();
        //        case "C":
        //            return serviceProvider.GetService<InMemoryCacheClient>();
        //        default:
        //            throw new KeyNotFoundException(); // or maybe return null, up to you
        //    }
        //});
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingMiddleware<,>));
        return services;
    }

    public delegate ICacheClient ServiceResolver(string key);
}