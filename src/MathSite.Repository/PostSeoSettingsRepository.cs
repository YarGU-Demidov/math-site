using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPostSeoSettingsRepository : IRepository<PostSeoSetting>
    {
        
    }

    public class PostSeoSettingsRepository : EfCoreRepositoryBase<PostSeoSetting>, IPostSeoSettingsRepository
    {
        public PostSeoSettingsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}