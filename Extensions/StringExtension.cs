using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace WMKancelariapp.Extensions
{
    public static class StringExtension
    {
        public static string ToTitleCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        public static string Truncate(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var textEnd = "...";
            if (maxLength >= text.Length)
            {
                maxLength = text.Length;
                textEnd = string.Empty;
            }

            var result = text.Remove(maxLength > text.Length ? text.Length : maxLength) + textEnd;
            return result;
        }

        public static string RemoveEmailDomain(this string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
            {
                return email;
            }
            return email.Remove(email.IndexOf('@'));
        }

        public static string ConvertTimeToMinutes(this string value)
        {
            if (value != null && value.Count(x => x == ':') == 1)
            {
                var time = value.Split(':');
                var hour = int.TryParse(time[0], out int hours);
                var minute = int.TryParse(time[1], out int minutes);

                return (hour && minute) ? (hours * 60 + minutes).ToString() : "Invalid input";
            }

            return value;
        }

        public static string ConvertMinutesToStringDisplay(this string durationMinutes)
        {
            const int minutesInDay = 24*60;
            
            if (durationMinutes.IsNullOrEmpty())
            {
                durationMinutes = "0";
            }
            
            var days = int.Parse(durationMinutes) / minutesInDay;
            var daysString = days > 0 ? days.ToString() : "";
            var daysText = days switch
            {
                0 => "",
                1 => " dzień ",
                _ => " dni "
            };
            var duration = TimeSpan.FromMinutes(double.Parse(durationMinutes) - days * 60 * 24).Ticks;
            var durationString = duration == 0 ? "" : TimeSpan.FromTicks(duration).ToString("hh\\:mm");

            var result = durationMinutes != "0"
                ? $"{daysString}{daysText}{durationString}"
                : "-";

            return result;
        }
    }
}
