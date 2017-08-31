﻿using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class GroupRightsSeeder : AbstractSeeder<GroupsRights>
	{
		/// <inheritdoc />
		public GroupRightsSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(GroupsRights);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminGroup = GetGroupByAlias(GroupAliases.Admin);
			var usersGroup = GetGroupByAlias(GroupAliases.User);

			var adminAccessRight = GetRightByAlias(RightAliases.AdminAccess);
			var logoutAccess = GetRightByAlias(RightAliases.LogoutAccess);
			var panelAccess = GetRightByAlias(RightAliases.PanelAccess);

			var adminRights = new[]
			{
				CreateGroupRights(true, adminGroup, adminAccessRight),
				CreateGroupRights(true, adminGroup, logoutAccess),
				CreateGroupRights(true, adminGroup, panelAccess)
			};

			var usersRights = new[]
			{
				CreateGroupRights(false, usersGroup, adminAccessRight),
				CreateGroupRights(true, usersGroup, logoutAccess),
				CreateGroupRights(true, usersGroup, panelAccess)
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

		private static GroupsRights CreateGroupRights(bool allowed, Group group, Right right)
		{
			return new GroupsRights
			{
				Allowed = allowed,
				Group = group,
				Right = right
			};
		}
	}
}