using System;
using System.Threading.Tasks;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic
	{
		Task CreateSettingAsync(string key, byte[] value);
		Task UpdateSettingAsync(string key, byte[] value);
		Task DeleteSettingAsync(string key);

		Task<Entities.SiteSettings> TryGetByKeyAsync(string key);
		Task<Entities.SiteSettings> FirstOrDefaultAsync();
	}
}