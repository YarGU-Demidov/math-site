using System.Collections.Generic;
using MathSite.Common.Crypto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

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


		public static TestInMemoryDatabaseFactory UseDefault(IPasswordsManager passwordsManager = null, ILoggerFactory loggerFactory = null)
		{
			if (passwordsManager == null)
				passwordsManager = new DoubleSha512HashPasswordsManager();

			if (loggerFactory == null)
				loggerFactory = new LoggerFactory(new List<ILoggerProvider> { new DebugLoggerProvider() });

			return new TestInMemoryDatabaseFactory(passwordsManager, loggerFactory);
		}
	}
}