using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic
	{
		Task CreateAsync(string key, byte[] value);
		Task UpdateAsync(string key, byte[] value);
		Task DeleteAsync(string key);

		Task<SiteSetting> TryGetByKeyAsync(string key);
	}
}