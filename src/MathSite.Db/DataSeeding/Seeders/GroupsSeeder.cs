using System.Collections.Generic;
using System.Linq;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class GroupsSeeder: AbstractSeeder
	{
		/// <inheritdoc />
		public GroupsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Groups";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Groups.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var administratorsGroup = CreateGroup("Administrators", "System Administrators", GroupsAliases.Admin);
			var usersGroup = CreateGroup("Site user", "Simple site user", GroupsAliases.User);

			var groups = new[]
			{
				administratorsGroup,
				usersGroup
			};
			Context.Groups.AddRange(groups);
		}

		private Group CreateGroup(string name, string description, string alias)
		{
			return new Group
			{
				Name = name,
				Description = description,
				Alias = alias,
				GroupsRights = new List<GroupsRights>()
			};
		}
	}
}