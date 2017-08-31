using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.SiteSettings
{
	public class SiteSettingsLogic : LogicBase<Entities.SiteSettings>, ISiteSettingsLogic
	{
		private const string AlreadyExistsFormat = "Key '{0}' already exists in database!";
		private const string DoNotExistsFormat = "Key '{0}' don't exists in database!";
		
		public SiteSettingsLogic(IMathSiteDbContext context)
			: base(context)
		{
		}

		public async Task CreateSettingAsync(Guid currentUser, string key, byte[] value)
		{
			await UseContextAsync(async context =>
			{
				var sameKeyExists = await context.SiteSettings.AnyAsync(settings => settings.Key == key);

				if (sameKeyExists)
					throw new ArgumentException(string.Format(AlreadyExistsFormat, key));

				var setting = new Entities.SiteSettings(key, value);
				await context.SiteSettings.AddAsync(setting);

				await context.SaveChangesAsync();
			});
		}

		public async Task UpdateSettingAsync(Guid currentUser, string key, byte[] value)
		{
			await UseContextAsync(async context =>
			{
				var setting = await context.SiteSettings.FirstOrDefaultAsync(settings => settings.Key == key);

				if (setting == null)
					throw new ArgumentException(string.Format(DoNotExistsFormat, key));

				setting.Value = value;

				context.SiteSettings.Update(setting);

				await context.SaveChangesAsync();
			});
		}

		public async Task DeleteSettingAsync(Guid currentUser, string key)
		{
			await UseContextAsync(async context =>
			{
				var setting = await context.SiteSettings.FirstOrDefaultAsync(settings => settings.Key == key);

				if (setting == null)
					throw new ArgumentException(string.Format(DoNotExistsFormat, key));

				context.SiteSettings.Remove(setting);
				await context.SaveChangesAsync();
			});
		}
	}
}