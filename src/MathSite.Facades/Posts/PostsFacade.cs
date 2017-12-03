using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Specifications.Posts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseFacade, IPostsFacade
    {
        private TimeSpan CacheMinutes { get; } = TimeSpan.FromMinutes(10);
        private readonly ILogger<IPostsFacade> _postsFacadeLogger;
        private readonly IUserValidationFacade _userValidation;
        private readonly IUsersFacade _usersFacade;

        public PostsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache,
            ISiteSettingsFacade siteSettingsFacade, 
            ILogger<IPostsFacade> postsFacadeLogger, 
            IUserValidationFacade userValidation,
            IUsersFacade usersFacade
        )
            : base(repositoryManager, memoryCache)
        {
            _postsFacadeLogger = postsFacadeLogger;
            _userValidation = userValidation;
            _usersFacade = usersFacade;
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }
        
        public async Task<int> GetPostPagesCountAsync(string postTypeAlias, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache)
        {
            var perPage = await GetPerPageCountAsync(cache);

            return await GetPostPagesCountAsync(postTypeAlias, perPage, state, publishState, frontPageState, cache);
        }

        public async Task<int> GetPostPagesCountAsync(string postTypeAlias, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache)
        {
            var newsCount = await GetPostsWithTypeCount(postTypeAlias, state, publishState, frontPageState, cache);

            return (int)Math.Ceiling(newsCount / (float)perPage);
        }


        public async Task<Post> GetPostByUrlAndTypeAsync(Guid currentUserId, string url, string postTypeAlias, bool cache)
        {
            var requirements = new PostWithTypeAliasSpecification(postTypeAlias)
                .And(new PostWithSpecifiedUrlSpecification(url));

            var userExists = await _usersFacade.DoesUserExistsAsync(currentUserId);
            var hasRightToViewRemovedAndUnpublished = await _userValidation.UserHasRightAsync(currentUserId, RightAliases.ManageNewsAccess);

            if (!userExists || !hasRightToViewRemovedAndUnpublished)
                requirements = requirements.And(new PostPublishedSpecification())
                    .AndNot(new PostDeletedSpecification());

            const CacheItemPriority cachePriority = CacheItemPriority.Low;

            var postCacheKey = $"{postTypeAlias}:PostData:{url}";

            return cache
                ? await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
                {
                    entry.Priority = cachePriority;
                    entry.SetSlidingExpiration(CacheMinutes);

                    return await RepositoryManager.PostsRepository.FirstOrDefaultAsync(requirements);
                })
                : await RepositoryManager.PostsRepository.FirstOrDefaultAsync(requirements);
        }


        public async Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, bool cache)
        {
            return await GetPostsAsync(postTypeAlias, page, await GetPerPageCountAsync(cache), RemovedStateRequest.Excluded, PublishStateRequest.Published, FrontPageStateRequest.AllVisibilityStates, cache);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var toSkip = perPage * (page - 1);

            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState);

            var cacheKey = $"Post={postTypeAlias}:Page={page}:PerPage={perPage}:Removed={state}:Published={publishState}:FrontPage={frontPageState}";

            return cache
                ? await MemoryCache.GetOrCreateAsync(
                    cacheKey,
                    async entry =>
                    {
                        entry.SetPriority(cachePriority);
                        entry.SetSlidingExpiration(CacheMinutes);

                        return await RepositoryManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(requirements, perPage, toSkip);
                    })
                : await RepositoryManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(requirements, perPage, toSkip);
        }


        public async Task<Guid> CreatePostAsync(Post post, PostSeoSetting seoSettings, PostSetting settings = null)
        {
            try
            {
                var seoSettingsId = await RepositoryManager.PostSeoSettingsRepository.InsertAndGetIdAsync(seoSettings);
                var settingsId = await RepositoryManager.PostSettingRepository.InsertAndGetIdAsync(settings);

                post.PostSeoSettingsId = seoSettingsId;
                post.PostSettingsId = settingsId;

                return await RepositoryManager.PostsRepository.InsertAndGetIdAsync(post);
            }
            catch (Exception e)
            {
                _postsFacadeLogger.LogError(e, "Can't create post. Exception was thrown.");
                return Guid.Empty;
            }
        }


        private async Task<int> GetPerPageCountAsync(bool cache)
        {
            return int.Parse(await SiteSettingsFacade.GetStringSettingAsync(SiteSettingsNames.PerPage, cache) ?? "5");
        }
        
        private async Task<int> GetPostsWithTypeCount(string postTypeAlias, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache)
        {
            var requirements = CreateRequirements(postTypeAlias, state, publishState, frontPageState);

            return await GetCountAsync(requirements, RepositoryManager.PostsRepository, cache, CacheMinutes);
        }

        private static Specification<Post> CreateRequirements(string postTypeAlias, RemovedStateRequest state,
            PublishStateRequest publishState, FrontPageStateRequest frontPageState)
        {
            Specification<Post> requirements = new PostWithTypeAliasSpecification(postTypeAlias);

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

            return requirements;
        }
    }
}