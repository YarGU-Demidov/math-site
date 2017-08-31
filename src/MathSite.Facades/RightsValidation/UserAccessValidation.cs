using System;
using System.Threading.Tasks;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Facades.RightsValidation
{
	public class UserAccessValidation : BaseFacade, IUserAccessValidation
	{
		private const string UserNotFoundFormat = "Текущий пользователь с Id='{0}' не найден";

		private const string UserDoNotHaveAdminRightsFormat =
			"Пользователь с Id='{0}' не имеет прав администратора на выполнение данной операции";
		
		public UserAccessValidation(IBusinessLogicManger logicManger)
			: base(logicManger)
		{
		}

		/*/// <summary>
		///     Выполняет проверку существования текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		public void CheckCurrentUserExistence(Guid currentUserId)
		{
			// TODO: IS THAT'S ALL??
			if (currentUserId == Guid.Empty)
				throw new SecurityException(string.Format(UserNotFoundFormat, currentUserId));
		}

		/// <summary>
		///     Асинхронно выполняет проверку прав текущего пользователя.
		/// </summary>
		/// <param name="currentUserId">Идентификатор текущего пользователя.</param>
		public async Task CheckCurrentUserRightsAsync(Guid currentUserId)
		{
			var isUserHaveAdminRights = await _contextManager.UsersRights
				.Where(i => i.UserId == currentUserId)
				.AnyAsync(i => i.Right.Alias == GroupAliases.Admin);

			if (!isUserHaveAdminRights)
				throw new Exception(string.Format(UserDoNotHaveAdminRightsFormat, currentUserId));
		}*/

		public async Task<bool> DoesUserExistsAsync(Guid userId)
		{
			return await LogicManger.UsersLogic
				.GetFromItemsAsync(users => users.AnyAsync(user => user.Id == userId));
		}

		public async Task<bool> CheckCurrentUserRightsAsync(Guid userId, Guid rightId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> CheckCurrentUserRightsAsync(Guid userId, string rightAlias)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> CheckCurrentUserRightsAsync(User user, Guid rightId)
		{
			return await CheckCurrentUserRightsAsync(user.Id, rightId);
		}

		public async Task<bool> CheckCurrentUserRightsAsync(User user, string rightAlias)
		{
			return await CheckCurrentUserRightsAsync(user.Id, rightAlias);
		}

		public async Task<bool> CheckCurrentUserRightsAsync(Guid userId, Right right)
		{
			return await CheckCurrentUserRightsAsync(userId, right.Id);
		}

		public async Task<bool> CheckCurrentUserRightsAsync(User user, Right right)
		{
			return await CheckCurrentUserRightsAsync(user.Id, right.Id);
		}
	}
}