using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Facades;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class PersonBaseFacadeTestClass : BaseFacade<IPersonsRepository, Person>
    {
        public PersonBaseFacadeTestClass(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<int> GetCountWithoutCacheAsync()
        {
            return await GetCountAsync(new AnySpecification<Person>(), false);
        }
    }

    public class BaseFacadeTests : FacadesTestsBase
    {
        [Fact]
        public async Task TestCount()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var testClass = new PersonBaseFacadeTestClass(manager, MemoryCache);

                var personsCount = await testClass.GetCountWithoutCacheAsync();

                Assert.Equal(3, personsCount);
            });
        }
    }
}