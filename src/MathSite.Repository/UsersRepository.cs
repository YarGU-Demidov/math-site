using System;
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
    }

    public class UsersRepository : EfCoreRepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
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
    }
}