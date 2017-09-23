using System;
using System.Linq;
using System.Linq.Expressions;

namespace MathSite.Common
{
    /// <summary>
    ///     Дополнительные методы для LINQ
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        ///     Упорядочить записи по возрастанию или убыванию.
        /// </summary>
        /// <typeparam name="TSource">Тип исходных данных.</typeparam>
        /// <typeparam name="TKey">Тип данных сортируемого элемента.</typeparam>
        /// <param name="query">Исходный набор данных.</param>
        /// <param name="keySelector">Ссылка на метод сортировщика.</param>
        /// <param name="isAscending">True, если по возрастанию, иначе - false.</param>
        /// <returns>Упорядоченный по возрастанию или убыванию набор данных.</returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector, bool isAscending)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            return isAscending
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);
        }

        /// <summary>
        ///     Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        /// <typeparam name="TSource">Тип исходных данных.</typeparam>
        /// <param name="query">Исходный набор данных.</param>
        /// <param name="skipCount">Сколько записей пропустить.</param>
        /// <param name="maxResultCount">Сколько выбрать.</param>
        public static IQueryable<TSource> PageBy<TSource>(this IQueryable<TSource> query, int skipCount, int maxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}