using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.SiteSettings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.SiteSettings
{
    public class SiteSettingsFacade : BaseMathFacade<ISiteSettingsRepository, SiteSetting>, ISiteSettingsFacade
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

        public async Task<int> GetPerPageCountAsync(bool cache, int defaultCount = 18)
        {
            return int.Parse(await GetStringSettingAsync(SiteSettingsNames.PerPage, cache) ?? defaultCount.ToString());
        }

        public Task<string> GetTitleDelimiter(bool cache = true)
        {
            return GetStringSettingAsync(SiteSettingsNames.TitleDelimiter, cache);
        }

        public Task<string> GetDefaultHomePageTitle(bool cache = true)
        {
            return GetStringSettingAsync(SiteSettingsNames.DefaultHomePageTitle, cache);
        }

        public Task<string> GetDefaultNewsPageTitle(bool cache = true)
        {
            return GetStringSettingAsync(SiteSettingsNames.DefaultNewsPageTitle, cache);
        }

        public Task<string> GetSiteName(bool cache = true)
        {
            return GetStringSettingAsync(SiteSettingsNames.SiteName, cache);
        }

        public Task<bool> SetPerPageCountAsync(Guid userId, string perPageCount)
        {
            return SetStringSettingAsync(userId, SiteSettingsNames.PerPage, perPageCount);
        }

        public Task<bool> SetTitleDelimiter(Guid userId, string titleDelimiter)
        {
            return SetStringSettingAsync(userId, SiteSettingsNames.TitleDelimiter, titleDelimiter);
        }

        public Task<bool> SetDefaultHomePageTitle(Guid userId, string defaultHomePageTitle)
        {
            return SetStringSettingAsync(userId, SiteSettingsNames.DefaultHomePageTitle, defaultHomePageTitle);
        }

        public Task<bool> SetDefaultNewsPageTitle(Guid userId, string defaultNewsPageTitle)
        {
            return SetStringSettingAsync(userId, SiteSettingsNames.DefaultNewsPageTitle, defaultNewsPageTitle);
        }

        public Task<bool> SetSiteName(Guid userId, string siteName)
        {
            return SetStringSettingAsync(userId, SiteSettingsNames.SiteName, siteName);
        }

        private async Task<string> GetStringSettingAsync(string name, bool cache)
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

        private async Task<bool> SetStringSettingAsync(Guid userId, string name, string value)
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

            var setting = await Repository.FirstOrDefaultAsync(requirements.ToExpression());

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