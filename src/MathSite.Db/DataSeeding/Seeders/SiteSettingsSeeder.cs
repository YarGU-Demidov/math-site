using System.Text;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class SiteSettingsSeeder : AbstractSeeder<SiteSettings>
	{
		/// <inheritdoc />
		public SiteSettingsSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		public override string SeedingObjectName { get; } = nameof(SiteSettings);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var siteNameSetting = CreateCategory(
				SiteSettingsNames.SiteName,
				Encoding.UTF8.GetBytes("Математический Факультет ЯрГУ")
			);

			var defaultDelimiter = CreateCategory(
				SiteSettingsNames.TitleDelimiter,
				Encoding.UTF8.GetBytes(" | ")
			);

			var categories = new[]
			{
				siteNameSetting,
				defaultDelimiter
			};

			Context.SiteSettings.AddRange(categories);
		}

		private static SiteSettings CreateCategory(string key, byte[] value)
		{
			return new SiteSettings
			{
				Key = key,
				Value = value
			};
		}
	}
}