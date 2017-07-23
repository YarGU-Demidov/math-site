using System.Collections.Generic;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	/// <inheritdoc />
	public class RightSeeder : AbstractSeeder<Right>
	{
		/// <inheritdoc />
		public RightSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Right";
		
		/// <inheritdoc />
		protected override void SeedData()
		{
			var adminAccessRight = CreateRight(
				"Admin Access",
				"Allowing access to admin panel.",
				RightAliases.AdminAccess
			);
			var logoutRight = CreateRight(
				"Logout Access",
				"Allowing to logout.",
				RightAliases.LogoutAccess
			);
			var panelAccessRight = CreateRight(
				"Panel Access",
				"Allowing to access user panel.",
				RightAliases.PanelAccess
			);

			var rights = new[]
			{
				adminAccessRight,
				logoutRight,
				panelAccessRight
			};

			Context.Rights.AddRange(rights);
		}

		private static Right CreateRight(string name, string description, string alias)
		{
			return new Right
			{
				Name = name,
				Description = description,
				Alias = alias,
				UsersRights = new List<UsersRights>(),
				GroupsRights = new List<GroupsRights>()
			};
		}
	}
}