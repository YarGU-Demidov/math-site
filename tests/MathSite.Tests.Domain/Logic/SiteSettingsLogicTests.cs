using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Domain.Logic.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
    public class SiteSettingsLogicTests : DomainTestsBase
    {
        private async Task CreateSiteSetting(ISiteSettingsLogic logic, string key, byte[] value = null)
        {
            var salt = Guid.NewGuid();

            value = value ?? Encoding.UTF8.GetBytes($"test-value-{salt}");
            await logic.CreateAsync(key, value);
        }

        [Fact]
        public async Task CreateSettingTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var settingsLogic = new SiteSettingsLogic(context);

                var testingKey = "testKeyForCreating";
                var testingValue = Encoding.UTF8.GetBytes("testValue");

                await CreateSiteSetting(settingsLogic, testingKey, testingValue);

                var setting = await settingsLogic.TryGetByKeyAsync(testingKey);

                Assert.NotNull(setting);

                Assert.NotNull(setting.Key);
                Assert.NotNull(setting.Value);

                Assert.Equal(testingKey, setting.Key);
                Assert.Equal(testingValue, setting.Value);
            });
        }

        [Fact]
        public async Task DeleteSettingTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var settingsLogic = new SiteSettingsLogic(context);

                const string testingKey = "testKeyForDeleting";

                await CreateSiteSetting(settingsLogic, testingKey);

                var setting = await settingsLogic.TryGetByKeyAsync(testingKey);

                await settingsLogic.DeleteAsync(setting.Key);

                var newSetting = await settingsLogic.TryGetByKeyAsync(testingKey);

                Assert.Null(newSetting);
            });
        }

        [Fact]
        public async Task TryGetByKeyTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var settingsLogic = new SiteSettingsLogic(context);

                var testingKey = "testKeyForCreating";
                var testingValue = Encoding.UTF8.GetBytes("testValue");

                await settingsLogic.CreateAsync(testingKey, testingValue);

                var setting = await context.SiteSettings.FirstOrDefaultAsync(settings => settings.Key == testingKey);

                Assert.NotNull(setting);
                Assert.Equal(testingValue, setting.Value);
            });
        }

        [Fact]
        public async Task UpdateSettingTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var settingsLogic = new SiteSettingsLogic(context);

                const string key = "test-key-for-update";

                await CreateSiteSetting(settingsLogic, key);

                var newValue = Encoding.UTF8.GetBytes("new value");

                await settingsLogic.UpdateAsync(key, newValue);

                var setting = await settingsLogic.TryGetByKeyAsync(key);

                Assert.Equal(newValue, setting.Value);
            });
        }
    }
}