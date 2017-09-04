using System;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.UserValidation;
using Xunit;

namespace MathSite.Tests.Facades
{
	public class SiteSettingsFacadeTests : FacadesTestsBase
	{
		public IUserValidationFacade GetFacade(IBusinessLogicManager manager)
		{
			return new UserValidationFacade(manager, MemoryCache);
		}

		[Fact]
		public async Task SetSettingStringTest()
		{
			await WithLogicAsync(async manager =>
			{
				var userValidationFacade = GetFacade(manager);
				var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);
				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

				var testSalt = Guid.NewGuid();
				var testKey = $"testKey-{testSalt}";
				var testValue = $"testValue-{testSalt}";

				var done = await siteSettingsFacade.SetStringSettingAsync(user.Id, testKey, testValue);

				Assert.True(done);

				var setting = await manager.SiteSettingsLogic.TryGetByKeyAsync(testKey);

				Assert.NotNull(setting);
				
				Assert.Equal(setting.Value, Encoding.UTF8.GetBytes(testValue));
			});
		}

		[Fact]
		public async Task GetSettingStringTest()
		{
			await WithLogicAsync(async manager =>
			{
				var userValidationFacade = GetFacade(manager);
				var siteSettingsFacade = new SiteSettingsFacade(manager, userValidationFacade, MemoryCache);

				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);

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

				var user = await manager.UsersLogic.TryGetByLoginAsync(UsersAliases.FirstUser);
				
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