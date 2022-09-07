namespace Interview.Infrastructure.EntitiesConfig
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasIndex(r => new { r.DayId, r.RestaurantId }).IsUnique();
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
        }
    }
}
