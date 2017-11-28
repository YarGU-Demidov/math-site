using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Entities;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Users
{
    public class UsersFacade : BaseFacade<IUsersRepository, User>, IUsersFacade
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private TimeSpan CacheTime { get; } = TimeSpan.FromMinutes(5);

        public UsersFacade(
            IRepositoryManager repositoryManager, 
            IMemoryCache memoryCache, 
            IHttpContextAccessor contextAccessor
        ) 
            : base(repositoryManager, memoryCache)
        {
            _contextAccessor = contextAccessor;
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<int> GetUsersCountAsync(int perPage, bool cache)
        {
            perPage = perPage > 0 ? perPage : 1;

            var requirements = new AnySpecification<User>();

            var newsCount = await GetCountAsync(requirements, Repository, cache, CacheTime);

            return (int)Math.Ceiling(newsCount / (float)perPage);
        }

        // TODO: FIXME: Extract to classes or smth else
        public async Task<IEnumerable<User>> GetUsersAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await Repository.GetAllWithPagingAndPersonAsync(skip, perPage);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var userId = _contextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (userId == null)
                return null;

            var userIdGuid = Guid.Parse(userId);

            if (userIdGuid == Guid.Empty)
                return null;

            return await Repository.FirstOrDefaultAsync(userIdGuid);
        }
    }
}