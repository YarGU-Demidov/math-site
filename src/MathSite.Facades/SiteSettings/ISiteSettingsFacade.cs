using System;
using System.Threading.Tasks;

namespace MathSite.Facades.SiteSettings
{
	public interface ISiteSettingsFacade
	{
		Task<string> this[string name] { get; }
		Task<string> GetStringSettingAsync(string name);
		Task<bool> SetStringSettingAsync(Guid userId, string name, string value);
	}
}