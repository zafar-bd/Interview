namespace Interview.Domain.Restaurant
{
    public class Schedule
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int DayId { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public Day Day { get; set; } = new();
        public Restaurant Restaurant { get; set; } = new();
    }
}
