namespace Interview.Infrastructure.Seed;

public class Sample
{
    public string Key { get; set; }
    public string Schedule { get; set; }

}

public class Deficiencies
{
    public string ApplicationType { get; set; }
    public string DeficiencyType { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
public static class Week
{
    public static List<Day> GetDays(bool isFromDataSeed)
    {
        List<Day> days = new();
        var splittedDays = AppConstants.CommaSeparatedDays.Split(',');
        for (int i = 0; i < splittedDays.Length; i++)
        {
            days.Add(new()
            {
                Id = isFromDataSeed ? 0 : i + 1,
                Name = splittedDays[i]
            });
        }

        return days;
    }
}
