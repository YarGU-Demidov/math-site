using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IGroupTypeRepository : IRepository<GroupType>
    {
    }

    public class GroupTypeRepository : EfCoreRepositoryBase<GroupType>, IGroupTypeRepository
    {
        public GroupTypeRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}