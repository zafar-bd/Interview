namespace Infrastructure.Data;

public class RestaurantEFContext : DbContext
{
    public RestaurantEFContext(DbContextOptions<RestaurantEFContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}