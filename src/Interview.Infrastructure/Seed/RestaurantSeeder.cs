namespace Interview.Infrastructure.Seed;

public static class RestaurantSeeder
{
    public static IServiceProvider SeedData(this IServiceProvider services)
    {
        var scopedFactory = services.GetService<IServiceScopeFactory>();

        using (var scope = scopedFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<RestaurantEFContext>();
            var dayRepo = scope.ServiceProvider.GetService<IAsyncRepository<Day>>();
            var restaurantRepo = scope.ServiceProvider.GetService<IAsyncRepository<Restaurant>>();

            if (!dayRepo.Entity.Any())
            {
                dayRepo.Entity.AddRange(Week.GetDays(true));
                context.SaveChanges();
            }

            if (!restaurantRepo.Entity.Any())
            {
                restaurantRepo.Entity.AddRange(DataReader.RetrieveSampleData());
                context.SaveChanges();
            }
        }
        return services;
    }
}
