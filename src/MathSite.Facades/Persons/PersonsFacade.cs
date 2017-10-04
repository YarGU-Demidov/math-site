using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Persons
{
    public class PersonsFacade : BaseFacade, IPersonsFacade
    {
        private TimeSpan CacheTime { get; } = TimeSpan.FromMinutes(5);

        public PersonsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<int> GetPersonsCountAsync(int page, int perPage, bool cache)
        {
            var requirements = new AnySpecification<Person>();

            return await GetCountAsync(requirements, RepositoryManager.PersonsRepository, cache, CacheTime);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync(int page, int perPage, bool cache)
        {
            var skip = (page - 1) * perPage;

            return await RepositoryManager.PersonsRepository.GetAllWithPagingAsync(skip, perPage);
        }
    }
}