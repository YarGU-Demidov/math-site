using System;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Facades.UserValidation;
using Xunit;

namespace MathSite.Tests.Facades
{
	public class UserValidationTests : FacadesTestsBase
	{
		public IUserValidationFacade GetFacade(IBusinessLogicManager manager)
		{
			return new UserValidationFacade(manager, MemoryCache);
		}

		[Fact]
		public async Task CheckRights_Fail()
		{
			await WithLogicAsync(async manager =>
			{
				var rightsValidator = GetFacade(manager);

				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.SecondUser);

				var groupRight = await manager.RightsLogic.TryGetByAliasAsync(RightAliases.AdminAccess);

				var hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight);
				Assert.False(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight.Alias);
				Assert.False(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user, groupRight);
				Assert.False(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user, groupRight.Alias);
				Assert.False(hasRight);
			});
		}

		[Fact]
		public async Task CheckRights_Success()
		{
			await WithLogicAsync(async manager =>
			{
				var rightsValidator = GetFacade(manager);

				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

				var groupRight = await manager.RightsLogic.TryGetByAliasAsync(RightAliases.PanelAccess);

				var hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight);
				Assert.True(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user.Id, groupRight.Alias);
				Assert.True(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user, groupRight);
				Assert.True(hasRight);

				hasRight = await rightsValidator.UserHasRightAsync(user, groupRight.Alias);
				Assert.True(hasRight);
			});
		}

		[Fact]
		public async Task UserDoesNotExistsTest()
		{
			await WithLogicAsync(async manager =>
			{
				Guid? userId = null;
				do
				{
					var tempId = Guid.NewGuid();
					var tempUser = await manager.UsersLogic.TryGetByIdAsync(tempId);

					if (tempUser == null)
						userId = tempId;
				} while (userId == null);

				var rightsValidator = GetFacade(manager);

				var exists = await rightsValidator.DoesUserExistsAsync(userId.Value);

				Assert.False(exists);
			});
		}

		[Fact]
		public async Task UserExistsTest()
		{
			await WithLogicAsync(async manager =>
			{
				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);
				var rightsValidator = GetFacade(manager);

				var exists = await rightsValidator.DoesUserExistsAsync(user.Id);

				Assert.True(exists);
			});
		}
	}
}