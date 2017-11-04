using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Users
{
    public class UsersFacade : BaseFacade, IUsersFacade
    {
        private TimeSpan CacheTime { get; } = TimeSpan.FromMinutes(5);

        public UsersFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<int> GetUsersCountAsync(int perPage, bool cache)
        {
            perPage = perPage > 0 ? perPage : 1;

            var requirements = new AnySpecification<User>();

            var newsCount = await GetCountAsync(requirements, RepositoryManager.UsersRepository, cache, CacheTime);

            return (int)Math.Ceiling(newsCount / (float)perPage);
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<IEnumerable<User>> GetUsersAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await RepositoryManager.UsersRepository.GetAllWithPagingAndPersonAndGroupAsync(skip, perPage);
        }
    }
}