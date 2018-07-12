using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IUsersRepository : IMathSiteEfCoreRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithPagingAsync(int skip, int count);
        IUsersRepository WithPerson();
        IUsersRepository WithRights();

        Task<List<User>> GetAllListOrderedByAsync<TKey>(Expression<Func<User, bool>> predicate,
            Expression<Func<User, TKey>> keySelector, bool isAscending);

    }

    public class UsersRepository : MathSiteEfCoreRepositoryBase<User>, IUsersRepository
    {

        public UsersRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllWithPagingAsync(int skip, int count)
        {
            return await GetAllWithPaging(skip, count)
                .ToArrayAsync();
        }

        public IUsersRepository WithPerson()
        {
            SetCurrentQuery(GetCurrentQuery().Include(user => user.Person));
            return this;
        }

        public IUsersRepository WithRights()
        {
            var query = GetCurrentQuery()
                .Include(u => u.UserRights).ThenInclude(ur => ur.Right)
                .Include(u => u.Group).ThenInclude(g => g.GroupsRights).ThenInclude(gr => gr.Right);

            SetCurrentQuery(query);

            return this;
        }

        public async Task<List<User>> GetAllListOrderedByAsync<TKey>(Expression<Func<User, bool>> predicate, Expression<Func<User, TKey>> keySelector, bool isAscending)
        {
            return await GetAll()
                .Where(predicate)
                .OrderBy(keySelector, isAscending)
                .ToListAsync();
        }

    }
}