using System;
using System.Threading.Tasks;
using MathSite.Domain.Common;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic : ILogicBase<Entities.SiteSettings>
	{
		Task<Guid> CreateSettingAsync(Guid currentUser, string key, byte[] value);
		Task UpdateSettingAsync(Guid currentUser, Guid id, string key, byte[] value);
		Task<Guid> DeleteSettingAsync(Guid currentUser, Guid id, string key, byte[] value);
	}
}