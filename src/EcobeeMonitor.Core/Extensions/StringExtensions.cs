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
    }
}
