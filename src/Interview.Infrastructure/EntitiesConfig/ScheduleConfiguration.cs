namespace Interview.Infrastructure.EntitiesConfig
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(r => new { r.DayId, r.RestaurantId });
            builder.Property(r => r.RowVersion).IsConcurrencyToken();
        }
    }
}
