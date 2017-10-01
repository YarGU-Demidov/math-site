using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IRightsRepository : IRepository<Right>
    {
        
    }

    public class RightsRepository : EfCoreRepositoryBase<Right>, IRightsRepository
    {
        public RightsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}