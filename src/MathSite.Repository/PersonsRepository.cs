using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;

namespace MathSite.Repository
{
    public interface IPersonsRepository : IRepository<Person>
    {
        
    }

    public class PersonsRepository : EfCoreRepositoryBase<Person>, IPersonsRepository
    {
        public PersonsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}