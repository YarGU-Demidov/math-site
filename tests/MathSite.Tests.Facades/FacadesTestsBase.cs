using System;
using System.Threading.Tasks;
using MathSite.Domain.Common;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.Logic.Users;
using MathSite.Tests.CoreThings;

namespace MathSite.Tests.Facades
{
	public class FacadesTestsBase
	{
		private readonly ITestDatabaseFactory _databaseFactory;

		public FacadesTestsBase()
			: this(TestSqliteDatabaseFactory.UseDefault())
		{
		}

		public FacadesTestsBase(ITestDatabaseFactory databaseFactory)
		{
			_databaseFactory = databaseFactory;
		}

		protected void WithLogic(Action<IBusinessLogicManger> actions)
		{
			_databaseFactory.ExecuteWithContext(context =>
			{
				var logic = new BusinessLogicManager(
					context, 
					new GroupsLogic(context), 
					new PersonsLogic(context), 
					new UsersLogic(context), 
					new FilesLogic(context), 
					new SiteSettingsLogic(context)
				);
				using (logic)
				{
					actions(logic);
				}
			});
		}

		protected async Task WithLogicAsync(Func<IBusinessLogicManger, Task> actions)
		{
			await _databaseFactory.ExecuteWithContextAsync(async context =>
			{
				var logic = new BusinessLogicManager(
					context, 
					new GroupsLogic(context), 
					new PersonsLogic(context), 
					new UsersLogic(context), 
					new FilesLogic(context), 
					new SiteSettingsLogic(context)
				);
				using (logic)
				{
					await actions(logic);
				}
			});
		}
	}
}