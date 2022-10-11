using System.ComponentModel.DataAnnotations;

namespace Interview.Domain.Restaurant;

public sealed class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
    public List<Schedule> Schedules { get; set; } = new();
}
