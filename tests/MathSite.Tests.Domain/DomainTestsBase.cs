namespace MathSite.Tests.Domain
{
	public abstract class DomainTestsBase
	{
		protected readonly ITestDatabaseFactory DatabaseFactory;

		public DomainTestsBase()
		{
			DatabaseFactory = TestSqlLiteDatabaseFactory.UseDefault();
		}
	}
}