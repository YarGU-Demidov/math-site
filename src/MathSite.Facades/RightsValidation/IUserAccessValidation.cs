using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.RightsValidation
{
	public interface IUserAccessValidation
	{
		/// <summary>
		///     Выполняет проверку существования текущего пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор текущего пользователя.</param>
		Task<bool> DoesUserExistsAsync(Guid userId);

		/// <summary>
		///     Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="rightId">ID права.</param>
		Task<bool> CheckCurrentUserRightsAsync(Guid userId, Guid rightId);

		/// <summary>
		///     Асинхронно выполняет проверку прав пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="rightAlias">Alias права.</param>
		Task<bool> CheckCurrentUserRightsAsync(Guid userId, string rightAlias);

		/// <summary>
		///     Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="user">Пользователь.</param>
		/// <param name="rightId">ID права.</param>
		Task<bool> CheckCurrentUserRightsAsync(User user, Guid rightId);

		/// <summary>
		///     Асинхронно выполняет проверку прав пользователя.
		/// </summary>
		/// <param name="user">Пользователь.</param>
		/// <param name="rightAlias">Alias права.</param>
		Task<bool> CheckCurrentUserRightsAsync(User user, string rightAlias);

		/// <summary>
		///     Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="right">Право.</param>
		Task<bool> CheckCurrentUserRightsAsync(Guid userId, Right right);

		/// <summary>
		///     Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="user">Пользователь.</param>
		/// <param name="right">Право.</param>
		Task<bool> CheckCurrentUserRightsAsync(User user, Right right);
	}
}