using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPersonsRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllWithPagingAndUserAsync(int skip, int count);
    }

    public class PersonsRepository : EfCoreRepositoryBase<Person>, IPersonsRepository
    {
        public PersonsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Person>> GetAllWithPagingAndUserAsync(int skip, int count)
        {
            return await GetAllWithPaging(skip, count)
                .Include(person => person.User)
                .ToArrayAsync();
        }
    }
}