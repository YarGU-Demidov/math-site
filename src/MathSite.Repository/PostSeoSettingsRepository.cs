using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPostSeoSettingsRepository : IRepository<PostSeoSetting>
    {
        IPostSeoSettingsRepository WithPost();
    }

    public class PostSeoSettingsRepository : MathSiteEfCoreRepositoryBase<PostSeoSetting>, IPostSeoSettingsRepository
    {
        public PostSeoSettingsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IPostSeoSettingsRepository WithPost()
        {
            SetCurrentQuery(GetCurrentQuery().Include(setting => setting.Post));
            return this;
        }
    }
}