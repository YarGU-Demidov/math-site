using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications.SiteSettings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.SiteSettings
{
    public class SiteSettingsFacade : BaseFacade, ISiteSettingsFacade
    {
        private const string MemoryCachePrefix = "SiteSetting-";
        private readonly IUserValidationFacade _userValidation;
        private readonly IUsersFacade _usersFacade;

        public SiteSettingsFacade(
            IRepositoryManager repositoryManager, 
            IUserValidationFacade userValidation,
            IMemoryCache memoryCache,
            IUsersFacade usersFacade
        )
            : base(repositoryManager, memoryCache)
        {
            _userValidation = userValidation;
            _usersFacade = usersFacade;
        }

        public Task<string> this[string name] => GetStringSettingAsync(name, true);

        public async Task<string> GetStringSettingAsync(string name, bool cache)
        {
            var settingValue = cache
                ? await MemoryCache.GetOrCreateAsync(
                    GetKey(name),
                    async entry =>
                    {
                        entry.SetSlidingExpiration(GetCacheSpan());
                        entry.Priority = CacheItemPriority.High;
                        return await GetValueForKey(name);
                    }
                )
                : await GetValueForKey(name);

            return settingValue;
        }

        public async Task<bool> SetStringSettingAsync(Guid userId, string name, string value)
        {
            var userExists = await _usersFacade.DoesUserExistsAsync(userId);
            if (!userExists)
                return false;

            var hasRight = await _userValidation.UserHasRightAsync(userId, RightAliases.SetSiteSettingsAccess);
            if (!hasRight)
                return false;

            var requirements = new HasKeySpecification(name);

            var setting =
                await RepositoryManager.SiteSettingsRepository.FirstOrDefaultAsync(requirements.ToExpression());

            var valueBytes = Encoding.UTF8.GetBytes(value);

            MemoryCache.Set(GetKey(name), value, GetCacheSpan());

            if (setting == null)
            {
                await RepositoryManager.SiteSettingsRepository.InsertAsync(new SiteSetting(name, valueBytes));
            }
            else
            {
                setting.Value = valueBytes;
                await RepositoryManager.SiteSettingsRepository.UpdateAsync(setting);
            }

            return true;
        }

        private async Task<string> GetValueForKey(string name)
        {
            var requirements = new HasKeySpecification(name);

            var setting =
                await RepositoryManager.SiteSettingsRepository.FirstOrDefaultAsync(requirements.ToExpression());

            return setting != null
                ? Encoding.UTF8.GetString(setting.Value)
                : null;
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