using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.LogicValidation;
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
				var validator = new CurrentUserAccessValidation(context);
				var settingsLogic = new SiteSettingsLogic(context, validator);

				var groupsLogic = new GroupsLogic(context, validator);

				var adminGroup = await groupsLogic.GetFromItemsAsync(
					dbContext => context.Groups.Include(group => group.Users),
					groups => groups.FirstAsync(group => group.Alias == GroupAliases.Admin)
				);

				var user = adminGroup.Users.First();

				var id = await settingsLogic.CreateSettingAsync(user.Id, "testKey", Encoding.UTF8.GetBytes("testValue"));

				Assert.NotEqual(Guid.Empty, id);
			});
		}

		[Fact]
		public async Task UpdateSettingTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var validator = new CurrentUserAccessValidation(context);
				var settingsLogic = new SiteSettingsLogic(context, validator);

				var groupsLogic = new GroupsLogic(context, validator);
				var adminGroup = await groupsLogic.GetFromItemsAsync(
					dbContext => context.Groups.Include(group => group.Users),
					groups => groups.FirstAsync(group => group.Alias == GroupAliases.Admin)
				);
				var user = adminGroup.Users.First();

				var setting = await settingsLogic.GetFromItems(queryable => queryable.FirstAsync());

				var newValue = Encoding.UTF8.GetBytes("new value");

				await settingsLogic.UpdateSettingAsync(user.Id, setting.Id, setting.Key, newValue);

				setting = await settingsLogic.GetFromItems(queryable => queryable.FirstAsync());

				Assert.Equal(newValue, setting.Value);
			});
		}
	}
}