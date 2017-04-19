using System.Collections.Generic;
using System.Linq;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	using StaticData;

	/// <inheritdoc />
	public class GroupSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public GroupSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Group";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Groups.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var employeesGroup = CreateGroup(
				"Employees",
				"Teacher or employees",
				GroupAliases.Admin,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.Employee));

			var studentsGroup = CreateGroup(
				"Students",
				"Students",
				GroupAliases.User,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.Student));

			var groups = new[]
			{
				employeesGroup,
				studentsGroup
			};

			Context.Groups.AddRange(groups);
		}

		private static Group CreateGroup(string name, string description, string groupAlias, GroupType groupType)
		{
			return new Group
			{
				Name = name,
				Description = description,
				Alias = groupAlias,
				GroupType = groupType,
				GroupsRights = new List<GroupsRights>(),
				Users = new List<User>(),
				PostGroupsAllowed = new List<PostGroupsAllowed>()
			};
		}
	}
}