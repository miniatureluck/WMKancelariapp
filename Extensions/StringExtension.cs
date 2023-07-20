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
    }
}
