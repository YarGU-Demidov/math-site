using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IRightsRepository : IMathSiteEfCoreRepository<Right>
    {
        
    }

    public class RightsRepository : MathSiteEfCoreRepositoryBase<Right>, IRightsRepository
    {
        public RightsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}