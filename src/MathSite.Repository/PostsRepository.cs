using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPostsRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllWithAllDataIncludedPagedAsync(Expression<Func<Post, bool>> predicate, int limit, int skip = 0, bool desc = true);
    }

    public class PostsRepository : EfCoreRepositoryBase<Post>, IPostsRepository
    {
        public PostsRepository(MathSiteDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetAllWithAllDataIncludedPagedAsync(Expression<Func<Post, bool>> predicate, int limit, int skip = 0, bool desc = true)
        {
            var query = Table.Where(predicate);

            query = desc 
                ? query.OrderByDescending(post => post.PublishDate) 
                : query.OrderBy(post => post.PublishDate);

            var queryWithIncludes = query
                .Include(post => post.Author).ThenInclude(user => user.Person)
                .Include(post => post.PostSeoSetting).ThenInclude(setting => setting.PostKeywords)
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PostType)
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PreviewImage)
                .Include(post => post.PostType).ThenInclude(type => type.DefaultPostsSettings)
                .Include(post => post.Comments)
                .Include(post => post.PostRatings)
                .PageBy(skip, limit);

            return await queryWithIncludes.ToArrayAsync();
        }
    }
}