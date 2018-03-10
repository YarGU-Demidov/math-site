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
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllPagedAsync(Expression<Func<Category, bool>> predicate, int limit, int skip = 0, bool desc = true);
    }

    public class CategoryRepository : MathSiteEfCoreRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MathSiteDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllPagedAsync(Expression<Func<Category, bool>> predicate, int limit, int skip = 0, bool desc = true)
        {
            QueryBuilder = GetCurrentQuery().Where(predicate);

            Expression<Func<Category, DateTime>> orderBy = post => post.CreationDate;

            QueryBuilder = desc 
                ? QueryBuilder.OrderByDescending(orderBy) 
                : QueryBuilder.OrderBy(orderBy);

            return await GetAllWithPaging(skip, limit).ToArrayAsync();
        }
    }
}