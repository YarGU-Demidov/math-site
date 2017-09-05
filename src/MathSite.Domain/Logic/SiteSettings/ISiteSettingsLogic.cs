using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic
	{
		Task CreateSettingAsync(string key, byte[] value);
		Task UpdateSettingAsync(string key, byte[] value);
		Task DeleteSettingAsync(string key);

		Task<SiteSetting> TryGetByKeyAsync(string key);
		Task<SiteSetting> FirstOrDefaultAsync();
	}
}