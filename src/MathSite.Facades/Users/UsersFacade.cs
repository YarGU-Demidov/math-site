using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Users;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Users
{
    public class UsersFacade : BaseFacade<IUsersRepository, User>, IUsersFacade
    {
        public UsersFacade(
            IRepositoryManager repositoryManager,
            IMemoryCache memoryCache
        )
            : base(repositoryManager, memoryCache)
        {
        }

        private TimeSpan CacheTime { get; } = TimeSpan.FromMinutes(5);

        // TODO: FIXME: Extract to classes or smth else
        public async Task<int> GetUsersCountAsync(int perPage, bool cache)
        {
            perPage = perPage > 0 ? perPage : 1;

            var requirements = new AnySpecification<User>();

            var newsCount = await GetCountAsync(requirements, cache, CacheTime);

            return (int) Math.Ceiling(newsCount / (float) perPage);
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<IEnumerable<User>> GetUsersAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await Repository.WithPerson().GetAllWithPagingAsync(skip, perPage);
        }

        public async Task<User> GetCurrentUserAsync(string possibleUserId)
        {
            if (possibleUserId.IsNullOrWhiteSpace())
                return null;

            var userIdGuid = Guid.Parse(possibleUserId);

            return await GetCurrentUserAsync(userIdGuid);
        }

        public async Task<User> GetCurrentUserAsync(Guid possibleUserId)
        {
            if (possibleUserId == default)
                return null;

            return await Repository
                .WithPerson()
                .FirstOrDefaultAsync(possibleUserId);
        }

        public async Task<bool> DoesUserExistsAsync(Guid userId)
        {
            return await Repository.FirstOrDefaultAsync(userId) != null;
        }
        public async Task<bool> DoesUserExistsAsync(string login)
        {
            var requirements = new HasLoginSpecification(login);

            return await RepositoryManager.UsersRepository.FirstOrDefaultAsync(requirements) != null;
        }
    }
}