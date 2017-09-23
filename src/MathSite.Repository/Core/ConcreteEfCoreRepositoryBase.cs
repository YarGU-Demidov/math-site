using System;
using MathSite.Common.Entities;
using MathSite.Db;

namespace MathSite.Repository.Core
{
    public class EfCoreRepositoryBase<TEntity> :
        EfCoreRepositoryBase<MathSiteDbContext, TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        public EfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> :
        EfCoreRepositoryBase<MathSiteDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public EfCoreRepositoryBase(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}