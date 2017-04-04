using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class RightsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public RightsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Rights";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Rights.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminAccessRight = CreateRight(
				"Admin Access", 
				"Allowing access to admin panel.", 
				RightsAliases.AdminAccess
			);
			var logoutRight = CreateRight(
				"Logout Access", 
				"Allowing to logout.", 
				RightsAliases.LogoutAccess
			);
			var panelAccessRight = CreateRight(
				"Panel Access", 
				"Allowing to access user panel.", 
				RightsAliases.PanelAccess
			);

			var rights = new[]
			{
				adminAccessRight,
				logoutRight,
				panelAccessRight
			};

			Context.Rights.AddRange(rights);
		}

		private Right CreateRight(string name, string description, string alias)
		{
			return new Right
			{
				Name = name,
				Description = description,
				Alias = alias,
				GroupsRights = new List<GroupsRights>()
			};
		}
	}
}