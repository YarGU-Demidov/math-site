using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common.Entities;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades
{
    public class BaseFacade
    {
        public BaseFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
        {
            RepositoryManager = repositoryManager;
            MemoryCache = memoryCache;
        }

        public IRepositoryManager RepositoryManager { get; }
        public IMemoryCache MemoryCache { get; }


        protected async Task<int> GetCountAsync<TEntity, TKey>(
            Expression<Func<TEntity, bool>> requirements,
            IRepository<TEntity, TKey> repo,
            bool cache,
            TimeSpan? expirationTime = null
        )
            where TEntity : class, IEntity<TKey>
        {
            if (cache && expirationTime == null)
                throw new ArgumentNullException(nameof(expirationTime));

            return cache
                ? await MemoryCache.GetOrCreateAsync(
                    $"{typeof(TEntity).Namespace}.{typeof(TEntity).Name}:Count",
                    async entry =>
                    {
                        entry.SetSlidingExpiration(expirationTime.Value);
                        entry.SetPriority(CacheItemPriority.Low);

                        return await repo.CountAsync(requirements);
                    })
                : await repo.CountAsync(requirements);
        }
    }
}