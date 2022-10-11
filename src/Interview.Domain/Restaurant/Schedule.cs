using System.ComponentModel.DataAnnotations;

namespace Interview.Domain.Restaurant;

public sealed class Schedule
{
    public int RestaurantId { get; set; }
    public int DayId { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
    public Day Day { get; set; }
    public Restaurant Restaurant { get; set; }
}
