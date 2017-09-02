using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Users
{
	public interface IUsersLogic
	{
		/// <summary>
		///     Асинхронно создает пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="passwordHash">Пароль.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		Task<Guid> CreateUserAsync(string login, byte[] passwordHash, Guid groupId);

		/// <summary>
		///     Асинхронно обновляет пользователя.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <param name="groupId">Идентификатор группы.</param>
		/// <param name="passwordHash">Пароль.</param>
		Task UpdateUserAsync(Guid id, byte[] passwordHash, Guid groupId);

		/// <summary>
		///     Асинхронно удаляет пользователя.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		Task DeleteUserAsync(Guid id);

		Task<User> TryGetByIdAsync(Guid userId);

		Task<User> TryGetByLoginAsync(string login);

		Task<User> TryGetUserWithRightsById(Guid id);

		Task<User> TryGetUserWithRightsByLogin(string login);
	}
}