using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPersonsRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllWithPagingAsync(int skip, int count);
    }

    public class PersonsRepository : EfCoreRepositoryBase<Person>, IPersonsRepository
    {
        public PersonsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Person>> GetAllWithPagingAsync(int skip, int count)
        {
            return await GetAll()
                .PageBy(skip, count)
                .ToArrayAsync();
        }
    }
}