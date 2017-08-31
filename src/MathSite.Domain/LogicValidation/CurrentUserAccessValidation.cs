using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.LogicValidation
{
	public class CurrentUserAccessValidation : ICurrentUserAccessValidation
	{
		private const string UserNotFoundFormat = "Текущий пользователь с Id='{0}' не найден";

		private const string UserDoNotHaveAdminRightsFormat =
			"Пользователь с Id='{0}' не имеет прав администратора на выполнение данной операции";

		private readonly IMathSiteDbContext _contextManager;

		public CurrentUserAccessValidation(IMathSiteDbContext context)
		{
			_contextManager = context;
		}

		/// <summary>
		///		Выполняет проверку существования текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		public void CheckCurrentUserExistence(Guid currentUserId)
		{
			// TODO: IS THAT'S ALL??
			if (currentUserId == Guid.Empty)
				throw new SecurityException(string.Format(UserNotFoundFormat, currentUserId));
		}

		/// <summary>
		///		Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		public async Task CheckCurrentUserRightsAsync(Guid currentUserId)
		{
			var isUserHaveAdminRights = await _contextManager.UsersRights
				.Where(i => i.UserId == currentUserId)
				.AnyAsync(i => i.Right.Alias == GroupAliases.Admin);

			if (!isUserHaveAdminRights)
				throw new Exception(string.Format(UserDoNotHaveAdminRightsFormat, currentUserId));
		}
	}
}