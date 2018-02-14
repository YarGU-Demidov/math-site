using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Db.DbExtensions;
using MathSite.Entities;
using MathSite.Facades.Persons;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications.SiteSettings;
using MathSite.Specifications.Users;
using MathSite.Tests.CoreThings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class SiteSettingsFacadeTests : FacadesTestsBase<SiteSettingsFacade>
    {

        [Fact]
        public async Task GetSettingStringTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                
                var siteSettingsFacade = GetFacade(context, manager);

                var user = await GetUserByLogin(manager, UsersAliases.Mokeev1995);

                var testTitle = $"testTitle-{Guid.NewGuid()}";

                await siteSettingsFacade.SetDefaultHomePageTitle(user.Id, testTitle);

                var value = await siteSettingsFacade.GetDefaultHomePageTitle(false);

                Assert.Equal(testTitle, value);
            });
        }

        [Fact]
        public async Task GetSettingStringTest_KeyDoesNotExists()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var siteSettingsFacade = GetFacade(context, manager);
                await context.Clear<SiteSetting>();
                
                var value = await siteSettingsFacade.GetDefaultNewsPageTitle(false);

                Assert.Null(value);
            });
        }

        [Fact]
        public async Task SetSettingStringTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var siteSettingsFacade = GetFacade(context, manager);
                var user = await GetUserByLogin(manager, UsersAliases.Mokeev1995);
                
                var testValue = $"testValue-{Guid.NewGuid()}";

                var done = await siteSettingsFacade.SetSiteName(user.Id, testValue);

                Assert.True(done);

                var requirements = new HasKeySpecification(SiteSettingsNames.SiteName);

                var setting = await manager.SiteSettingsRepository.FirstOrDefaultAsync(requirements.ToExpression());

                Assert.NotNull(setting);

                Assert.Equal(setting.Value, Encoding.UTF8.GetBytes(testValue));
            });
        }

        private async Task<User> GetUserByLogin(IRepositoryManager manager, string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await manager.UsersRepository.WithRights().FirstOrDefaultAsync(requirements.ToExpression());
        }

        protected override SiteSettingsFacade GetFacade(MathSiteDbContext context, IRepositoryManager manager)
        {
            var passwordsManager = new DoubleSha512HashPasswordsManager();
            var userValidationFacade = new UserValidationFacade(manager, MemoryCache, passwordsManager);
            var usersFacade = new UsersFacade(manager, MemoryCache, userValidationFacade, passwordsManager);
            return new SiteSettingsFacade(manager, userValidationFacade, MemoryCache, usersFacade);
        }
    }
}