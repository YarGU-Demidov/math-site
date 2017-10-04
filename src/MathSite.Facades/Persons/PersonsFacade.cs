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

        public async Task<int> GetPersonsCountAsync(int perPage, bool cache)
        {
            perPage = perPage > 0 ? perPage : 1;

            var requirements = new AnySpecification<Person>();

            var newsCount = await GetCountAsync(requirements, RepositoryManager.PersonsRepository, cache, CacheTime);

            return (int)Math.Ceiling(newsCount / (float)perPage);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await RepositoryManager.PersonsRepository.GetAllWithPagingAsync(skip, perPage);
        }
    }
}