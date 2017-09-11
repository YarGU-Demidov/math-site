using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Posts
{
	public interface IPostsLogic
	{
		Task<Guid> CreateAsync(
			string title, 
			string excerpt, 
			string content, 
			bool published,
			DateTime publishDate,
			string postTypeAlias,
			Guid author,
			Guid? settings,
			Guid seoSettings
		);
		
		Task UpdateAsync(
			Guid id,
			string title, 
			string excerpt, 
			string content, 
			bool published,
			DateTime publishDate,
			string postTypeAlias,
			Guid author
		);
		
		Task DeleteAsync(Guid id);
		
		Task<Post> TryGetByIdAsync(Guid id);
		
		Task<Post> TryGetByUrlAsync(string url);
		Task<Post> TryGetActivePostByUrlAndTypeAsync(string url, string postType);
		
		Task<IEnumerable<Post>> TryGetMainPagePostsWithAllDataAsync(int count, string postTypeAlias);
		Task<IEnumerable<Post>> TryGetNews(int perPage, int page, string postTypeAlias);
	}
}