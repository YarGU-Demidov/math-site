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

namespace MathSite.Facades.SiteSettings
{
    public class SiteSettingsFacade : BaseMathFacade<ISiteSettingsRepository, SiteSetting>, ISiteSettingsFacade
    {
        private readonly IUserValidationFacade _userValidation;
        private readonly IUsersFacade _usersFacade;

        public SiteSettingsFacade(
            IRepositoryManager repositoryManager, 
            IUserValidationFacade userValidation,
            IUsersFacade usersFacade
        )
            : base(repositoryManager)
        {
            _userValidation = userValidation;
            _usersFacade = usersFacade;
        }

        public async Task<int> GetPerPageCountAsync(int defaultCount = 18)
        {
            return int.Parse(await GetStringSettingAsync(SiteSettingsNames.PerPage) ?? defaultCount.ToString());
        }

        public Task<string> GetTitleDelimiter()
        {
            return GetStringSettingAsync(SiteSettingsNames.TitleDelimiter);
        }

        public Task<string> GetDefaultHomePageTitle()
        {
            return GetStringSettingAsync(SiteSettingsNames.DefaultHomePageTitle);
        }

        public Task<string> GetDefaultNewsPageTitle()
        {
            return GetStringSettingAsync(SiteSettingsNames.DefaultNewsPageTitle);
        }

        public Task<string> GetSiteName()
        {
            return GetStringSettingAsync(SiteSettingsNames.SiteName);
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

        private async Task<string> GetStringSettingAsync(string name)
        {
            var settingValue = await GetValueForKey(name);

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
    }
}