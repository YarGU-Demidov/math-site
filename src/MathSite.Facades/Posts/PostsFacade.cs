using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseFacade, IPostsFacade
    {
        private const int CacheMinutes = 10;
        private const int CacheHours = 12;

        public PostsFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache,
            ISiteSettingsFacade siteSettingsFacade)
            : base(logicManager, memoryCache)
        {
            SiteSettingsFacade = siteSettingsFacade;
        }

        public ISiteSettingsFacade SiteSettingsFacade { get; }

        public async Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var postsCacheKey = $"Last{count}FeaturedPosts";
            var postTypeCacheKey = $"Last{count}FeaturedPostsType";

            var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostTypeLogic.TryGetByAliasAsync(PostTypeAliases.News);
            });

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostsLogic.TryGetMainPagePostsWithAllDataAsync(count, postType.Alias);
            });
        }

        public async Task<Post> GetNewsPostByUrlAsync(string url)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Low;
            const string postTypeCacheKey = "NewsPostType";

            var postCacheKey = $"NewsPostData:{url}";

            var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheHours));

                return await LogicManager.PostTypeLogic.TryGetByAliasAsync(PostTypeAliases.News);
            });

            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostsLogic.TryGetActivePostByUrlAndTypeAsync(url, postType.Alias);
            });
        }

        public async Task<Post> GetStaticPageByUrlAsync(string url)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Low;
            const string postTypeCacheKey = "StaticPagePostType";

            var postCacheKey = $"StaticPagePostData:{url}";

            var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostTypeLogic.TryGetByAliasAsync(PostTypeAliases.StaticPage);
            });

            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostsLogic.TryGetActivePostByUrlAndTypeAsync(url, postType.Alias);
            });
        }

        public async Task<IEnumerable<Post>> GetNewsAsync(int page)
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;
            const string postTypeCacheKey = "NewsPage-PostTypeFor";

            var postsCacheKey = $"NewsPage{page}";

            var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostTypeLogic.TryGetByAliasAsync(PostTypeAliases.News);
            });

            var perPage = await GetPerPageCount();

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                return await LogicManager.PostsLogic.TryGetNewsAsync(perPage, page, postType.Alias);
            });
        }

        public async Task<int> GetNewsPagesCountAsync()
        {
            const CacheItemPriority cachePriority = CacheItemPriority.Normal;
            const string newsPagesCountCacheKey = "NewsPage-PagesCount";

            var newsCount = await MemoryCache.GetOrCreateAsync(newsPagesCountCacheKey, async entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));
                entry.Priority = cachePriority;

                return await LogicManager.PostsLogic.GetPostsCountAsync(PostTypeAliases.News);
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