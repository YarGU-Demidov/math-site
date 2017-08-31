using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.LogicValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Domain.SiteSettings
{
	public class SiteSettingsLogicTests
	{
		public SiteSettingsLogicTests()
		{
			_databaseFactory = TestSqlLiteDatabaseFactory.UseDefault();
		}

		private readonly ITestDatabaseFactory _databaseFactory;

		[Fact]
		public async Task CreateSetting()
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				var validator = new CurrentUserAccessValidation(context);
				var settingsLogic = new SiteSettingsLogic(context, validator);

				var adminGroup = await context.Groups.Include(group => group.Users).FirstAsync(group => group.Alias == GroupAliases.Admin);
				var user = adminGroup.Users.First();

				var id = await settingsLogic.CreateSettingAsync(user.Id, "testKey", Encoding.UTF8.GetBytes("testValue"));

				Assert.NotEqual(Guid.Empty, id);
			});
		}
	}
}