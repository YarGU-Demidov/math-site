using System;
using System.Linq;
using MathSite.Common.Entities;
using MathSite.Common.Extensions;
using MathSite.Db;

namespace MathSite.Repository.Core
{
    public class MathSiteEfCoreRepositoryBase<TEntity> :
        MathSiteEfCoreRepositoryBase<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        public MathSiteEfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class MathSiteEfCoreRepositoryBase<TEntity, TPrimaryKey> :
        EfCoreRepositoryBase<MathSiteDbContext, TEntity, TPrimaryKey>
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
    }
}