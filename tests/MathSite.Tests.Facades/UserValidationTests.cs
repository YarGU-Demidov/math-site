using System;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.UserValidation;
using Xunit;

namespace MathSite.Tests.Facades
{
	public class UserValidationTests : FacadesTestsBase
	{
		[Fact]
		public async Task CheckRights_Fail()
		{
			await WithLogicAsync(async manger =>
			{
				var rightsValidator = new UserValidationFacade(manger);

				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.SecondUser);

				var groupRight = await manger.RightsLogic.TryGetByAliasAsync(RightAliases.AdminAccess);

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
			await WithLogicAsync(async manger =>
			{
				var rightsValidator = new UserValidationFacade(manger);

				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

				var groupRight = await manger.RightsLogic.TryGetByAliasAsync(RightAliases.PanelAccess);

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
			await WithLogicAsync(async manger =>
			{
				Guid? userId = null;
				do
				{
					var tempId = Guid.NewGuid();
					var tempUser = await manger.UsersLogic.TryGetByIdAsync(tempId);

					if (tempUser == null)
						userId = tempId;
				} while (userId == null);

				var rightsValidator = new UserValidationFacade(manger);

				var exists = await rightsValidator.DoesUserExistsAsync(userId.Value);

				Assert.False(exists);
			});
		}

		[Fact]
		public async Task UserExistsTest()
		{
			await WithLogicAsync(async manger =>
			{
				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);
				var rightsValidator = new UserValidationFacade(manger);

				var exists = await rightsValidator.DoesUserExistsAsync(user.Id);

				Assert.True(exists);
			});
		}
	}
}