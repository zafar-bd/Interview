namespace Interview.Domain.ViewModel
{
    public class RestaurantScheduleViewModel
    {
        public int Count { get; set; }
        public RestaurantData[] Data { get; set; }

    }

    public class RestaurantData
    {
        public RestaurantBasicData Restaurant { get; set; }
        public ScheduleViewModel[] Schedules { get; set; }
    }

    public class RestaurantBasicData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DayViewModel Day { get; set; } = new();
    }

    public class DayViewModel
    {
        public string Name { get; set; }
    }
}
