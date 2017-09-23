using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPostSettingRepository : IRepository<PostSetting>
    {
    }

    public class PostSettingRepository : EfCoreRepositoryBase<PostSetting>, IPostSettingRepository
    {
        public PostSettingRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}