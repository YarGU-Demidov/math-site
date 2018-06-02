using System;

namespace MathSite.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T obj)
        {
            return ReferenceEquals(obj, null);
        }
        public static bool IsNotNull<T>(this T obj)
        {
            return !obj.IsNull();
        }
    }
}