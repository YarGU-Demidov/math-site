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
			using (_databaseFactory)
			using (var context = _databaseFactory.GetContext())
			{
				var validator = new CurrentUserAccessValidation(context);

				var user = context.Users.First();

				// shouldn't throw any exception!
				validator.CheckCurrentUserExistence(user.Id);
			}
		}

		[Fact]
		public void UserExsistance_NotExists_EmptyGuid()
		{
			using (_databaseFactory)
			using (var context = _databaseFactory.GetContext())
			{
				var validator = new CurrentUserAccessValidation(context);

				Assert.Throws<SecurityException>(() => validator.CheckCurrentUserExistence(Guid.Empty));
			}
		}

		// TODO: FIXME!!!
		[Fact]
		public void UserExsistance_NotExists_NotEmptyGuid()
		{
			using (_databaseFactory)
			using (var context = _databaseFactory.GetContext())
			{
				var validator = new CurrentUserAccessValidation(context);

				Assert.Throws<SecurityException>(() => validator.CheckCurrentUserExistence(Guid.NewGuid()));
			}
		}

		// TODO: WRITE MORE TESTS!!!
	}
}