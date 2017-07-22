using System;
using System.Linq;
using System.Security;
using MathSite.Domain.LogicValidation;
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

				// shouldn't throw any exception!
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

				var guid = Guid.NewGuid();

				do
				{
					if (!context.Users.Any(user => user.Id == guid))
						break;

					guid = Guid.NewGuid();
				} while (true);

				Assert.Throws<SecurityException>(() => validator.CheckCurrentUserExistence(guid));
			});
		}

		// TODO: WRITE MORE TESTS!!!
	}
}