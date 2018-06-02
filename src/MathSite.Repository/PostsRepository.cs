using MathSite.Db;
using MathSite.Entities;
using MathSite.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Repository
{
    public interface IPostsRepository : IMathSiteEfCoreRepository<Post>
    {
        IPostsRepository WithAuthor();
        IPostsRepository WithPostSeoSettings();
        IPostsRepository WithPostSetttings();
        IPostsRepository WithPostType();
        IPostsRepository WithComments();
        IPostsRepository WithPostRatings();
        IPostsRepository WithCategories();
    }

    public class PostsRepository : MathSiteEfCoreRepositoryBase<Post>, IPostsRepository
    {
        public PostsRepository(MathSiteDbContext dbContext) 
            : base(dbContext)
        {
        }

        public IPostsRepository WithAuthor()
        {
            var query = GetCurrentQuery()
                .Include(post => post.Author)
                .ThenInclude(user => user.Person);

            SetCurrentQuery(query);

            return this;
        }

        public IPostsRepository WithPostSeoSettings()
        {
            var query = GetCurrentQuery()
                .Include(post => post.PostSeoSetting)
                // с этой припиской +1 запрос идёт (на вычитывание keywords)
                /*.ThenInclude(setting => setting.PostKeywords)*/;

            SetCurrentQuery(query);

            return this;
        }

        public IPostsRepository WithPostSetttings()
        {
            var query = GetCurrentQuery()
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PostType)
                .Include(post => post.PostSettings).ThenInclude(setting => setting.PreviewImage);

            SetCurrentQuery(query);

            return this;
        }

        public IPostsRepository WithPostType()
        {
            SetCurrentQuery(
                GetCurrentQuery().Include(post => post.PostType).ThenInclude(type => type.DefaultPostsSettings)
            );

            return this;
        }

        public IPostsRepository WithComments()
        {
            SetCurrentQuery(GetCurrentQuery().Include(post => post.Comments));
            return this;
        }

        public IPostsRepository WithPostRatings()
        {
            SetCurrentQuery(GetCurrentQuery().Include(post => post.PostRatings));
            return this;
        }

        public IPostsRepository WithCategories()
        {
            SetCurrentQuery(
                GetCurrentQuery().Include(post => post.PostCategories).ThenInclude(category => category.Category)
            );
            return this;
        }
    }
}