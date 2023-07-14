using System.Globalization;

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
    }
}
