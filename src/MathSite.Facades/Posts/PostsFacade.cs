using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Posts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseMathFacade<IPostsRepository, Post>, IPostsFacade
    {
        private readonly ILogger<IPostsFacade> _postsFacadeLogger;
        private readonly IUsersFacade _usersFacade;
        private readonly IUserValidationFacade _userValidation;

        public PostsFacade(IRepositoryManager repositoryManager,
            ISiteSettingsFacade siteSettingsFacade,
            ILogger<IPostsFacade> postsFacadeLogger,
            IUserValidationFacade userValidation,
            IUsersFacade usersFacade
        ) : base(repositoryManager)
        {
            _postsFacadeLogger = postsFacadeLogger;
            _userValidation = userValidation;
            _usersFacade = usersFacade;
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        public async Task<int> GetPostPagesCountAsync(
            string postTypeAlias,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState
        )
        {
            return await GetPostPagesCountAsync(null, postTypeAlias, perPage, state, publishState, frontPageState);
        }

        public async Task<int> GetPostPagesCountAsync(
            Guid? categoryId,
            string postTypeAlias,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState
        )
        {
            var newsCount = await GetPostsWithTypeCount(
                postTypeAlias,
                categoryId,
                state,
                publishState,
                frontPageState
            );

            return GetPagesCount(perPage, newsCount);
        }


        public async Task<Post> GetPostByUrlAndTypeAsync(
            Guid currentUserId,
            string url,
            string postTypeAlias
        )
        {
            var requirements = new PostWithTypeAliasSpecification(postTypeAlias)
                .And(new PostWithSpecifiedUrlSpecification(url));

            var userExists = await _usersFacade.DoesUserExistsAsync(currentUserId);
            var hasRightToViewRemovedAndUnpublished =
                await _userValidation.UserHasRightAsync(currentUserId, RightAliases.ManageNewsAccess);

            if (!userExists || !hasRightToViewRemovedAndUnpublished)
                requirements = requirements.And(new PostPublishedSpecification())
                    .AndNot(new PostDeletedSpecification());
            
            return await Repository
                .WithPostSetttings()
                .WithPostSeoSettings()
                .FirstOrDefaultAsync(requirements);
        }

        public async Task<Post> GetPostAsync(Guid id)
        {
            return await Repository
                .WithPostSeoSettings()
                .WithPostSetttings()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(
            Guid? categoryId, 
            string postTypeAlias, 
            int page, 
            int perPage, 
            RemovedStateRequest state,
            PublishStateRequest publishState, 
            FrontPageStateRequest frontPageState,
            bool sortByPublish = true
        )
        {
            return await GetPostsAsync(categoryId, postTypeAlias, page, perPage, state, publishState, frontPageState, null, sortByPublish);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(
            string postTypeAlias, 
            int page, 
            int perPage, 
            RemovedStateRequest state,
            PublishStateRequest publishState, 
            FrontPageStateRequest frontPageState, 
            IEnumerable<Category> excludedCategories,
            bool sortByPublish = true
        )
        {
            return await GetPostsAsync(null, postTypeAlias, page, perPage, state, publishState, frontPageState, excludedCategories, sortByPublish);
        }

        public Task<IEnumerable<Post>> GetPostsAsync(
            string postTypeAlias, 
            int page, 
            int perPage,
            RemovedStateRequest state, 
            PublishStateRequest publishState, 
            FrontPageStateRequest frontPageState,
            bool sortByPublish = true
        )
        {
            return GetPostsAsync(null, postTypeAlias, page, perPage, state, publishState, frontPageState, null, sortByPublish);
        }

        public async Task<Guid> CreatePostAsync(Post post)
        {
            return await Repository.InsertAndGetIdAsync(post);
        }

        private async Task<IEnumerable<Post>> GetPostsAsync(
            Guid? categoryId,
            string postTypeAlias,
            int page,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            IEnumerable<Category> excludedCategories,
            bool sortByPublish
        )
        {
            var localExcludedCategories = excludedCategories as Category[] ?? excludedCategories?.ToArray();

            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState, categoryId, localExcludedCategories);
            
            return await GetItemsForPageAsync(
                repository => repository.WithPostSeoSettings().WithPostType().WithPostSetttings().OrderBy(post => sortByPublish ? post.PublishDate : post.CreationDate, false) as IPostsRepository,
                requirements,
                page,
                perPage
            );
        }

        public async Task<Guid> UpdatePostAsync(Post post)
        {
            try
            {
                return await Repository.InsertOrUpdateAndGetIdAsync(post);
            }
            catch (Exception e)
            {
                _postsFacadeLogger.LogError(e, "Can't update post. Exception was thrown.");
                return Guid.Empty;
            }
        }

        public async Task DeletePostAsync(Guid id)
        {
            try
            {
                await RepositoryManager.PostsRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _postsFacadeLogger.LogError(e, "Can't delete post. Exception was thrown.");
            }
        }

        public async Task<PostType> GetPostTypeAsync(string alias)
        {
            return await RepositoryManager.PostTypeRepository.SingleAsync(postType => postType.Alias == alias);
        }

        private async Task<int> GetPostsWithTypeCount(
            string postTypeAlias,
            Guid? categoryId,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState
        )
        {
            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState, categoryId);

            return await GetCountAsync(requirements);
        }

        private static Specification<Post> CreateRequirements(string postTypeAlias,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            Guid? categoryId = null, 
            ICollection<Category> excludedCategories = null
        )
        {
            var requirements = postTypeAlias.IsNotNull()
                ? new PostWithTypeAliasSpecification(postTypeAlias)
                : (Specification<Post>) new AnySpecification<Post>();

            if (state == RemovedStateRequest.OnlyRemoved)
                requirements = requirements.And(new PostDeletedSpecification());
            else if (state == RemovedStateRequest.Excluded)
                requirements = requirements.AndNot(new PostDeletedSpecification());

            if (publishState == PublishStateRequest.Published)
                requirements = requirements.And(new PostPublishedSpecification());
            else if (publishState == PublishStateRequest.Unpubished)
                requirements = requirements.AndNot(new PostPublishedSpecification());

            if (frontPageState == FrontPageStateRequest.Visible)
                requirements = requirements.And(new PostOnStartPageSpecification());
            else if (frontPageState == FrontPageStateRequest.Invisible)
                requirements = requirements.AndNot(new PostOnStartPageSpecification());

            if (categoryId.HasValue)
                requirements = requirements.And(new PostHasCategorySpecification(categoryId.Value));

            if (excludedCategories.IsNotNullOrEmpty()) 
                requirements = (excludedCategories ?? throw new ArgumentNullException(nameof(excludedCategories))).Aggregate(
                    requirements, 
                    (current, excludedCategory) => current.And(new PostNotInCategorySpecification(excludedCategory))
                );

            return requirements;
        }
    }
}