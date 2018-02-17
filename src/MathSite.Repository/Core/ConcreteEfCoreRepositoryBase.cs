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
        protected IQueryable<TEntity> QueryBuilder;

        public MathSiteEfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<TEntity> GetAll()
        {
            if (QueryBuilder.IsNull()) 
                return base.GetAll();

            var tmpQuery = QueryBuilder;
            QueryBuilder = null;
            return tmpQuery;
        }

        protected IQueryable<TEntity> GetCurrentQuery()
        {
            return QueryBuilder ?? GetAll();
        }
    }
}