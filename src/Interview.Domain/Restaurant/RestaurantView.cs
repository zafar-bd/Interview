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

    public class RestaurantView1
    {
        public string Name { get; set; }
        public List<MyClass> Schedules { get; set; }
    }

    public class MyClass
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Day { get; set; }
    }
}
