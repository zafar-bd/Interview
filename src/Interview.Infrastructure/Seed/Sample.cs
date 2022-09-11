﻿using CsvHelper;

namespace Interview.Infrastructure.Seed;

public class Sample
{
    public string Key { get; set; }
    public string Schedule { get; set; }

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
