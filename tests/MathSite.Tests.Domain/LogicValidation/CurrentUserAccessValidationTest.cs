using System;
using System.Linq;
using System.Security;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.LogicValidation;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Domain.LogicValidation
{
	public class CurrentUserAccessValidationTest
	{
		public CurrentUserAccessValidationTest()
		{
			_databaseFactory = new TestDatabaseFactory();
		}

		private readonly TestDatabaseFactory _databaseFactory;

		[Fact]
		public void UserExsistance_Exists()
		{
			_databaseFactory.ExecuteWithContext(context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				var user = context.Users.First();

				// NOTE: shouldn't throw any exception!
				validator.CheckCurrentUserExistence(user.Id);
			});
		}

		[Fact]
		public void UserExsistance_NotExists_EmptyGuid()
		{
			_databaseFactory.ExecuteWithContext(context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				Assert.Throws<SecurityException>(() => validator.CheckCurrentUserExistence(Guid.Empty));
			});
		}

		// TODO: FIXME!!!
		[Fact]
		public void UserExsistance_NotExists_NotEmptyGuid()
		{
			_databaseFactory.ExecuteWithContext(context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				var guid = GetUniqueGuid(context.Users);

				Assert.Throws<SecurityException>(() => validator.CheckCurrentUserExistence(guid));
			});
		}

		[Fact]
		public async void CurrentUser_HasRights_Test()
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				var userId = await context.Users
					.Where(user => user.Login == UsersAliases.FirstUser)
					.Select(user => user.Id)
					.FirstAsync();

				// NOTE: shouldn't throw any exception!
				await validator.CheckCurrentUserRightsAsync(userId);
			});
		}

		[Fact]
		public async void CurrentUser_HasNotRights_Test()
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				var userId = await context.Users
					.Where(user => user.Login == UsersAliases.ThirdUser)
					.Select(user => user.Id)
					.FirstAsync();

				await Assert.ThrowsAsync<Exception>(async () => await validator.CheckCurrentUserRightsAsync(userId));
			});
		}

		[Fact]
		public async void CurrentUser_IsNotUser_Test()
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				var validator = new CurrentUserAccessValidation(context);

				var uniqGuid = GetUniqueGuid(context.Users);

				await Assert.ThrowsAsync<SecurityException>(async () => await validator.CheckCurrentUserRightsAsync(Guid.Empty));
				await Assert.ThrowsAsync<SecurityException>(async () => await validator.CheckCurrentUserRightsAsync(uniqGuid));
			});
		}

		private static Guid GetUniqueGuid(IQueryable<User> users)
		{
			var guid = Guid.NewGuid();

			do
			{
				if (!users.Any(user => user.Id == guid))
					break;

				guid = Guid.NewGuid();
			} while (true);

			return guid;
		}

		// TODO: WRITE MORE TESTS!!!
	}
}