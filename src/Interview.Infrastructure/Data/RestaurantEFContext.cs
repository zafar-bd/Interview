namespace Infrastructure.Data;

public class RestaurantEFContext : DbContext
{
    public RestaurantEFContext(DbContextOptions<RestaurantEFContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RestaurantView>().ToView("Vw_Restaurant");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());        
    }
}