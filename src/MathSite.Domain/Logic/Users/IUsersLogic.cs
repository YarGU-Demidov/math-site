using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Domain.Common;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Users
{
	public interface IUsersLogic : ILogicBase<User>
	{
		/// <summary>
		///     Асинхронно создает пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="creationDate">Дата регистрации пользователя.</param>
		Task<Guid> CreateUserAsync(string login, byte[] passwordHash, Guid groupId);

		/// <summary>
		///     Асинхронно обновляет пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="passwordHash">Пароль.</param>
		Task UpdateUserAsync(Guid currentUserId, byte[] passwordHash, Guid groupId);

		/// <summary>
		///     Асинхронно удаляет пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		/// <param name="personId">Идентификатор личности.</param>
		Task DeleteUserAsync(Guid currentUserId, Guid personId);

		/// <summary>
		///     Возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		TResult GetUserRights<TResult>(Func<IQueryable<UsersRights>, TResult> getResult);

		/// <summary>
		///     Асинхронно возвращает результат из перечня прав пользователя.
		/// </summary>
		/// <typeparam name="TResult">Тип результата.</typeparam>
		/// <param name="getResult">Метод получения результата.</param>
		Task<TResult> GetUserRightsAsync<TResult>(Func<IQueryable<UsersRights>, Task<TResult>> getResult);
	}
}