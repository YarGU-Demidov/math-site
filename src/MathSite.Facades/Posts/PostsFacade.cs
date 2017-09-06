using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Posts
{
	public interface IPostsFacade
	{
		Task<bool> IsUrlFreeAsync(string url);
		Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count);
	}
	
	public class PostsFacade : BaseFacade, IPostsFacade
	{
		private const int PostsCacheMinutes = 10;
		
		public PostsFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache) 
			: base(logicManager, memoryCache)
		{
		}


		public async Task<bool> IsUrlFreeAsync(string url)
		{
			var setting = await LogicManager.PostSeoSettingsLogic.TryGetByUrlAsync(url);
			return setting == null;
		}

		public async Task<IEnumerable<Post>> GetLastSelectedForMainPagePostsAsync(int count)
		{
			const CacheItemPriority cachePriority = CacheItemPriority.Normal;
			var postsCacheKey = $"Last{count}FeaturedPosts";
			var postTypeCacheKey = $"Last{count}FeaturedPostsType";
			
			var postType = await MemoryCache.GetOrCreateAsync(postTypeCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(PostsCacheMinutes));
				return await LogicManager.PostTypeLogic.TryGetTypeByName(PostTypeAliases.News);
			});
			
			return await MemoryCache.GetOrCreateAsync(postsCacheKey, async entry =>
			{
				entry.Priority = cachePriority;
				entry.SetSlidingExpiration(TimeSpan.FromMinutes(PostsCacheMinutes));
				return await LogicManager.PostsLogic.TryGetMainPagePostsWithAllDataAsync(count, postType.TypeName);
			});
		}
	}
}