using System;
using System.Linq;
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
            seoSettings = seoSettings ?? await seoSettingsLogic.CreateAsync(
                              $"test-post-url-{salt}",
                              $"test-post-seo-title-{salt}",
                              $"test-post-sep-description-{salt}"
                          );

            return await logic.CreateAsync(title, excerpt, content, published, publishDate.Value, postTypeAlias,
                authorId.Value, settings, seoSettings.Value);
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
                var settingsId = await postSettingsLogic.CreateAsync(true, false, true, null);
                var seoSettingsId = await seoSettingsLogic.CreateAsync("test-post-url-new", "test-post-seo-title-new",
                    "test-post-seo-description-new");

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
        public async Task DeletePostTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var postsLogic = new PostsLogic(context);
                var usersLogic = new UsersLogic(context);
                var seoSettingsLogic = new PostSeoSettingsLogic(context);

                var id = await CreatePostAsync(postsLogic, usersLogic, seoSettingsLogic);

                await postsLogic.DeleteAsync(id);

                var person = await postsLogic.TryGetByIdAsync(id);

                Assert.Null(person);
            });
        }

        [Fact]
        public async Task GetPostsCountTest()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var postsLogic = new PostsLogic(context);

                var newsCount = await postsLogic.GetPostsCountAsync(PostTypeAliases.News);
                var pagesCount = await postsLogic.GetPostsCountAsync(PostTypeAliases.StaticPage);

                Assert.Equal(10, newsCount);
                Assert.Equal(1, pagesCount);
            });
        }

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
                var postsLogic = new PostsLogic(context);
                var post = await postsLogic.TryGetByIdAsync(id);

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

                var seoId = await seoSettingsLogic.CreateAsync(url, "test-title", "test-desc");

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
                var postsLogic = new PostsLogic(context);
                var post = await postsLogic.TryGetByUrlAsync(" wrong url. it doesn't exists :) ");

                Assert.Null(post);
            });
        }

        [Fact]
        public async Task TryGetActivePostByUrlAndType_Found_Test()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var postsLogic = new PostsLogic(context);

                var post = await postsLogic.TryGetActivePostByUrlAndTypeAsync("tenth-url", PostTypeAliases.News);

                Assert.Equal("tenth-url", post.PostSeoSetting.Url);
            });
        }

        [Fact]
        public async Task TryGetActivePostByUrlAndType_NotFound_Test()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var postsLogic = new PostsLogic(context);

                var post = await postsLogic.TryGetActivePostByUrlAndTypeAsync("teeeeeest", PostTypeAliases.News);

                Assert.Null(post);
            });
        }

        [Fact]
        public async Task TryGetMainPagePostsWithAllData_Found_Test()
        {
            await ExecuteWithContextAsync(async context =>
            {
                var postsLogic = new PostsLogic(context);

                var posts = (await postsLogic.TryGetMainPagePostsWithAllDataAsync(3, PostTypeAliases.News)).ToArray();

                Assert.Equal(3, posts.Length);

                var first = posts.First();

                Assert.Equal("tenth-url", first.PostSeoSetting.Url);

                var second = posts.Skip(1).First();

                Assert.Equal("eighth-url", second.PostSeoSetting.Url);

                var third = posts.Skip(2).First();

                Assert.Equal("seventh-url", third.PostSeoSetting.Url);
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

                await postsLogic.UpdateAsync(postId, title, excerpt, content, published, publishDate, postTypeAlias,
                    author.Id);

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
    }
}