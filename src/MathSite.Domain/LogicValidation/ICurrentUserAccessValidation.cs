using System;
using System.Threading.Tasks;

namespace MathSite.Domain.LogicValidation
{
	public interface ICurrentUserAccessValidation
	{
		/// <summary>
		///		Выполняет проверку существования текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		void CheckCurrentUserExistence(Guid currentUserId);

		/// <summary>
		///		Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		Task CheckCurrentUserRightsAsync(Guid currentUserId);
	}
}