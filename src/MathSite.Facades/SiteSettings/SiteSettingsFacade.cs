using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Facades.UserValidation;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.SiteSettings
{
	public class SiteSettingsFacade : BaseFacade, ISiteSettingsFacade
	{
		private readonly IUserValidationFacade _userValidation;

		private const string MemoryCachePrefix = "SiteSetting-";

		public SiteSettingsFacade(IBusinessLogicManager logicManager, IUserValidationFacade userValidation, IMemoryCache memoryCache) 
			: base(logicManager, memoryCache)
		{
			_userValidation = userValidation;
		}

		public Task<string> this[string name] => GetStringSettingAsync(name);

		public async Task<string> GetStringSettingAsync(string name)
		{
			var settingValue = await MemoryCache.GetOrCreateAsync(GetKey(name), async entry =>
			{
				entry.SetSlidingExpiration(GetCacheSpan());
				entry.Priority = CacheItemPriority.High;

				var setting = await LogicManager.SiteSettingsLogic.TryGetByKeyAsync(name);

				return setting != null
					? Encoding.UTF8.GetString(setting.Value)
					: null;
			});

			return settingValue;
		}

		public async Task<bool> SetStringSettingAsync(Guid userId, string name, string value)
		{
			var userExists = await _userValidation.DoesUserExistsAsync(userId);
			if (!userExists)
				return false;

			var hasRight = await _userValidation.UserHasRightAsync(userId, RightAliases.SetSiteSettingsAccess);
			if (!hasRight)
				return false;

			var setting = await LogicManager.SiteSettingsLogic.TryGetByKeyAsync(name);

			var valueBytes = Encoding.UTF8.GetBytes(value);

			MemoryCache.Set(GetKey(name), value, GetCacheSpan());

			if (setting == null)
			{
				await LogicManager.SiteSettingsLogic.CreateSettingAsync(name, valueBytes);
			}
			else
			{
				await LogicManager.SiteSettingsLogic.UpdateSettingAsync(name, valueBytes);
			}
			return true;
		}

		private string GetKey(string name)
		{
			return MemoryCachePrefix + name;
		}

		private TimeSpan GetCacheSpan()
		{
			return TimeSpan.FromMinutes(5);
		}
	}
}