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
	public interface IPostsFacade
	{
		Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count);
		Task<Post> GetNewsPostByUrlAsync(string url);
		Task<IEnumerable<Post>> GetNewsAsync(int page);
	}
	
	public class PostsFacade : BaseFacade, IPostsFacade
	{
		public ISiteSettingsFacade SiteSettingsFacade { get; }

		public PostsFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache, ISiteSettingsFacade siteSettingsFacade) 
			: base(logicManager, memoryCache)
		{
			SiteSettingsFacade = siteSettingsFacade;
		}

		public async Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count)
		{
			const CacheItemPriority cachePriority = CacheItemPriority.Normal;
			const int featuredPostsCacheMinutes = 10;

			var postsCacheKey = $"Last{count}FeaturedPosts";
			var postTypeCacheKey = $"Last{count}FeaturedPostsType";
			
			var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(featuredPostsCacheMinutes));
				return await LogicManager.PostTypeLogic.TryGetTypeByAlias(PostTypeAliases.News);
			});
			
			return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(featuredPostsCacheMinutes));
				return await LogicManager.PostsLogic.TryGetMainPagePostsWithAllDataAsync(count, postType.Alias);
			});
		}

		public async Task<Post> GetNewsPostByUrlAsync(string url)
		{
			const CacheItemPriority cachePriority = CacheItemPriority.Low;

			const string postTypeCacheKey = "NewsPostType";
			const int postCacheMinutes = 10;
			const int postTypeCacheHours = 12;

			var postCacheKey = $"NewsPostData:{url}";

			var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(postTypeCacheHours));
				return await LogicManager.PostTypeLogic.TryGetTypeByAlias(PostTypeAliases.News);
			});

			return await MemoryCache.GetOrCreateAsync(postCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(postCacheMinutes));
				return await LogicManager.PostsLogic.TryGetActivePostByUrlAndTypeAsync(url, postType.Alias);
			});
		}

		public async Task<IEnumerable<Post>> GetNewsAsync(int page)
		{
			const CacheItemPriority cachePriority = CacheItemPriority.Normal;
			const string perPageCacheKey = "NewsPage-PerPage";
			const string postTypeCacheKey = "NewsPage-PostTypeFor";

			var newsPageCacheTime = TimeSpan.FromMinutes(10);
			var postsCacheKey = $"NewsPage{page}";

			var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(newsPageCacheTime);
				return await LogicManager.PostTypeLogic.TryGetTypeByAlias(PostTypeAliases.News);
			});

			var perPage = await MemoryCache.GetOrCreateAsync(perPageCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(newsPageCacheTime);

				var perPageSetting = await SiteSettingsFacade["PerPageNews"];

				return int.Parse(
					perPageSetting ?? "10"
				);
			});

			return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(newsPageCacheTime);
				return await LogicManager.PostsLogic.TryGetNews(perPage, page, postType.Alias);
			});
		}
	}
}