﻿using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Facades;
using MathSite.Repository;
using MathSite.Repository.Core;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class PersonBaseFacadeTestClass : BaseMathFacade<IPersonsRepository, Person>
    {
        public PersonBaseFacadeTestClass(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
        }

        public async Task<int> GetCountWithoutCacheAsync()
        {
            return await GetCountAsync(new AnySpecification<Person>());
        }
    }

    public class BaseFacadeTests : FacadesTestsBase
    {
        [Fact]
        public async Task TestCount()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var testClass = new PersonBaseFacadeTestClass(manager);

                var personsCount = await testClass.GetCountWithoutCacheAsync();

                Assert.Equal(3, personsCount);
            });
        }
    }
}