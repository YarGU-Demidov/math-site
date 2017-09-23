using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications.SiteSettings;
using MathSite.Specifications.Users;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class SiteSettingsFacadeTests : FacadesTestsBase
    {
        public IUserValidationFacade GetFacade(IRepositoryManager manager)
        {
            return new UserValidationFacade(manager, MemoryCache, new DoubleSha512HashPasswordsManager());
        }

        [Fact]
        public async Task GetSettingStringTest()
        {
            await WithLogicAsync(async manager =>
            {
                var userValidationFacade = GetFacade(manager);
                var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);

                var user = await GetUserByLogin(manager, UsersAliases.FirstUser);

                var testSalt = Guid.NewGuid();
                var testKey = $"testKey-{testSalt}";
                var testValue = $"testValue-{testSalt}";

                await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

                var value = await siteSettingsFacade.GetStringSettingAsync(testKey);

                Assert.Equal(testValue, value);
            });
        }

        [Fact]
        public async Task GetSettingStringTest_KeyDoesNotExists()
        {
            await WithLogicAsync(async manager =>
            {
                var userValidationFacade = GetFacade(manager);
                var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);

                var testKey = $"testKey-{Guid.NewGuid()}";

                var value = await siteSettingsFacade.GetStringSettingAsync(testKey);

                Assert.Null(value);
            });
        }

        [Fact]
        public async Task IndexerTask()
        {
            await WithLogicAsync(async manager =>
            {
                var userValidationFacade = GetFacade(manager);
                var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);

                var user = await GetUserByLogin(manager, UsersAliases.FirstUser);

                var testSalt = Guid.NewGuid();
                var testKey = $"testKey-{testSalt}";
                var testValue = $"testValue-{testSalt}";

                await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

                var value = await siteSettingsFacade[testKey];

                Assert.Equal(testValue, value);
            });
        }

        [Fact]
        public async Task SetSettingStringTest()
        {
            await WithLogicAsync(async manager =>
            {
                var userValidationFacade = GetFacade(manager);
                var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);
                var user = await GetUserByLogin(manager, UsersAliases.FirstUser);

                var testSalt = Guid.NewGuid();
                var testKey = $"testKey-{testSalt}";
                var testValue = $"testValue-{testSalt}";

                var done = await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

                Assert.True(done);

                var requirements = new HasKeySpecification(testKey);

                var setting = await manager.SiteSettingsRepository.FirstOrDefaultAsync(requirements.ToExpression());

                Assert.NotNull(setting);

                Assert.Equal(setting.Value, Encoding.UTF8.GetBytes(testValue));
            });
        }

        private async Task<User> GetUserByLogin(IRepositoryManager manager, string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await manager.UsersRepository.FirstOrDefaultWithRightsAsync(requirements.ToExpression());
        }
    }
}