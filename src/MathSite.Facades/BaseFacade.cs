using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common.Entities;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades
{
    public class BaseFacade<TRepository, TEntity> : BaseFacade<TRepository, TEntity, Guid>
        where TEntity : class, IEntity<Guid>
        where TRepository : class, IRepository<TEntity, Guid>
    {
        public BaseFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }
    }


    public class BaseFacade<TRepository, TEntity, TPrimaryKey> : BaseFacade 
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : class, IRepository<TEntity, TPrimaryKey>
    {
        public BaseFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
            Repository = repositoryManager.TryGetRepository<TRepository>();
        }

        protected TRepository Repository { get; }

        protected async Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>> requirements, 
            bool cache, 
            TimeSpan? expirationTime = null,
            string cacheKey = null
        )
        {
            return await GetCountAsync(requirements, Repository, cache, expirationTime, cacheKey);
        }
    }

    public abstract class BaseFacade: IFacade
    {
        public BaseFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
        {
            RepositoryManager = repositoryManager;
            MemoryCache = memoryCache;
        }

        protected IRepositoryManager RepositoryManager { get; }
        protected IMemoryCache MemoryCache { get; }


        protected async Task<int> GetCountAsync<TEntity, TKey>(
            Expression<Func<TEntity, bool>> requirements,
            IRepository<TEntity, TKey> repo,
            bool cache,
            TimeSpan? expirationTime = null,
            string cacheKey = null
        )
            where TEntity : class, IEntity<TKey>
        {
            if (cache && expirationTime == null)
                throw new ArgumentNullException(nameof(expirationTime));

            return cache
                ? await MemoryCache.GetOrCreateAsync(
                    cacheKey ?? $"{typeof(TEntity).Namespace}.{typeof(TEntity).Name}:Count",
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