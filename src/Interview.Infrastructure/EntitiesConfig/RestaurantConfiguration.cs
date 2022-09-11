namespace Interview.Infrastructure.EntitiesConfig;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(r => r.Name).IsUnique();
        builder.Property(r => r.RowVersion).IsConcurrencyToken();
    }
}
