using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class UserSettingsSeeder : AbstractSeeder<UserSettings>
	{
		/// <inheritdoc />
		public UserSettingsSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(UserSettings);


		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstUserSettings = CreateUserSettings(
				"FirstUserSettings",
				"FirstUserSettings",
				"It's settings for FirstUser",
				GetUserByLogin(UsersAliases.FirstUser)
			);

			var secondUserSettings = CreateUserSettings(
				"SecondUserSettings",
				"SecondUserSettings",
				"It's settings for SecondUser",
				GetUserByLogin(UsersAliases.SecondUser)
			);

			var userSettings = new[]
			{
				firstUserSettings,
				secondUserSettings
			};

			Context.UserSettingses.AddRange(userSettings);
		}

		private User GetUserByLogin(string login)
		{
			return Context.Users.First(u => u.Login == login);
		}

		private static UserSettings CreateUserSettings(string name, string key, string value, User user)
		{
			return new UserSettings
			{
				Namespace = name,
				Key = key,
				Value = value,
				User = user
			};
		}
	}
}