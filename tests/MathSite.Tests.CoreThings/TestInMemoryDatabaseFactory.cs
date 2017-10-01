using Microsoft.EntityFrameworkCore;

namespace MathSite.Tests.CoreThings
{
    public class TestInMemoryDatabaseFactory : TestDatabaseFactory
    {
        protected override DbContextOptions GetContextOptions()
        {
            return new DbContextOptionsBuilder()
                .UseInMemoryDatabase("MathSite")
                .Options;
        }


        public static TestInMemoryDatabaseFactory UseDefault()
        {
            return new TestInMemoryDatabaseFactory();
        }
    }
}