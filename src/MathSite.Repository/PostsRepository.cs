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

    public class PostsRepository : EfCoreRepositoryBase<Post>, IPostsRepository
    {
        private IQueryable<Post> _query;
        
        public PostsRepository(MathSiteDbContext dbContext) 
            : base(dbContext)
        {
        }

        public override IQueryable<Post> GetAll()
        {
            if (_query.IsNull()) 
                return base.GetAll();

            var tmpQuery = _query;
            _query = null;
            return tmpQuery;
        }

        public IPostsRepository WithAuthor()
        {
            _query = GetCurrentQuery().Include(post => post.Author).ThenInclude(user => user.Person);
            return this;
        }

        public IPostsRepository WithPostSeoSettings()
        {
            _query = GetCurrentQuery().Include(post => post.PostSeoSetting).ThenInclude(setting => setting.PostKeywords);
            return this;
        }

        public IPostsRepository WithPostSetttings()
        {
            _query = GetCurrentQuery().Include(post => post.PostSettings).ThenInclude(setting => setting.PostType)
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PreviewImage);

            return this;
        }

        public IPostsRepository WithPostType()
        {
            _query = GetCurrentQuery().Include(post => post.PostType).ThenInclude(type => type.DefaultPostsSettings);

            return this;
        }

        public IPostsRepository WithComments()
        {
            _query = GetCurrentQuery().Include(post => post.Comments);
            return this;
        }

        public IPostsRepository WithPostRatings()
        {
            _query = GetCurrentQuery().Include(post => post.PostRatings);
            return this;
        }

        public IPostsRepository WithCategories()
        {
            _query = GetCurrentQuery().Include(post => post.PostCategories).ThenInclude(category => category.Category);
            return this;
        }

        private IQueryable<Post> GetCurrentQuery()
        {
            return _query ?? GetAll();
        }

        public async Task<IEnumerable<Post>> GetAllPagedAsync(Expression<Func<Post, bool>> predicate, int limit, int skip = 0, bool desc = true)
        {
            _query = GetCurrentQuery().Where(predicate);

            var query = GetAllWithPaging(skip, limit);

            Expression<Func<Post, DateTime>> orderBy = post => post.PublishDate;

            query = desc 
                ? query.OrderByDescending(orderBy) 
                : query.OrderBy(orderBy);

            return await query.ToArrayAsync();
        }
    }
}