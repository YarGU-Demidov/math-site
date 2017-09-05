using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.SiteSettings
{
	public class SiteSettingsLogic : LogicBase<SiteSetting>, ISiteSettingsLogic
	{
		public SiteSettingsLogic(MathSiteDbContext context)
			: base(context)
		{
		}

		public async Task CreateSettingAsync(string key, byte[] value)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var setting = new SiteSetting(key, value);
				await context.SiteSettings.AddAsync(setting);
			});
		}

		public async Task UpdateSettingAsync(string key, byte[] value)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var setting = await context.SiteSettings.FirstAsync(settings => settings.Key == key);

				setting.Value = value;

				context.SiteSettings.Update(setting);
			});
		}

		public async Task DeleteSettingAsync(string key)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var setting = await context.SiteSettings.FirstAsync(settings => settings.Key == key);

				context.SiteSettings.Remove(setting);
			});
		}

		public async Task<SiteSetting> TryGetByKeyAsync(string key)
		{
			SiteSetting setting = null;
			await UseContextAsync(async context =>
			{
				setting = await context.SiteSettings.FirstOrDefaultAsync(settings => settings.Key == key);
			});

			return setting;
		}

		public async Task<SiteSetting> FirstOrDefaultAsync()
		{
			SiteSetting setting = null;
			await UseContextAsync(async context =>
			{
				setting = await context.SiteSettings.FirstOrDefaultAsync();
			});

			return setting;
		}
	}
}