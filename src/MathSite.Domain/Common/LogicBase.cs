using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;

namespace MathSite.Domain.Common
{
	/// <summary>
	/// Базовый класс реализации слоя бизнес-логики.
	/// </summary>
	public class LogicBase
	{
		private readonly IMathSiteDbContext _contextManager;

		protected LogicBase(IMathSiteDbContext contextManager)
		{
			_contextManager = contextManager;
		}

		/// <summary>
		/// Использование контекста базы данных.
		/// </summary>
		/// <param name="action">Метод использования.</param>
		protected void UseContext(Action<IMathSiteDbContext> action)
		{
			action(_contextManager);
		}

		/// <summary>
		/// Асинхронное использование контекста базы данных.
		/// </summary>
		/// <param name="asyncAction">Функция получения метода использования.</param>
		protected async Task UseContextAsync(Func<IMathSiteDbContext, Task> asyncAction)
		{
			await asyncAction(_contextManager);
		}

		/// <summary>
		/// Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResult">Метод получения результата.</param>
		protected TResult GetFromItems<TEntity, TResult>(Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities,
			Func<IQueryable<TEntity>, TResult> getResult)
		{
			var result = default(TResult);
			UseContext(context =>
			{
				var entities = getEntities(context);
				result = getResult(entities);
			});
			return result;
		}

		/// <summary>
		/// Асинхронно возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResultAsync">Метод получения результата.</param>
		protected async Task<TResult> GetFromItemsAsync<TEntity, TResult>(
			Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities, Func<IQueryable<TEntity>, Task<TResult>> getResultAsync)
		{
			var result = default(TResult);
			await UseContextAsync(async context =>
			{
				var entities = getEntities(context);
				result = await getResultAsync(entities);
			});
			return result;
		}
	}
}