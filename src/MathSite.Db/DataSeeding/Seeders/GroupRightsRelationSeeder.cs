using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class GroupRightsRelationSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public GroupRightsRelationSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Group Rights Relations";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.GroupsRights.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminGroup = GetGroupByAlias(GroupsAliases.Admin);
			var usersGroup = GetGroupByAlias(GroupsAliases.User);

			var adminAccessRight = GetRightByAlias(RightsAliases.AdminAccess);
			var logoutAccess = GetRightByAlias(RightsAliases.LogoutAccess);
			var panelAccess = GetRightByAlias(RightsAliases.PanelAccess);

			var adminRights = new[]
			{
				CreateGroupsRights(true, adminGroup, adminAccessRight),
				CreateGroupsRights(true, adminGroup, logoutAccess),
				CreateGroupsRights(true, adminGroup, panelAccess),
			};

			var usersRights = new[]
			{
				CreateGroupsRights(false, usersGroup, adminAccessRight),
				CreateGroupsRights(true, usersGroup, logoutAccess),
				CreateGroupsRights(true, usersGroup, panelAccess)
			};

			Context.GroupsRights.AddRange(usersRights);
			Dispose();

			Context.GroupsRights.AddRange(adminRights);
			Dispose();
		}

		private GroupsRights CreateGroupsRights(bool allowed, Group group, Right right)
		{
			return new GroupsRights
			{
				Allowed = allowed,
				Group = group,
				Right = right
			};
		}

		private Right GetRightByAlias(string alias)
		{
			return Context.Rights.First(right => right.Alias == alias);
		}

		private Group GetGroupByAlias(string alias)
		{
			return Context.Groups.First(group => @group.Alias == alias);
		}
	}
}