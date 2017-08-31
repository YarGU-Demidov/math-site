using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Domain.SiteSettings
{
	public class SiteSettingsLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task CreateSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var groupsLogic = new GroupsLogic(context);

				var adminGroup = await groupsLogic.GetFromItemsAsync(
					dbContext => context.Groups.Include(group => group.Users),
					groups => groups.FirstAsync(group => group.Alias == GroupAliases.Admin)
				);

				var user = adminGroup.Users.First();

				var testingKey = "testKeyForCreating";
				var testingValue = Encoding.UTF8.GetBytes("testValue");

				await settingsLogic.CreateSettingAsync(user.Id, testingKey, testingValue);

				var setting = await settingsLogic.GetFromItems(
					async queryable => await queryable.Where(settings => settings.Key == testingKey).FirstOrDefaultAsync()
				);

				Assert.NotNull(setting);

				Assert.NotNull(setting.Key);
				Assert.NotNull(setting.Value);

				Assert.Equal(testingKey, setting.Key);
				Assert.Equal(testingValue, setting.Value);
			});
		}

		[Fact]
		public async Task UpdateSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var groupsLogic = new GroupsLogic(context);
				var adminGroup = await groupsLogic.GetFromItemsAsync(
					dbContext => context.Groups.Include(group => group.Users),
					groups => groups.FirstAsync(group => group.Alias == GroupAliases.Admin)
				);
				var user = adminGroup.Users.First();

				var setting = await settingsLogic.GetFromItems(queryable => queryable.FirstAsync());

				var newValue = Encoding.UTF8.GetBytes("new value");

				await settingsLogic.UpdateSettingAsync(user.Id, setting.Key, newValue);

				setting = await settingsLogic.GetFromItems(queryable => queryable.FirstAsync());

				Assert.Equal(newValue, setting.Value);
			});
		}

		[Fact]
		public async Task DeleteSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var settingsLogic = new SiteSettingsLogic(context);

				var groupsLogic = new GroupsLogic(context);
				var adminGroup = await groupsLogic.GetFromItemsAsync(
					dbContext => context.Groups.Include(group => group.Users),
					groups => groups.FirstAsync(group => group.Alias == GroupAliases.Admin)
				);
				var user = adminGroup.Users.First();

				const string testingKey = "testKeyForDeleting";

				await settingsLogic.CreateSettingAsync(user.Id, testingKey, Encoding.UTF8.GetBytes("testValue"));

				var setting = await settingsLogic.GetFromItems(
					queryable => queryable.Where(settings => settings.Key == testingKey).FirstAsync()
				);

				await settingsLogic.DeleteSettingAsync(user.Id, setting.Key);

				var newSetting = await settingsLogic.GetFromItems(
					async queryable => await queryable.Where(settings => settings.Key == testingKey).FirstOrDefaultAsync());

				Assert.Null(newSetting);
			});
		}
	}
}