using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.UserValidation
{
	public class UserValidationFacade : BaseFacade, IUserValidationFacade
	{
		public UserValidationFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache)
			: base(logicManager, memoryCache)
		{
		}

		public async Task<bool> DoesUserExistsAsync(Guid userId)
		{
			var user = await LogicManager.UsersLogic.TryGetByIdAsync(userId);
			return user != null;
		}

		public async Task<bool> UserHasRightAsync(Guid userId, string rightAlias)
		{
			if (userId == Guid.Empty)
				return false;

			var user = await LogicManager.UsersLogic.TryGetUserWithRightsById(userId);

			if (user == null)
				return false;

			var userRights = user.UserRights.ToArray();
			var groupRights = user.Group.GroupsRights.ToArray();

			var allowed = user.Group.IsAdmin;

			if (userRights.All(rights => rights.Right.Alias != rightAlias) && !user.Group.IsAdmin)
			{
				foreach (var groupRight in groupRights)
					if (groupRight.Right.Alias == rightAlias)
						allowed = groupRight.Allowed;

				return allowed;
			}

			foreach (var userRight in userRights)
				if (userRight.Right.Alias == rightAlias)
					allowed = userRight.Allowed;

			return allowed;
		}

		public async Task<bool> UserHasRightAsync(User user, string rightAlias)
		{
			return await UserHasRightAsync(user.Id, rightAlias);
		}

		public async Task<bool> UserHasRightAsync(Guid userId, Right right)
		{
			return await UserHasRightAsync(userId, right.Alias);
		}

		public async Task<bool> UserHasRightAsync(User user, Right right)
		{
			return await UserHasRightAsync(user.Id, right.Alias);
		}
	}
}