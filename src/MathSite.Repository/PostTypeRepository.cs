using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPostTypeRepository : IMathSiteEfCoreRepository<PostType>
    {
        IPostTypeRepository WithDefaultPostSettings();
    }

    public class PostTypeRepository : MathSiteEfCoreRepositoryBase<PostType>, IPostTypeRepository
    {
        public PostTypeRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IPostTypeRepository WithDefaultPostSettings()
        {
            SetCurrentQuery(GetCurrentQuery().Include(type => type.DefaultPostsSettings));
            return this;
        }
    }
}