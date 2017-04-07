using System;
using System.Linq;
using System.Linq.Expressions;

namespace MathSite.Common
{
	/// <summary>
	///		Дополнительные методы для LINQ
	/// </summary>
	public static class LinqExtension
	{
		/// <summary>
		///		Упорядочить записи по возрастанию или убыванию
		/// </summary>
		/// <typeparam name="TSource">Тип исходных данных</typeparam>
		/// <typeparam name="TKey">Тип данных сортируемого элемента</typeparam>
		/// <param name="source">Исходный набор данных</param>
		/// <param name="keySelector">Ссылка на метод сортировщика</param>
		/// <param name="isAscending">True, если по возрастанию, иначе -- false</param>
		/// <returns>Упорядоченный по возрастанию или убыванию набор данных</returns>
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source,
			Expression<Func<TSource, TKey>> keySelector, bool isAscending)
		{
			return isAscending 
				? source.OrderBy(keySelector)
				: source.OrderByDescending(keySelector);
		}
	}
}