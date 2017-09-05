using System;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Posts
{
	public interface IPostsLogic
	{
		Task<Guid> CreatePostAsync(
			string title, 
			string excerpt, 
			string content, 
			bool published,
			DateTime publishDate,
			Guid postType,
			Guid author,
			Guid settings,
			Guid seoSettings
		);
		
		Task UpdatePostAsync(
			Guid id,
			string title, 
			string excerpt, 
			string content, 
			bool published,
			DateTime publishDate,
			Guid postType,
			Guid author
		);
		
		Task DeletePostAsync(Guid id);
		
		Task<Post> TryGetPostByIdAsync(Guid id);
		
		Task<Post> TryGetPostByUrlAsync(string url);
	}
}