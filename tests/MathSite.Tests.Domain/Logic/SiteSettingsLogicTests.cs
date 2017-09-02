using System.Text;
using System.Threading.Tasks;
using MathSite.Domain.Logic.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class SiteSettingsLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task CreateSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var testingKey = "testKeyForCreating";
				var testingValue = Encoding.UTF8.GetBytes("testValue");

				await settingsLogic.CreateSettingAsync(testingKey, testingValue);

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

				await settingsLogic.CreateSettingAsync(testingKey, Encoding.UTF8.GetBytes("testValue"));

				var setting = await settingsLogic.TryGetByKeyAsync(testingKey);

				await settingsLogic.DeleteSettingAsync(setting.Key);

				var newSetting = await settingsLogic.TryGetByKeyAsync(testingKey);

				Assert.Null(newSetting);
			});
		}

		[Fact]
		public async Task UpdateSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var setting = await settingsLogic.FirstOrDefaultAsync();

				var newValue = Encoding.UTF8.GetBytes("new value");

				await settingsLogic.UpdateSettingAsync(setting.Key, newValue);

				setting = await settingsLogic.TryGetByKeyAsync(setting.Key);

				Assert.Equal(newValue, setting.Value);
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

				await settingsLogic.CreateSettingAsync(testingKey, testingValue);

				var setting = await context.SiteSettings.FirstOrDefaultAsync(settings => settings.Key == testingKey);

				Assert.NotNull(setting);
				Assert.Equal(testingValue, setting.Value);
			});
		}

		[Fact]
		public async Task FirstOrDefaultAsyncTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var first = await context.SiteSettings.FirstOrDefaultAsync();

				var setting = await settingsLogic.FirstOrDefaultAsync();

				Assert.Equal(first, setting);
			});
		}
	}
}