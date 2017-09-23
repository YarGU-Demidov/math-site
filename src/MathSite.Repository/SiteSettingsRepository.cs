using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface ISiteSettingsRepository : IRepository<SiteSetting>
    {
    }

    public class SiteSettingsRepository : EfCoreRepositoryBase<SiteSetting>, ISiteSettingsRepository
    {
        public SiteSettingsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}