namespace Interview.Infrastructure.Seed;

public class DataReader
{
    public static IEnumerable<Restaurant> RetrieveSampleData()
    {
        IEnumerable<Sample> records = ReadCsv();
        List<Restaurant> restaurants = new();
        foreach (var groupedRecord in records.GroupBy(s => s.Key))
        {
            Restaurant resturant = new()
            {
                Name = groupedRecord.Key
            };

            foreach (string scheduleInput in groupedRecord.Select(g => g.Schedule))
            {
                var schedules = scheduleInput.Split('/');
                foreach (var sch in schedules)
                {
                    BuildSchedules(resturant, sch);
                }
            }
            restaurants.Add(resturant);
        }

        return restaurants;
    }

    private static IEnumerable<Sample> ReadCsv()
    {
        CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using StreamReader reader = new("Seed/data.csv");
        using CsvReader csv = new(reader, config);
        var records = csv.GetRecords<Sample>().ToList();
        return records;
    }

    private static (TimeSpan StartTime, TimeSpan EndTime) ParseTime(string input)
    {
        StringBuilder timeValue = new();

        foreach (Match m in Regex.Matches(input.Replace(" ", ""), AppConstants.TimePattern))
            timeValue.Append(m.Value);

        var times = timeValue.ToString().Split('-').Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
        var startTime = DateTime.Parse(times[0]).TimeOfDay;
        var endTime = DateTime.Parse(times[1]).TimeOfDay;

        return (startTime, endTime);
    }

    private static Schedule BuildSchedule(int dayId, string timeString)
    {
        timeString = Regex.Replace(timeString, AppConstants.DayPattern, "");
        Schedule schedule = new()
        {
            DayId = dayId
        };

        var (startTime, endTime) = ParseTime(timeString);
        schedule.Start = startTime;
        schedule.End = endTime;

        return schedule;
    }

    private static Restaurant BuildSchedules(Restaurant restaurant, string shedule)
    {
        var daysFromStorage = Week.GetDays(false);
        shedule = shedule.Replace(" ", "");

        if (Regex.IsMatch(shedule, AppConstants.DayRangePattern))
        {
            BuildSchedules(restaurant, shedule, daysFromStorage);
        }

        shedule = Regex.Replace(shedule, AppConstants.DayRangePattern, "");
        if (!Regex.IsMatch(shedule, AppConstants.DayPattern))
            return restaurant;

        BuildScheduleDay(restaurant, shedule, daysFromStorage);

        return restaurant;
    }

    private static void BuildSchedules(Restaurant restaurant, string shedule, List<Day> daysFromStorage)
    {
        foreach (Match m in Regex.Matches(shedule, AppConstants.DayRangePattern))
        {
            var dayRange = m.Value.Split("-").ToList();
            for (int i = 0; i < dayRange.Count; i++)
            {
                string startDay = dayRange[i];
                Day? startDayFromStorage = daysFromStorage.FirstOrDefault(d => d.Name == startDay && !restaurant.Schedules.Any(f => f.DayId == d.Id));
                if (!string.IsNullOrWhiteSpace(startDayFromStorage?.Name))
                {
                    Day? nextDay = daysFromStorage.FirstOrDefault(d => d.Name == dayRange[i + 1]);

                    var allDays = new List<Day>();

                    if (startDayFromStorage.Id > nextDay?.Id)
                        allDays = daysFromStorage.Where(d => d.Id <= startDayFromStorage?.Id && d.Id >= nextDay?.Id).ToList();
                    else
                        allDays = daysFromStorage.Where(d => d.Id >= startDayFromStorage?.Id && d.Id <= nextDay?.Id).ToList();

                    foreach (var day in allDays)
                    {
                        var schedule = BuildSchedule(day.Id, shedule.ToString());
                        restaurant.Schedules.Add(schedule);
                    }
                }
            }
        }
    }

    private static void BuildScheduleDay(Restaurant restaurant, string shedule, List<Day> daysFromStorage)
    {
        foreach (Match m in Regex.Matches(shedule, AppConstants.DayPattern))
        {
            var days = m.Value.Split(" ").Where(d => !string.IsNullOrWhiteSpace(d)).ToList();
            for (int i = 0; i < days.Count; i++)
            {
                string currentDay = days[i];
                Day? currentDayFromStorage = daysFromStorage.FirstOrDefault(d => d.Name == currentDay && !restaurant.Schedules.Any(f => f.DayId == d.Id));
                if (!string.IsNullOrWhiteSpace(currentDayFromStorage?.Name))
                {
                    var schedule = BuildSchedule(currentDayFromStorage.Id, shedule.ToString());
                    restaurant.Schedules.Add(schedule);
                }
            }
        }
    }
}
