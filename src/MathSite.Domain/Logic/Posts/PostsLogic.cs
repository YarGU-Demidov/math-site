using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Domain.Logic.Posts
{
	public class PostsLogic : LogicBase<Post>, IPostsLogic
	{
		public PostsLogic(MathSiteDbContext context) : base(context)
		{
		}

		public async Task<Guid> CreatePostAsync(string title, string excerpt, string content, bool published,
			DateTime publishDate, string postTypeName,
			Guid author, Guid settings, Guid seoSettings)
		{
			var id = Guid.Empty;
			await UseContextAsync(async context =>
			{
				var post = new Post
				{
					AuthorId = author,
					Content = content,
					Excerpt = excerpt,
					Published = published,
					PublishDate = publishDate,
					PostTypeAlias = postTypeName,
					Title = title,
					PostSettingsId = settings,
					PostSeoSettingsId = seoSettings
				};

				await context.Posts.AddAsync(post);
				await context.SaveChangesAsync();

				id = post.Id;
			});

			return id;
		}

		public async Task UpdatePostAsync(Guid id, string title, string excerpt, string content, bool published,
			DateTime publishDate,
			string postTypeName, Guid author)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var post = await GetFromItemsAsync(posts => posts.FirstAsync(p => p.Id == id));

				post.Title = title;
				post.Excerpt = excerpt;
				post.Content = content;
				post.Published = published;
				post.PublishDate = publishDate;
				post.PostTypeAlias = postTypeName;
				post.AuthorId = author;

				context.Posts.Update(post);
			});
		}

		public async Task DeletePostAsync(Guid id)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var post = await GetFromItemsAsync(posts => posts.FirstAsync(p => p.Id == id));

				context.Posts.Remove(post);
			});
		}

		public async Task<Post> TryGetPostByIdAsync(Guid id)
		{
			Post post = null;

			await UseContextAsync(async context =>
			{
				post = await GetFromItemsAsync(
					dbContext => dbContext.Posts
						.Include(p => p.PostSeoSetting)
						.Include(p => p.Author).ThenInclude(u => u.Person)
						.Include(p => p.PostSettings),
					posts => posts.Where(p => p.Id == id).FirstOrDefaultAsync()
				);
			});

			return post;
		}

		public async Task<IEnumerable<Post>> TryGetPostByUrlAsync(string url)
		{
			IEnumerable<Post> postsArray = null;

			await UseContextAsync(async context =>
			{
				postsArray = await GetFromItemsAsync(
					dbContext => dbContext.Posts
						.Include(p => p.PostSeoSetting)
						.Include(p => p.Author).ThenInclude(u => u.Person)
						.Include(p => p.PostSettings),
					posts => posts.Where(p => p.PostSeoSetting.Url == url).ToArrayAsync()
				);
			});

			return postsArray;
		}

		public async Task<IEnumerable<Post>> TryGetMainPagePostsWithAllDataAsync(int count, string postTypeAlias)
		{
			IEnumerable<Post> posts = null;

			await UseContextAsync(async context =>
			{
				posts = await GetFromItemsAsync(
					dbContext => dbContext.Posts
						.Include(p => p.PostSeoSetting)
						.Include(p => p.PostType)
						.Include(p => p.PostSettings),
					postsWithData => postsWithData
						.Where(p =>
							p.PostSettings.PostOnStartPage == true &&
							p.PostType.Alias == postTypeAlias &&
							p.Published &&
							(p.Deleted == false || p.Deleted == null) &&
							p.PublishDate <= DateTime.UtcNow
						)
						.OrderByDescending(p => p.PublishDate)
						.Take(count)
						.ToArrayAsync()
				);
			});

			return posts;
		}

		public async Task<IEnumerable<Post>> TryGetNews(int perPage, int page, string postTypeAlias)
		{
			IEnumerable<Post> posts = null;

			await UseContextAsync(async context =>
			{
				var toSkip = (page - 1) * perPage;

				posts = await GetFromItemsAsync(
					dbContext => dbContext.Posts
						.Include(p => p.PostSeoSetting)
						.Include(p => p.PostType)
						.Include(p => p.PostSettings),
					postsWithData => postsWithData
						.Where(p =>
							p.PostType.Alias == postTypeAlias &&
							p.Published &&
							(p.Deleted == false || p.Deleted == null) &&
							p.PublishDate <= DateTime.UtcNow
						)
						.OrderByDescending(p => p.PublishDate)
						.Skip(toSkip)
						.Take(perPage)
						.ToArrayAsync()
				);
			});

			return posts;
		}

		public async Task<Post> TryGetActivePostByUrlAndTypeAsync(string url, string postType)
		{
			Post post = null;

			await UseContextAsync(async context =>
			{
				post = await GetFromItemsAsync(
					dbContext => dbContext.Posts
						.Include(p => p.PostSeoSetting)
						.Include(p => p.Author).ThenInclude(u => u.Person)
						.Include(p => p.PostSettings),
					posts => posts.FirstOrDefaultAsync(p => 
						p.PostSeoSetting.Url == url && 
						p.PostTypeAlias == postType &&
						(p.Deleted == false || p.Deleted == null) &&
						p.Published
					)
				);
			});

			return post;
		}
	}
}