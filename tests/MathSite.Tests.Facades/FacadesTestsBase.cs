using System;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Posts;
using MathSite.Domain.Logic.PostSeoSettings;
using MathSite.Domain.Logic.PostSettings;
using MathSite.Domain.Logic.Rights;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.Logic.Users;
using MathSite.Tests.CoreThings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Tests.Facades
{
	public class FacadesTestsBase
	{
		private readonly ITestDatabaseFactory _databaseFactory;

		public FacadesTestsBase()
			: this(TestSqliteDatabaseFactory.UseDefault())
		{
			MemoryCache = new MemoryCache(new MemoryCacheOptions());
		}

		public IMemoryCache MemoryCache { get; }

		public FacadesTestsBase(ITestDatabaseFactory databaseFactory)
		{
			_databaseFactory = databaseFactory;
		}

		protected void WithLogic(Action<IBusinessLogicManager> actions)
		{
			_databaseFactory.ExecuteWithContext(context =>
			{
				actions(CreateBusinessLogicManger(context));
			});
		}

		protected async Task WithLogicAsync(Func<IBusinessLogicManager, Task> actions)
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				await actions(CreateBusinessLogicManger(context));
			});
		}

		private IBusinessLogicManager CreateBusinessLogicManger(MathSiteDbContext context)
		{
			return new BusinessLogicManager(
				new GroupsLogic(context),
				new PersonsLogic(context),
				new UsersLogic(context),
				new FilesLogic(context),
				new SiteSettingsLogic(context),
				new RightsLogic(context),
				new PostsLogic(context),
				new PostSeoSettingsLogic(context),
				new PostSettingLogic(context)
			);
		}
	}
}