using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPostSettingRepository : IMathSiteEfCoreRepository<PostSetting>
    {
    }

    public class PostSettingRepository : MathSiteEfCoreRepositoryBase<PostSetting>, IPostSettingRepository
    {
        public PostSettingRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}