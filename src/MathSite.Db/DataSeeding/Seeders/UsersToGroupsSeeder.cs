using System.Linq;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	using StaticData;

	/// <inheritdoc />
	public class UsersToGroupsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public UsersToGroupsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "UsersToGroups";

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
			var adminGroup = GetGroupByAlias(GroupAliases.Admin);
			var usersGroup = GetGroupByAlias(GroupAliases.User);

			var firstUser = GetUserByLogin("mokeev1995");
			var secondUser = GetUserByLogin("test");

			firstUser.Group = adminGroup;
			secondUser.Group = usersGroup;
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