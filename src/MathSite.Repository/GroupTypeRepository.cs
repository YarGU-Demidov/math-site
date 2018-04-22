using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IGroupTypeRepository : IMathSiteEfCoreRepository<GroupType>
    {
    }

    public class GroupTypeRepository : MathSiteEfCoreRepositoryBase<GroupType>, IGroupTypeRepository
    {
        public GroupTypeRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}