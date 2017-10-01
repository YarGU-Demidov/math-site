using System.Text;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class SiteSettingsSeeder : AbstractSeeder<SiteSetting>
    {
        /// <inheritdoc />
        public SiteSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        public override string SeedingObjectName { get; } = nameof(SiteSetting);

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

            var defaultHomePageTitle = CreateCategory(
                SiteSettingsNames.DefaultHomePageTitle,
                Encoding.UTF8.GetBytes("Главная страница")
            );

            var defaultNewsPageTitle = CreateCategory(
                SiteSettingsNames.DefaultNewsPageTitle,
                Encoding.UTF8.GetBytes("Новости нашего факультета")
            );

            var categories = new[]
            {
                siteNameSetting,
                defaultDelimiter,
                defaultHomePageTitle,
                defaultNewsPageTitle
            };

            Context.SiteSettings.AddRange(categories);
        }

        private static SiteSetting CreateCategory(string key, byte[] value)
        {
            return new SiteSetting
            {
                Key = key,
                Value = value
            };
        }
    }
}