namespace Interview.Domain.Shared
{
    public class AppConstants
    {
        public const string DayRangePattern = @"((Mon{1}|Tues{1}|Wed{1}|Thurs{1}|Fri{1}|Sat{1}|Sun{1})-(Mon{1}|Tues{1}|Wed{1}|Thurs{1}|Fri{1}|Sat{1}|Sun{1}))";
        public const string DayPattern = @"(Mon{1}|Tues{1}|Wed{1}|Thurs{1}|Fri{1}|Sat{1}|Sun{1})";
        public const string TimePattern = @"(\d|\dd|:|\d|\dd|pm{1}|am{1}|-{1}|\d|\dd|:|\d|\dd|pm{1}|am{1})";
        public const string CommaSeparatedDays = "Mon,Tues,Wed,Thurs,Fri,Sat,Sun";
        public const string LocalDbConnectionStringName = "local";
    }
}
