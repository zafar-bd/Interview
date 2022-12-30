namespace Interview.Domain.Restaurant
{
    [Keyless]
    public class RestaurantView
    {
        public string Name { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Day { get; set; }
    }
}
