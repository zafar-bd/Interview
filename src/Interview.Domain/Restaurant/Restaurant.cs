namespace Interview.Domain.Restaurant
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Schedule> Schedules { get; set; } = new();
    }
}
