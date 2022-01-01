using System.Collections.Generic;

namespace EcobeeMonitor.Core.Extensions
{
    public static class StringExtensions
    {
        public static double? ToDouble(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            if(double.TryParse(value, out var result)) return result;

            return null;
        }

        public static string GetColumnValue(this string[] data, string field, Dictionary<string, int> positions)
        {
            if (positions.TryGetValue(field, out var index))
            {
                return data[index];
            }
            return null;
        }
    }
}
