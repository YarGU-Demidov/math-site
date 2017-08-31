using System;
using System.Linq;
using System.Threading.Tasks;

namespace MathSite.Domain.Logic.SiteSettings
{
	public interface ISiteSettingsLogic
	{
		Task<Guid> CreateSettingAsync(Guid currentUser, string key, byte[] value);
		Task UpdateSettingAsync(Guid currentUser, Guid id, string key, byte[] value);
		Task<Guid> DeleteSettingAsync(Guid currentUser, Guid id, string key, byte[] value);

		Task<TResult> GetFromSettingsAsync<TResult>(Func<IQueryable<MathSite.Entities.SiteSettings>, Task<TResult>> getResult);
	}
}