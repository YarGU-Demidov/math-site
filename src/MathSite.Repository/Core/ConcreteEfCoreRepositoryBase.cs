using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common.Entities;
using MathSite.Common.Extensions;
using MathSite.Db;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository.Core
{
    public interface IMathSiteEfCoreRepository<TEntity> : IMathSiteEfCoreRepository<TEntity, Guid>
        where TEntity : class, IEntity<Guid> {}

    public interface IMathSiteEfCoreRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetAllPagedAsync(int skip, int count);
        Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int limit, int skip = 0, bool desc = true);
    }

    public class MathSiteEfCoreRepositoryBase<TEntity> :
        MathSiteEfCoreRepositoryBase<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        public MathSiteEfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class MathSiteEfCoreRepositoryBase<TEntity, TPrimaryKey> :
        EfCoreRepositoryBase<MathSiteDbContext, TEntity, TPrimaryKey>, IMathSiteEfCoreRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private IQueryable<TEntity> _queryBuilder;

        protected bool QueryInitialized { get; private set; }

        public MathSiteEfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        private IQueryable<TEntity> QueryBuilder
        {
            get => _queryBuilder;
            set
            {
                _queryBuilder = value;
                QueryInitialized = value.IsNotNull();
            }
        }

        public override IQueryable<TEntity> GetAll()
        {
            if (!QueryInitialized)
                return base.GetAll();

            var tmpQuery = QueryBuilder;
            SetCurrentQuery(null);
            return tmpQuery;
        }

        protected IQueryable<TEntity> GetCurrentQuery()
        {
            return QueryBuilder ?? GetAll();
        }

        protected void SetCurrentQuery(IQueryable<TEntity> query)
        {
            QueryBuilder = query;
        }

        public async Task<IEnumerable<TEntity>> GetAllPagedAsync(int skip, int count)
        {
            return await GetAllWithPaging(skip, count)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int limit, int skip = 0, bool desc = true)
        {
            SetCurrentQuery(GetCurrentQuery().Where(predicate));

            Expression<Func<TEntity, DateTime>> orderBy = post => post.CreationDate;

            var query = desc 
                ? GetCurrentQuery().OrderByDescending(orderBy) 
                : GetCurrentQuery().OrderBy(orderBy);

            SetCurrentQuery(query);

            return await GetAllWithPaging(skip, limit).ToArrayAsync();
        }
    }
}