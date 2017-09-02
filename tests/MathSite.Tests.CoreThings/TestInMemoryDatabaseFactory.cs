using MathSite.Common.Crypto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MathSite.Tests.CoreThings
{
	public class TestInMemoryDatabaseFactory : TestDatabaseFactory
	{
		public TestInMemoryDatabaseFactory(IPasswordsManager passwordsManager, ILoggerFactory loggerFactory)
			: base(passwordsManager, loggerFactory)
		{
		}

		protected override DbContextOptions GetContextOptions()
		{
			return new DbContextOptionsBuilder()
				.UseInMemoryDatabase("MathSite")
				.Options;
		}
	}
}