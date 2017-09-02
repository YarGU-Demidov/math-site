using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class GroupSeeder : AbstractSeeder<Group>
	{
		/// <inheritdoc />
		public GroupSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(Group);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var employeesGroup = CreateGroup(
				"Dean's Office",
				"Dean's Office",
				GroupAliases.DeansOffice,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.Employee)
			);

			var usersGroup = CreateGroup(
				"Users",
				"Site users",
				GroupAliases.User,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.User)
			);

			var administratorsGroup = CreateGroup(
				"Administrators",
				"Site administrators",
				GroupAliases.Admin,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.User),
				true
			);

			var studentsGroup = CreateGroup(
				"Students",
				"Students",
				GroupAliases.Students,
				Context.GroupTypes.First(groupType => groupType.Alias == GroupTypeAliases.Student)
			);

			var groups = new[]
			{
				employeesGroup,
				studentsGroup,
				usersGroup,
				administratorsGroup
			};

			Context.Groups.AddRange(groups);
		}

		private static Group CreateGroup(string name, string description, string groupAlias, GroupType groupType, bool isAdmin = false)
		{
			return new Group
			{
				Name = name,
				Description = description,
				Alias = groupAlias,
				GroupType = groupType,
				GroupsRights = new List<GroupsRights>(),
				Users = new List<User>(),
				PostGroupsAllowed = new List<PostGroupsAllowed>(),
				IsAdmin = isAdmin
			};
		}
	}
}