using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;

namespace MathSite.Domain.Common
{
	/// <summary>
	///     Базовый класс реализации слоя бизнес-логики.
	/// </summary>
	public abstract class LogicBase<TEntity>
		where TEntity : class
	{
		protected LogicBase(IMathSiteDbContext context)
		{
			ContextManager = context;
		}

		protected IMathSiteDbContext ContextManager { get; }

		/// <summary>
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResult">Метод получения результата.</param>
		protected virtual TResult GetFromItems<TResult>(
			Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities,
			Func<IQueryable<TEntity>, TResult> getResult
		)
		{
			return GetFromItems<TEntity, TResult>(getEntities, getResult);
		}

		protected virtual TResult GetFromItems<TMainEntity, TResult>(
			Func<IMathSiteDbContext, IQueryable<TMainEntity>> getEntities,
			Func<IQueryable<TMainEntity>, TResult> getResult
		) where TMainEntity : class
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
		///     Возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		protected virtual TResult GetFromItems<TResult>(
			Func<IQueryable<TEntity>, TResult> getResult
		)
		{
			return GetFromItems<TEntity, TResult>(getResult);
		}

		protected virtual TResult GetFromItems<TMainEntity, TResult>(Func<IQueryable<TMainEntity>, TResult> getResult)
			where TMainEntity : class
		{
			return GetFromItems(context => context.Set<TMainEntity>(), getResult);
		}

		/// <summary>
		///     Асинхронно возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getEntities">Метод получения перечней сущностей.</param>
		/// <param name="getResultAsync">Метод получения результата.</param>
		protected virtual async Task<TResult> GetFromItemsAsync<TResult>(
			Func<IMathSiteDbContext, IQueryable<TEntity>> getEntities,
			Func<IQueryable<TEntity>, Task<TResult>> getResultAsync
		)
		{
			var result = default(TResult);
			await UseContextAsync(async context =>
			{
				var entities = getEntities(context);
				result = await getResultAsync(entities);
			});
			return result;
		}

		/// <summary>
		///     Асинхронно возвращает результат из перечня элементов.
		/// </summary>
		/// <typeparam name="TEntity">Тип сущности.</typeparam>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResultAsync">Метод получения результата.</param>
		protected virtual async Task<TResult> GetFromItemsAsync<TResult>(
			Func<IQueryable<TEntity>, Task<TResult>> getResultAsync
		)
		{
			return await GetFromItemsAsync(context => context.Set<TEntity>(), getResultAsync);
		}

		/// <summary>
		///     Использование контекста базы данных.
		/// </summary>
		/// <param name="action">Метод использования.</param>
		protected void UseContext(Action<IMathSiteDbContext> action)
		{
			action(ContextManager);
		}

		/// <summary>
		///     Использование контекста базы данных и сохранение данных после этого.
		/// </summary>
		/// <param name="action">Метод использования.</param>
		protected void UseContextWithSave(Action<IMathSiteDbContext> action)
		{
			UseContext(action);
			ContextManager.SaveChanges();
		}

		/// <summary>
		///     Асинхронное использование контекста базы данных.
		/// </summary>
		/// <param name="asyncAction">Функция получения метода использования.</param>
		protected async Task UseContextAsync(Func<IMathSiteDbContext, Task> asyncAction)
		{
			await asyncAction(ContextManager);
		}

		/// <summary>
		///     Асинхронное использование контекста базы данных и сохранение данных после этого.
		/// </summary>
		/// <param name="asyncAction">Функция получения метода использования.</param>
		protected async Task UseContextWithSaveAsync(Func<IMathSiteDbContext, Task> asyncAction)
		{
			await UseContextAsync(asyncAction);
			await ContextManager.SaveChangesAsync();
		}
	}
}