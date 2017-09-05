using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class GroupRightsSeeder : AbstractSeeder<GroupsRight>
	{
		/// <inheritdoc />
		public GroupRightsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(GroupsRight);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminGroup = GetGroupByAlias(GroupAliases.Admin);
			var usersGroup = GetGroupByAlias(GroupAliases.User);

			var adminAccessRight = GetRightByAlias(RightAliases.AdminAccess);
			var logoutAccessRight = GetRightByAlias(RightAliases.LogoutAccess);
			var panelAccessRight = GetRightByAlias(RightAliases.PanelAccess);
			var setSiteSettingAccessRight = GetRightByAlias(RightAliases.SetSiteSettingsAccess);

			var adminRights = new[]
			{
				CreateGroupRights(true, adminGroup, adminAccessRight),
				CreateGroupRights(true, adminGroup, logoutAccessRight),
				CreateGroupRights(true, adminGroup, panelAccessRight),
				CreateGroupRights(true, adminGroup, setSiteSettingAccessRight)
			};

			var usersRights = new[]
			{
				CreateGroupRights(false, usersGroup, adminAccessRight),
				CreateGroupRights(true, usersGroup, logoutAccessRight),
				CreateGroupRights(true, usersGroup, panelAccessRight),
				CreateGroupRights(false, usersGroup, setSiteSettingAccessRight)
			};

			Context.GroupsRights.AddRange(usersRights);
			Dispose();

			Context.GroupsRights.AddRange(adminRights);
			Dispose();
		}

		private Right GetRightByAlias(string alias)
		{
			return Context.Rights.First(right => right.Alias == alias);
		}

		private Group GetGroupByAlias(string alias)
		{
			return Context.Groups.First(group => group.Alias == alias);
		}

		private static GroupsRight CreateGroupRights(bool allowed, Group group, Right right)
		{
			return new GroupsRight
			{
				Allowed = allowed,
				Group = group,
				Right = right
			};
		}
	}
}