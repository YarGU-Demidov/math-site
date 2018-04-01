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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseFacade<IPostsRepository, Post>, IPostsFacade
    {
        private readonly ILogger<IPostsFacade> _postsFacadeLogger;
        private readonly IUsersFacade _usersFacade;
        private readonly IUserValidationFacade _userValidation;

        public PostsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache,
            ISiteSettingsFacade siteSettingsFacade,
            ILogger<IPostsFacade> postsFacadeLogger,
            IUserValidationFacade userValidation,
            IUsersFacade usersFacade
        ) : base(repositoryManager, memoryCache)
        {
            _postsFacadeLogger = postsFacadeLogger;
            _userValidation = userValidation;
            _usersFacade = usersFacade;
            SiteSettingsFacade = siteSettingsFacade;
        }

        private TimeSpan CacheMinutes { get; } = TimeSpan.FromMinutes(10);

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        public async Task<int> GetPostPagesCountAsync(
            string postTypeAlias,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            bool cache
        )
        {
            return await GetPostPagesCountAsync(null, postTypeAlias, perPage, state, publishState, frontPageState, cache);
        }

        public async Task<int> GetPostPagesCountAsync(
            Guid? categoryId,
            string postTypeAlias,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            bool cache
        )
        {
            var newsCount = await GetPostsWithTypeCount(
                postTypeAlias,
                categoryId,
                state,
                publishState,
                frontPageState,
                cache
            );

            return (int) Math.Ceiling(newsCount / (float) perPage);
        }


        public async Task<Post> GetPostByUrlAndTypeAsync(
            Guid currentUserId,
            string url,
            string postTypeAlias,
            bool cache
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

            const CacheItemPriority cachePriority = CacheItemPriority.Low;

            var postCacheKey = $"{postTypeAlias}:PostData:{url}";

            async Task<Post> GetPost(Specification<Post> specifications)
            {
                return await Repository.WithPostSetttings().FirstOrDefaultAsync(specifications);
            }

            return cache
                ? await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
                {
                    entry.Priority = cachePriority;
                    entry.SetSlidingExpiration(CacheMinutes);

                    return await GetPost(requirements);
                })
                : await GetPost(requirements);
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
            bool cache
        )
        {
            return await GetPostsAsync(categoryId, postTypeAlias, page, perPage, state, publishState, frontPageState, null, cache);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(
            string postTypeAlias, 
            int page, 
            int perPage, 
            RemovedStateRequest state,
            PublishStateRequest publishState, 
            FrontPageStateRequest frontPageState, 
            IEnumerable<Category> excludedCategories, 
            bool cache
        )
        {
            return await GetPostsAsync(null, postTypeAlias, page, perPage, state, publishState, frontPageState, excludedCategories, cache);
        }

        public Task<IEnumerable<Post>> GetPostsAsync(
            string postTypeAlias, 
            int page, 
            int perPage,
            RemovedStateRequest state, 
            PublishStateRequest publishState, 
            FrontPageStateRequest frontPageState,
            bool cache
        )
        {
            return GetPostsAsync(null, postTypeAlias, page, perPage, state, publishState, frontPageState, null, cache);
        }

        public async Task<Guid> CreatePostAsync(Post post)
        {
            try
            {
                var seoSettingsId = await RepositoryManager.PostSeoSettingsRepository.InsertAndGetIdAsync(post.PostSeoSetting);
                var settingsId = await RepositoryManager.PostSettingRepository.InsertAndGetIdAsync(post.PostSettings);

                post.PostSeoSettingsId = seoSettingsId;
                post.PostSettingsId = settingsId;

                return await Repository.InsertAndGetIdAsync(post);
            }
            catch (Exception e)
            {
                _postsFacadeLogger.LogError(e, "Can't create post. Exception was thrown.");
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(
            Guid? categoryId,
            string postTypeAlias,
            int page,
            int perPage,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            IEnumerable<Category> excludedCategories,
            bool cache
        )
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var toSkip = perPage * (page - 1);

            var localExcludedCategories = excludedCategories as Category[] ?? excludedCategories?.ToArray();

            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState, categoryId, localExcludedCategories);

            var cacheKey =
                $"Post={postTypeAlias ?? "ALL"}:Page={page}:PerPage={perPage}:Removed={state}:Published={publishState}:FrontPage={frontPageState}";

            if (categoryId.HasValue)
                cacheKey += $":Category={categoryId.ToString()}";

            if (localExcludedCategories.IsNotNullOrEmpty())
                cacheKey += $":NotInCategories={localExcludedCategories.Select(category => category.Alias).Aggregate((f,s) => $"{f},{s}")}";

            async Task<IEnumerable<Post>> GetPosts(Specification<Post> specification, int perPageCount, int toSkipCount)
            {
                return await Repository
                    .WithPostSeoSettings()
                    .WithPostType()
                    .WithPostSetttings()
                    .GetAllPagedAsync(specification, perPageCount, toSkipCount);
            }

            return cache
                ? await MemoryCache.GetOrCreateAsync(
                    cacheKey,
                    async entry =>
                    {
                        entry.SetPriority(cachePriority);
                        entry.SetSlidingExpiration(CacheMinutes);

                        return await GetPosts(requirements, perPage, toSkip);
                    })
                : await GetPosts(requirements, perPage, toSkip);
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

        public async Task<PostSetting> GetPostSettingsAsync(Guid? id)
        {
            return await RepositoryManager.PostSettingRepository.SingleAsync(s => s.Id == id);
        }

        public async Task<PostSeoSetting> GetPostSeoSettingsAsync(Guid id)
        {
            return await RepositoryManager.PostSeoSettingsRepository.SingleAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Category>> GetPostCategoriesAsync()
        {
            return await RepositoryManager.CategoryRepository.GetAllListAsync();
        }

        private async Task<int> GetPostsWithTypeCount(
            string postTypeAlias,
            Guid? categoryId,
            RemovedStateRequest state,
            PublishStateRequest publishState,
            FrontPageStateRequest frontPageState,
            bool cache
        )
        {
            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState, categoryId);
            var cacheKey = $"PostType={postTypeAlias};Count";

            if (categoryId.HasValue)
                cacheKey += $";CategoryId={categoryId}";

            return await GetCountAsync(requirements, cache, CacheMinutes, cacheKey);
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
                requirements = excludedCategories.Aggregate(
                    requirements, 
                    (current, excludedCategory) => current.And(new PostNotInCategorySpecification(excludedCategory))
                );

            return requirements;
        }
    }
}