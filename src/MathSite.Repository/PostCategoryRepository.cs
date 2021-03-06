﻿using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPostCategoryRepository : IMathSiteEfCoreRepository<PostCategory>
    {
    }

    public class PostCategoryRepository : MathSiteEfCoreRepositoryBase<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
