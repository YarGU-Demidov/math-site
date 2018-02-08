namespace MathSite.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
    }
}