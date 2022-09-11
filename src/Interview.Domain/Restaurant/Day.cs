namespace Interview.Domain.Restaurant;

public class Day
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Schedule> Schedules { get; set; } = new();
}
