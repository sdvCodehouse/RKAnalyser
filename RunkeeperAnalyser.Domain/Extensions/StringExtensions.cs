using System;

namespace RunkeeperAnalyser.Domain.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string value)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out result))
                return result;

            return null;
        }

        public static DateTime? ToNullableDateTime(this string value)
        {
            DateTime result;
            if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out result))
                return result;

            return null;
        }
    }
}
