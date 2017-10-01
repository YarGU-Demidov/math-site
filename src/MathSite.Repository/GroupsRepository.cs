using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IGroupsRepository : IRepository<Group>
    {
        
    }

    public class GroupsRepository : EfCoreRepositoryBase<Group>, IGroupsRepository
    {
        public GroupsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}