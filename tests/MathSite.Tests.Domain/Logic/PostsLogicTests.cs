using System;
using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Domain.Logic.Posts;
using MathSite.Domain.Logic.PostSeoSettings;
using MathSite.Domain.Logic.PostSettings;
using MathSite.Domain.Logic.Users;
using Xunit;

namespace MathSite.Tests.Domain.Logic
{
	public class PostsLogicTests : DomainTestsBase
	{
		[Fact]
		public async Task TryGet_ById_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postsLogic = new PostsLogic(context);
				var usersLogic = new UsersLogic(context);
				var seoSettingsLogic = new PostSeoSettingsLogic(context);

				var id = await CreatePostAsync(postsLogic, usersLogic, seoSettingsLogic);

				var post = await postsLogic.TryGetByIdAsync(id);

				Assert.NotNull(post);
			});
		}

		[Fact]
		public async Task TryGet_ById_NotFound()
		{
			var id = Guid.NewGuid();

			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PostsLogic(context);
				var post = await personsLogic.TryGetByIdAsync(id);

				Assert.Null(post);
			});
		}

		[Fact]
		public async Task TryGet_ByUrl_Found()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postsLogic = new PostsLogic(context);
				var usersLogic = new UsersLogic(context);
				var seoSettingsLogic = new PostSeoSettingsLogic(context);

				var url = "test-find-post-by-url";

				var seoId = await seoSettingsLogic.CreateSeoSettingsAsync(url, "test-title", "test-desc");

				await CreatePostAsync(postsLogic, usersLogic, seoSettingsLogic, seoSettings: seoId);

				var file = await postsLogic.TryGetByUrlAsync(url);

				Assert.NotNull(file);
			});
		}

		[Fact]
		public async Task TryGet_ByUrl_NotFound()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var personsLogic = new PostsLogic(context);
				var post = await personsLogic.TryGetByUrlAsync(" wrong url. it doesn't exists :) ");

				Assert.Null(post);
			});
		}

		[Fact]
		public async Task TryGetMainPagePostsWithAllDataTest()
		{
			await ExecuteWithContextAsync(async context =>
			{

			});
		}

		[Fact]
		public async Task CreatePostTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postsLogic = new PostsLogic(context);
				var usersLogic = new UsersLogic(context);
				var seoSettingsLogic = new PostSeoSettingsLogic(context);
				var postSettingsLogic = new PostSettingLogic(context);

				var title = "test-post-title-new";
				var excerpt = "test-post-excerpt-new";
				var content = "test-post-content-new";
				var published = true;
				var publishDate = DateTime.Today.AddDays(-10);
				var postTypeAlias = PostTypeAliases.StaticPage;
				var author = await usersLogic.TryGetByLoginAsync(UsersAliases.SecondUser);
				var settingsId = await postSettingsLogic.CreatePostSettings(true, false, true, null);
				var seoSettingsId = await seoSettingsLogic.CreateSeoSettingsAsync("test-post-url-new", "test-post-seo-title-new", "test-post-seo-description-new");

				var postId = await CreatePostAsync(
					postsLogic, usersLogic, seoSettingsLogic, 
					title, excerpt, content, published, publishDate, postTypeAlias, author.Id, settingsId, seoSettingsId
				);

				Assert.NotEqual(Guid.Empty, postId);

				var post = await postsLogic.TryGetByIdAsync(postId);

				Assert.NotNull(post);
				Assert.Equal(title, post.Title);
				Assert.Equal(excerpt, post.Excerpt);
				Assert.Equal(content, post.Content);
				Assert.Equal(published, post.Published);
				Assert.Equal(publishDate, post.PublishDate);
				Assert.Equal(postTypeAlias, post.PostTypeAlias);
				Assert.Equal(author.Id, post.AuthorId);
				Assert.Equal(settingsId, post.PostSettingsId);
				Assert.Equal(seoSettingsId, post.PostSeoSettingsId);
			});
		}

		[Fact]
		public async Task UpdatePostTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postsLogic = new PostsLogic(context);
				var usersLogic = new UsersLogic(context);
				var seoSettingsLogic = new PostSeoSettingsLogic(context);

				var title = "test-post-title-new";
				var excerpt = "test-post-excerpt-new";
				var content = "test-post-content-new";
				var published = true;
				var publishDate = DateTime.Today.AddDays(-10);
				var postTypeAlias = PostTypeAliases.StaticPage;
				var author = await usersLogic.TryGetByLoginAsync(UsersAliases.SecondUser);

				var postId = await CreatePostAsync(
					postsLogic, usersLogic, seoSettingsLogic
				);

				await postsLogic.UpdatePostAsync(postId, title, excerpt, content, published, publishDate, postTypeAlias, author.Id);

				var post = await postsLogic.TryGetByIdAsync(postId);

				Assert.NotNull(post);
				Assert.Equal(title, post.Title);
				Assert.Equal(excerpt, post.Excerpt);
				Assert.Equal(content, post.Content);
				Assert.Equal(published, post.Published);
				Assert.Equal(publishDate, post.PublishDate);
				Assert.Equal(postTypeAlias, post.PostTypeAlias);
				Assert.Equal(author.Id, post.AuthorId);
			});
		}

		[Fact]
		public async Task DeletePostTest()
		{
			await ExecuteWithContextAsync(async context =>
			{
				var postsLogic = new PostsLogic(context);
				var usersLogic = new UsersLogic(context);
				var seoSettingsLogic = new PostSeoSettingsLogic(context);

				var id = await CreatePostAsync(postsLogic, usersLogic, seoSettingsLogic);

				await postsLogic.DeletePostAsync(id);

				var person = await postsLogic.TryGetByIdAsync(id);

				Assert.Null(person);
			});
		}

		private async Task<Guid> CreatePostAsync(
			IPostsLogic logic,
			IUsersLogic usersLogic,
			IPostSeoSettingsLogic seoSettingsLogic,
			string title = null,
			string excerpt = null,
			string content = null,
			bool published = true, 
			DateTime? publishDate = null,
			string postTypeAlias = null,
			Guid? authorId = null,
			Guid? settings = null,
			Guid? seoSettings = null
		)
		{
			var salt = Guid.NewGuid();

			title = title ?? $"test-post-title-{salt}";
			excerpt = excerpt ?? $"test-post-excerpt-{salt}";
			content = content ?? $"test-post-content-{salt}";
			publishDate = publishDate ?? DateTime.Today.AddDays(-10);
			postTypeAlias = postTypeAlias ?? PostTypeAliases.News;
			authorId = authorId ?? (await usersLogic.TryGetByLoginAsync(UsersAliases.FirstUser)).Id;
			seoSettings = seoSettings ?? await seoSettingsLogic.CreateSeoSettingsAsync(
				$"test-post-url-{salt}",
				$"test-post-seo-title-{salt}", 
				$"test-post-sep-description-{salt}"
			);

			return await logic.CreatePostAsync(title, excerpt, content, published, publishDate.Value, postTypeAlias, authorId.Value, settings, seoSettings.Value);
		}
	}
}