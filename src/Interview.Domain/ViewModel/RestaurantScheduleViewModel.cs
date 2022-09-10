namespace Interview.Domain.ViewModel
{
    public class RestaurantScheduleViewModel
    {
        public int Count { get; set; }
        public RestaurantData[] Data { get; set; }

    }

    public class RestaurantData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ScheduleViewModel[] Schedules { get; set; }
    }

    public class ScheduleViewModel
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public DayViewModel Day { get; set; } = new();
    }

    public class DayViewModel
    {
        public string Name { get; set; }
    }
}
