using System;
using System.Threading.Tasks;
using MathSite.Domain.Common;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Posts
{
	public interface IPostsFacade
	{
		Task<bool> IsUrlFreeAsync(string url);
	}
	
	public class PostsFacade : BaseFacade, IPostsFacade
	{
		public PostsFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache) 
			: base(logicManager, memoryCache)
		{
		}


		public async Task<bool> IsUrlFreeAsync(string url)
		{
			var setting = await LogicManager.PostSeoSettingsLogic.TryGetByUrlAsync(url);
			return setting == null;
		}

		public async Task<Guid> CreatePostAsync()
		{
			throw new NotImplementedException();
		}
	}
}