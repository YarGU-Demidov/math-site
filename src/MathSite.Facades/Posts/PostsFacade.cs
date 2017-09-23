using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.SiteSettings;
using MathSite.Repository.Core;
using MathSite.Specifications;
using MathSite.Specifications.Posts;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Posts
{
    public class PostsFacade : BaseFacade, IPostsFacade
    {
        private const int CacheMinutes = 10;
        private const int CacheHours = 12;

        public PostsFacade(IRepositoryManager logicManager, IMemoryCache memoryCache, ISiteSettingsFacade siteSettingsFacade)
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

                return await GetPostTypeByAliasAsync(PostTypeAliases.News);
            });

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(postType.Alias)
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostOnStartPageSpecification())
                    .And(new PostPublishedSpecification());

                return await LogicManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(
                    requirements.ToExpression(), 
                    count
                );
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

                return await GetPostTypeByAliasAsync(PostTypeAliases.News);
            });

            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(postType.Alias)
                    .And(new PostWithSpecifiedUrlSpecification(url))
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostPublishedSpecification());

                return await LogicManager.PostsRepository.FirstOrDefaultAsync(requirements.ToExpression());
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

                return await GetPostTypeByAliasAsync(PostTypeAliases.StaticPage);
            });

            return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));

                var requirements = new PostWithTypeAliasSpecification(postType.Alias)
                    .And(new PostWithSpecifiedUrlSpecification(url))
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostPublishedSpecification());

                return await LogicManager.PostsRepository.FirstOrDefaultAsync(requirements.ToExpression());
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

                return await GetPostTypeByAliasAsync(PostTypeAliases.News);
            });

            var perPage = await GetPerPageCount();

            return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
            {
                entry.Priority = cachePriority;
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(CacheMinutes));
                
                var requirements = new PostWithTypeAliasSpecification(postType.Alias)
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostOnStartPageSpecification())
                    .And(new PostPublishedSpecification());

                var toSkip = perPage * (page - 1);

                return await LogicManager.PostsRepository.GetAllWithAllDataIncludedPagedAsync(
                    requirements.ToExpression(),
                    perPage,
                    toSkip
                );
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

                var postType = await GetPostTypeByAliasAsync(PostTypeAliases.News);

                var requirements = new PostWithTypeAliasSpecification(postType.Alias)
                    .AndNot(new PostDeletedSpecification())
                    .And(new PostOnStartPageSpecification())
                    .And(new PostPublishedSpecification());

                return await LogicManager.PostsRepository.CountAsync(requirements.ToExpression());
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

        private async Task<PostType> GetPostTypeByAliasAsync(string postTypeAlias)
        {
            var requirements = new SameAliasSpecification<PostType>(postTypeAlias);

            return await LogicManager.PostTypeRepository.SingleAsync(requirements);
        }
    }
}