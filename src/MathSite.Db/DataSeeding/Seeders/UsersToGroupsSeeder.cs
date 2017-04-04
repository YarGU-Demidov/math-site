using System.Linq;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class UsersToGroupsSeeder: AbstractSeeder
	{
		/// <inheritdoc />
		public UsersToGroupsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Users to Groups";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Users.Any();
		}

		/// <inheritdoc />
		protected override bool ShouldSeed()
		{
			return Context.Groups.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminGroup = GetGroupByAlias(GroupsAliases.Admin);
			var usersGroup = GetGroupByAlias(GroupsAliases.User);

			var mokeev1995 = GetUserByLogin("mokeev1995");
			var testUser = GetUserByLogin("test");

			mokeev1995.Group = adminGroup;
			testUser.Group = usersGroup;
		}

		private Group GetGroupByAlias(string alias)
		{
			return Context.Groups.First(group => group.Alias == alias);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(user => user.Login == login);
		}
	}
}