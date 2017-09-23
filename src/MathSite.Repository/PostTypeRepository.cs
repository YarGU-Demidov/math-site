using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPostTypeRepository : IRepository<PostType>
    {
    }

    public class PostTypeRepository : EfCoreRepositoryBase<PostType>, IPostTypeRepository
    {
        public PostTypeRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}