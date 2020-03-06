using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MathSite.Common.Extensions
{
    /// <summary>
    ///     Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source.IsNull() || source.Count <= 0;
        }

        /// <summary>
        ///     Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            var sourceArray = source as T[] ?? source?.ToArray();

            return sourceArray.IsNull() || !sourceArray.Any();
        }

        /// <summary>
        ///     Checks whatever given collection object is not null or has item.
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return !source.IsNullOrEmpty();
        }

        /// <summary>
        ///     Checks whatever given collection object is not null or has no item.
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> source)
        {
            return !source.IsNullOrEmpty();
        }

        /// <summary>
        ///     Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Contains(item))
                return false;

            source.Add(item);
            return true;
        }
    }
}