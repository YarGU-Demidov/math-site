using System;
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

		public async Task<Guid> CreatePostAsync(string title, string excerpt, string content, bool published, DateTime publishDate, Guid postType,
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
					PostTypeId = postType,
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

		public async Task UpdatePostAsync(Guid id, string title, string excerpt, string content, bool published, DateTime publishDate,
			Guid postType, Guid author)
		{
			await UseContextWithSaveAsync(async context =>
			{
				var post = await GetFromItemsAsync(posts => posts.FirstAsync(p => p.Id == id));

				post.Title = title;
				post.Excerpt = excerpt;
				post.Content = content;
				post.Published = published;
				post.PublishDate = publishDate;
				post.PostTypeId = postType;
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
					dbContext => dbContext.Posts.Include(p => p.PostSeoSetting).Include(p => p.Author).ThenInclude(u => u.Person).Include(p => p.GroupsAllowed).Include(p => p.PostSettings), 
					posts => posts.Where(p => p.Id == id).FirstOrDefaultAsync()
				);
			});

			return post;
		}

		public async Task<Post> TryGetPostByUrlAsync(string url)
		{
			Post post = null;
			
			await UseContextAsync(async context =>
			{
				post = await GetFromItemsAsync(
					dbContext => dbContext.Posts.Include(p => p.PostSeoSetting).Include(p => p.Author).ThenInclude(u => u.Person).Include(p => p.GroupsAllowed).Include(p => p.PostSettings),
					posts => posts.Where(p => p.PostSeoSetting.Url == url).FirstOrDefaultAsync()
				);
			});

			return post;
		}
	}
}