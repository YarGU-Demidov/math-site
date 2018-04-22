using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IProfessorsRepository : IMathSiteEfCoreRepository<Professor>
    {
        IProfessorsRepository WithPerson();
    }

    public class ProfessorsRepository : MathSiteEfCoreRepositoryBase<Professor>, IProfessorsRepository
    {
        public ProfessorsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }


        public IProfessorsRepository WithPerson()
        {
            SetCurrentQuery(GetCurrentQuery().Include(professor => professor.Person));
            
            return this;
        }
    }
}