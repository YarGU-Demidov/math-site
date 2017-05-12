using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Models;

namespace MathSite.Domain.Logic.Users
{
	public interface IUsersLogic
	{
		/// <summary>
		///		Асинхронно создает пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="creationDate">Дата регистрации пользователя.</param>
		Task<Guid> CreateUserAsync(string login, string passwordHash, Guid groupId);

		/// <summary>
		///		Асинхронно обновляет пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="passwordHash">Пароль.</param>   
		Task UpdateUserAsync(Guid currentUserId, string passwordHash, Guid groupId);

		/// <summary>
		///		Асинхронно удаляет пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		Task DeleteUserAsync(Guid currentUserId, Guid personId);

		/// <summary>
		///		Возвращает результат из перечня пользователей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetFromUsers<TResult>(Func<IQueryable<User>, TResult> getResult);

		/// <summary>
		///		Асинхронно возвращает результат из перечня пользователей.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetFromUsersAsync<TResult>(Func<IQueryable<User>, Task<TResult>> getResult);

		/// <summary>
		///		Возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult);

		/// <summary>
		///		Асинхронно возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult);
	}
}