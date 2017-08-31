using System;
using System.Threading.Tasks;
using MathSite.Domain.Common;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic : ILogicBase<Entities.SiteSettings>
	{
		Task CreateSettingAsync(Guid currentUser, string key, byte[] value);
		Task UpdateSettingAsync(Guid currentUser, string key, byte[] value);
		Task DeleteSettingAsync(Guid currentUser, string key);
	}
}