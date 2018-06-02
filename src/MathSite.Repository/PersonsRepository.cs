using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPersonsRepository : IMathSiteEfCoreRepository<Person>
    {
        IPersonsRepository WithUser();
        IPersonsRepository WithProfessor();
        IPersonsRepository WithPhoto();
    }

    public class PersonsRepository : MathSiteEfCoreRepositoryBase<Person>, IPersonsRepository
    {
        public PersonsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public IPersonsRepository WithUser()
        {
            SetCurrentQuery(GetCurrentQuery().Include(person => person.User));
            return this;
        }

        public IPersonsRepository WithProfessor()
        {
            SetCurrentQuery(GetCurrentQuery().Include(person => person.Professor));
            return this;
        }

        public IPersonsRepository WithPhoto()
        {
            SetCurrentQuery(GetCurrentQuery().Include(person => person.Photo));
            return this;
        }
    }
}