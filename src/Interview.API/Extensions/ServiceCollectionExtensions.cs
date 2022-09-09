using Foundatio.Caching;
using Interview.Domain.Cache;
using Interview.Domain.Shared;

namespace Interview.API.Extensions
{
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
                options
                .UseSqlServer(configuration.GetConnectionString(AppConstants.LocalDbConnectionStringName), o => o.MigrationsAssembly("INterview"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ICacheClient, InMemoryCacheClient>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingMiddleware<,>));
            return services;
        }
    }
}