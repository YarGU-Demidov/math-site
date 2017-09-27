using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using MathSite.Repository.Core;
using MathSite.Specifications.Posts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseFacade, IPostsFacade
    {
        private readonly ILogger<IPostsFacade> _postsFacadeLogger;
        private const int CacheMinutes = 10;

        public PostsFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache, ISiteSettingsFacade siteSettingsFacade, ILogger<IPostsFacade> postsFacadeLogger)
            : base(repositoryManager, memoryCache)
        {
            _postsFacadeLogger = postsFacadeLogger;
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        public async Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var postsCacheKey = $"Last{count}FeaturedPosts";

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(PostTypeAliases.News)
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostOnStartPageSpecification())
                    .And(new PostPublishedSpecification());

                return await RepositoryManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(
                    requirements, 
                    count
                );
            });
        }

        public async Task<IEnumerable<Post>> GetAllNewsAsync(int page, int perPage, bool includeDeleted = false, bool onlyDeleted = false)
        {
            return await GetAllPostsWithDataAsync(PostTypeAliases.News, page, perPage, includeDeleted, onlyDeleted);
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
            catch(Exception e)
            {
                _postsFacadeLogger.LogError(e, "Can't create post. Exception was thrown.");
                return Guid.Empty;
            }
        }

        public async Task<Post> GetNewsPostByUrlAsync(string url)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Low;

            var postCacheKey = $"NewsPostData:{url}";
            
            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(PostTypeAliases.News)
                    .And(new PostWithSpecifiedUrlSpecification(url))
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostPublishedSpecification());

                return await RepositoryManager.PostsRepository.FirstOrDefaultAsync(requirements.ToExpression());
            });
        }

        public async Task<Post> GetStaticPageByUrlAsync(string url)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Low;

            var postCacheKey = $"StaticPagePostData:{url}";
            
            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(PostTypeAliases.StaticPage)
                    .And(new PostWithSpecifiedUrlSpecification(url))
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostPublishedSpecification());

                return await RepositoryManager.PostsRepository.FirstOrDefaultAsync(requirements);
            });
        }

        public async Task<IEnumerable<Post>> GetNewsAsync(int page)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var postsCacheKey = $"NewsPage{page}";
            
            var perPage = await GetPerPageCount();

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await GetAllPostsWithDataAsync(PostTypeAliases.News, page, perPage);
            });
        }

        private async Task<IEnumerable<Post>> GetAllPostsWithDataAsync(string postTypeAlias, int page, int perPage, bool includeDeleted = false, bool onlyDeleted = false)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            var toSkip = perPage * (page - 1);

            Specification<Post> requirements = new PostWithTypeAliasSpecification(postTypeAlias);

            if (onlyDeleted)
            {
                requirements = requirements.And(new PostDeletedSpecification());
            }
            else if (!includeDeleted)
            {
                requirements = requirements.AndNot(new PostDeletedSpecification());
            }

            return await RepositoryManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(requirements, perPage, toSkip);
        }

        public async Task<int> GetNewsPagesCountAsync()
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;
            const string newsPagesCountCacheKey = "NewsPage-PagesCount";

            var newsCount = await MemoryCache.GetOrCreateAsync(newsPagesCountCacheKey, async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));
                entry.Priority = cachePriority;
                
                var requirements = new PostWithTypeAliasSpecification(PostTypeAliases.News)
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostOnStartPageSpecification())
                    .And(new PostPublishedSpecification());

                return await RepositoryManager.PostsRepository.CountAsync(requirements);
            });

            var perPage = await GetPerPageCount();

            return (int) Math.Ceiling(newsCount / (float) perPage);
        }

        private async Task<int> GetPerPageCount(TimeSpan? cacheTime = null)
        {
            const string perPageCacheKey = "NewsPage-PerPage";
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            cacheTime = cacheTime ?? TimeSpan.FromMinutes(CacheMinutes);

            return await MemoryCache.GetOrCreateAsync(perPageCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(cacheTime.Value);

                var perPageSetting = await SiteSettingsFacade["PerPageNews"];

                return int.Parse(perPageSetting ?? "5");
            });
        }
    }
}