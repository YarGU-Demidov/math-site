using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> FirstOrDefaultWithRightsAsync(Expression<Func<User, bool>> predicate);
        Task<User> FirstOrDefaultWithRightsAsync(Guid id);
        Task<IEnumerable<User>> GetAllWithPagingAsync(int skip, int count);
        Task SetUserKey(string login, byte[] key);
        IUsersRepository WithPerson();
    }

    public class UsersRepository : EfCoreRepositoryBase<User>, IUsersRepository
    {
        private bool _loadPerson;

        public UsersRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<User> GetAll()
        {
            return _loadPerson 
                ? base.GetAll()
                    .Include(user => user.Person) 
                : base.GetAll();
        }

        public async Task<User> FirstOrDefaultWithRightsAsync(Expression<Func<User, bool>> predicate)
        {
            return await Table
                .Include(u => u.UserRights).ThenInclude(ur => ur.Right)
                .Include(u => u.Group).ThenInclude(g => g.GroupsRights).ThenInclude(gr => gr.Right)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<User> FirstOrDefaultWithRightsAsync(Guid id)
        {
            return await FirstOrDefaultWithRightsAsync(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllWithPagingAsync(int skip, int count)
        {
            return await WithPerson()
                .GetAllWithPaging(skip, count)
                .ToArrayAsync();
        }

        public async Task SetUserKey(string login, byte[] key)
        {
            var user =await Context.Users.Where(u => u.Login == login).FirstOrDefaultAsync();
            user.TwoFactorAutentificationKey = key;
            Context.SaveChanges();
        }
        public IUsersRepository WithPerson()
        {
            _loadPerson = true;
            return this;
        }
    }
}