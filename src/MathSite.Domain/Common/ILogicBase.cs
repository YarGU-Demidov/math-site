using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;

namespace MathSite.Domain.Common
{
	public interface ILogicBase<TEntity> where TEntity : class
	{
		/// <summary>
		///     Асинхронно возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResultAsync">Метод получения результата.</param>
		Task<TResult> GetFromItemsAsync<TResult>(
			Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities,
			Func<IQueryable<TEntity>, Task<TResult>> getResultAsync
		);

		/// <summary>
		///     Асинхронно возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResultAsync">Метод получения результата.</param>
		Task<TResult> GetFromItemsAsync<TResult>(
			Func<IQueryable<TEntity>, Task<TResult>> getResultAsync
		);

		/// <summary>
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromItems<TResult>(
			Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities,
			Func<IQueryable<TEntity>, TResult> getResult
		);

		/// <summary>
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TMainEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromItems<TMainEntity, TResult>(
			Func<IMathSiteDbContext, IQueryable<TMainEntity>> getEntities,
			Func<IQueryable<TMainEntity>, TResult> getResult
		) where TMainEntity : class;

		/// <summary>
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromItems<TResult>(
			Func<IQueryable<TEntity>, TResult> getResult
		);

		/// <summary>
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TMainEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromItems<TMainEntity, TResult>(
			Func<IQueryable<TMainEntity>, TResult> getResult
		) where TMainEntity : class;
	}
}