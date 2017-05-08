using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Models;

namespace MathSite.Domain.Logic.Users
{
    public interface IUsersLogic
    {
	    /// <summary>
	    /// Возвращает результат из перечня пользователей.
	    /// </summary>
	    /// <typeparam name="TResult">Тип результата.</typeparam>
	    /// <param name="getResult">Метод получения результата.</param>
	    TResult GetFromUsers<TResult>(Func<IQueryable<User>, TResult> getResult);

		/// <summary>
		/// Асинхронно возвращает результат из перечня пользователей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetFromUsersAsync<TResult>(Func<IQueryable<User>, Task<TResult>> getResult);

		/// <summary>
		/// Возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult);

		/// <summary>
		/// Асинхронно возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult);
	}
}
