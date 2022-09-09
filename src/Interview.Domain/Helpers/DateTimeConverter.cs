using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Interview.Domain.Helpers
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        protected const string DateFormat = "yyyy-MM-dd";
        protected const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
        protected const string FullDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
        protected const string LegacyDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        protected const string LegacyDateTimeWithZoneFormat = "yyyy-MM-dd HH:mm:ss.fffffffZ";

        public DateTimeConverter()
        {
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime value;
            if (reader.TryGetDateTime(out value))
                return value;

            var dateTimeAsString = reader.GetString();

            if (DateTime.TryParseExact(dateTimeAsString, DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            if (DateTime.TryParseExact(dateTimeAsString, FullDateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            if (DateTime.TryParseExact(dateTimeAsString, LegacyDateTimeFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            if (DateTime.TryParseExact(dateTimeAsString, LegacyDateTimeWithZoneFormat, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out value))
                return value;

            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateTimeFormat));
        }
    }
}
