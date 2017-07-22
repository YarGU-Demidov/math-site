using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class GroupTypeSeeder : AbstractSeeder<GroupType>
	{
		/// <inheritdoc />
		public GroupTypeSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "GroupType";
		
		/// <inheritdoc />
		protected override void SeedData()
		{
			var userGroupType = CreateGroupType("Users", "Site user", GroupTypeAliases.User);
			var studentGroupType = CreateGroupType("Students", "Site student", GroupTypeAliases.Student);
			var employeeGroupType = CreateGroupType("Employees", "Site teacher or employee", GroupTypeAliases.Employee);

			var groupsTypes = new[]
			{
				userGroupType,
				studentGroupType,
				employeeGroupType
			};

			Context.GroupTypes.AddRange(groupsTypes);
		}

		private static GroupType CreateGroupType(string name, string description, string alias)
		{
			return new GroupType
			{
				Name = name,
				Description = description,
				Alias = alias,
				Groups = new List<Group>()
			};
		}
	}
}