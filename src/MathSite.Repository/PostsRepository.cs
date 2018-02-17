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
    public interface IPostsRepository : IRepository<Post>
    {
        IPostsRepository WithAuthor();
        IPostsRepository WithPostSeoSettings();
        IPostsRepository WithPostSetttings();
        IPostsRepository WithPostType();
        IPostsRepository WithComments();
        IPostsRepository WithPostRatings();
        IPostsRepository WithCategories();

        Task<IEnumerable<Post>> GetAllPagedAsync(Expression<Func<Post, bool>> predicate, int limit, int skip = 0, bool desc = true);
    }

    public class PostsRepository : MathSiteEfCoreRepositoryBase<Post>, IPostsRepository
    {
        public PostsRepository(MathSiteDbContext dbContext) 
            : base(dbContext)
        {
        }

        public IPostsRepository WithAuthor()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.Author).ThenInclude(user => user.Person);
            return this;
        }

        public IPostsRepository WithPostSeoSettings()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.PostSeoSetting).ThenInclude(setting => setting.PostKeywords);
            return this;
        }

        public IPostsRepository WithPostSetttings()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.PostSettings).ThenInclude(setting => setting.PostType)
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PreviewImage);

            return this;
        }

        public IPostsRepository WithPostType()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.PostType).ThenInclude(type => type.DefaultPostsSettings);

            return this;
        }

        public IPostsRepository WithComments()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.Comments);
            return this;
        }

        public IPostsRepository WithPostRatings()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.PostRatings);
            return this;
        }

        public IPostsRepository WithCategories()
        {
            QueryBuilder = GetCurrentQuery().Include(post => post.PostCategories).ThenInclude(category => category.Category);
            return this;
        }
        
        public async Task<IEnumerable<Post>> GetAllPagedAsync(Expression<Func<Post, bool>> predicate, int limit, int skip = 0, bool desc = true)
        {
            QueryBuilder = GetCurrentQuery().Where(predicate);

            Expression<Func<Post, DateTime>> orderBy = post => post.PublishDate;

            QueryBuilder = desc 
                ? QueryBuilder.OrderByDescending(orderBy) 
                : QueryBuilder.OrderBy(orderBy);

            return await GetAllWithPaging(skip, limit).ToArrayAsync();
        }
    }
}