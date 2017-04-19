using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class UserRightsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public UserRightsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "UserRights";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.UsersRights.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstUser = GetUserByLogin(UsersAliases.FirstUser);
			var secondUser = GetUserByLogin(UsersAliases.SecondUser);

			var adminAccess = GetRightByAlias(RightAliases.AdminAccess);
			var logoutAccess = GetRightByAlias(RightAliases.LogoutAccess);
			var panelAccess = GetRightByAlias(RightAliases.PanelAccess);

			var adminRights = new[]
			{
				CreateRights(false, firstUser, adminAccess),
				CreateRights(true, firstUser, logoutAccess),
				CreateRights(true, firstUser, panelAccess),
			};

			var userRights = new[]
			{
				CreateRights(false, secondUser, adminAccess),
				CreateRights(true, secondUser, logoutAccess),
				CreateRights(true, secondUser, panelAccess)
			};

			Context.UsersRights.AddRange(userRights);
			Dispose();

			Context.UsersRights.AddRange(adminRights);
			Dispose();
		}

		private Right GetRightByAlias(string alias)
		{
			return Context.Rights.First(right => right.Alias == alias);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(user => user.Login == login);
		}

		private static UsersRights CreateRights(bool allowed, User user, Right right)
		{
			return new UsersRights
			{
				Allowed = allowed,
				User = user,
				Right = right
			};
		}
	}
}