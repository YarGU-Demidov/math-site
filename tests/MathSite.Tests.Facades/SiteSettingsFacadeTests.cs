using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using Xunit;

namespace MathSite.Tests.Facades
{
	public class SiteSettingsFacadeTests : FacadesTestsBase
	{
		[Fact]
		public async Task SetSettingStringTest()
		{
			await WithLogicAsync(async manger =>
			{
				var userValidationFacade = new UserValidationFacade(manger);
				var siteSettingsFacade = new SiteSettingsFacade(manger, userValidationFacade);
				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

				var testSalt = Guid.NewGuid();
				var testKey = $"testKey-{testSalt}";
				var testValue = $"testValue-{testSalt}";

				var done = await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

				Assert.True(done);

				var setting = await manger.SiteSettingsLogic.TryGetByKeyAsync(testKey);

				Assert.NotNull(setting);
				
				Assert.Equal(setting.Value, Encoding.UTF8.GetBytes(testValue));
			});
		}

		[Fact]
		public async Task GetSettingStringTest()
		{
			await WithLogicAsync(async manger =>
			{
				var userValidationFacade = new UserValidationFacade(manger);
				var siteSettingsFacade = new SiteSettingsFacade(manger, userValidationFacade);

				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

				var testKey = "testKey";
				var testValue = "testValue";

				await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

				var value = await siteSettingsFacade.GetStringSettingAsync(testKey);

				Assert.Equal(testValue, value);
			});
		}

		[Fact]
		public async Task IndexerTask()
		{
			await WithLogicAsync(async manger =>
			{
				var userValidationFacade = new UserValidationFacade(manger);
				var siteSettingsFacade = new SiteSettingsFacade(manger, userValidationFacade);

				var user = await manger.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);
				
				var testSalt = Guid.NewGuid();
				var testKey = $"testKey-{testSalt}";
				var testValue = $"testValue-{testSalt}";

				await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

				var value = await siteSettingsFacade[testKey];

				Assert.Equal(testValue, value);
			});
		}
	}
}