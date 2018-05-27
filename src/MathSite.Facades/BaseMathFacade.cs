using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common.Entities;
using MathSite.Common.Specifications;
using MathSite.Repository.Core;

namespace MathSite.Facades
{
    public class BaseMathFacade<TRepository, TEntity> : BaseMathFacade<TRepository, TEntity, Guid>
        where TEntity : class, IEntity<Guid>
        where TRepository : class, IMathSiteEfCoreRepository<TEntity, Guid>
    {
        public BaseMathFacade(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
        }
    }

    public class BaseMathFacade<TRepository, TEntity, TPrimaryKey> : BaseMathFacade
        where TEntity : class, IEntity<TPrimaryKey>
        where TRepository : class, IMathSiteEfCoreRepository<TEntity, TPrimaryKey>
    {
        public BaseMathFacade(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
            Repository = repositoryManager.TryGetRepository<TRepository>();
        }

        protected TRepository Repository { get; }

        protected async Task<int> GetCountAsync(
            Expression<Func<TEntity, bool>> requirements
        )
        {
            return await GetCountAsync(requirements, Repository);
        }

        public async Task<IEnumerable<TEntity>> GetItemsForPageAsync(
            Func<TRepository, TRepository> config,
            Expression<Func<TEntity, bool>> requirements,
            int page,
            int perPage
        )
        {
            return await GetItemsForPageAsync(config(Repository), requirements, page, perPage);
        }

        public async Task<IEnumerable<TEntity>> GetItemsForPageAsync(Expression<Func<TEntity, bool>> requirements, int page, int perPage)
        {
            return await GetItemsForPageAsync(Repository, requirements, page, perPage);
        }

        public async Task<IEnumerable<TEntity>> GetItemsForPageAsync(
            Func<TRepository, TRepository> config,
            int page,
            int perPage
        )
        {
            return await GetItemsForPageAsync(config(Repository), new AnySpecification<TEntity>(), page, perPage);
        }

        public async Task<IEnumerable<TEntity>> GetItemsForPageAsync(int page, int perPage)
        {
            return await GetItemsForPageAsync(Repository, new AnySpecification<TEntity>(), page, perPage);
        }
    }

    public abstract class BaseMathFacade : IFacade
    {
        public BaseMathFacade(IRepositoryManager repositoryManager)
        {
            RepositoryManager = repositoryManager;
        }

        protected IRepositoryManager RepositoryManager { get; }

        protected async Task<int> GetCountAsync<TEntity, TKey>(
            Expression<Func<TEntity, bool>> requirements,
            IRepository<TEntity, TKey> repo
        )
            where TEntity : class, IEntity<TKey>
        {
            
            return await repo.CountAsync(requirements);
        }

        protected int GetPagesCount(int perPage, int totalItems)
        {
            return (int) Math.Ceiling(totalItems / (float) perPage);
        }

        protected async Task<IEnumerable<TEntity>> GetItemsForPageAsync<TEntity, TPrimaryKey>(
            IMathSiteEfCoreRepository<TEntity, TPrimaryKey> repo,
            Expression<Func<TEntity, bool>> requirements,
            int page,
            int perPage,
            bool desc = true
        ) where TEntity : class, IEntity<TPrimaryKey>
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var skip = (page - 1) * perPage;

            return await repo
                .GetAllPagedAsync(requirements, perPage, skip, desc);
        }
    }
}