using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class PostSeeder : AbstractSeeder<Post>
    {
        /// <inheritdoc />
        public PostSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(Post);

        /// <inheritdoc />
        protected override void SeedData()
        {
            var newsPosts = CreateNewsPosts();

            var staticPages = CreateStaticPages();
            
            var events = CreateEvents();

            var posts = new List<Post>();

            posts.AddRange(newsPosts);
            posts.AddRange(staticPages);
            posts.AddRange(events);

            Context.Posts.AddRange(posts);
        }

        private IEnumerable<Post> CreateEvents()
        {
            return new[]
            {
                CreatePost(
                    title: "День открытых дверей",
                    excerpt: "День открытых дверей в ЯрГУ им. П.Г.Демидова",
                    content: "Мы приглашаем всех школьников на День открытых дверей в ЯрГУ. Больше инфы будет тут потом. Может быть.",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.Event),
                    postSetting: CreateSetting(true, true, true, default, DateTime.UtcNow.AddDays(20), "Ярославль, ул. Советская, д.14, ауд. 201"),
                    postSeoSetting: GetPostSeoSettingsByUrl("open-doors-2017")
                )
            };
        }

        private IEnumerable<Post> CreateStaticPages()
        {
            return new[]
            {
                CreatePost(
                    title: "New post",
                    excerpt: "New post about university",
                    content: "We are studying in the best university",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.StaticPage),
                    postSetting: null,
                    postSeoSetting: GetPostSeoSettingsByUrl("static-page-url")
                )
            };
        }

        private IEnumerable<Post> CreateNewsPosts()
        {
            return new[]
            {
                CreatePost(
                    title: "First post",
                    excerpt: "First post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("first-url")
                ),
                CreatePost(
                    title: "Second post",
                    excerpt: "Second post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("second-url")
                ),
                CreatePost(
                    title: "Third post",
                    excerpt: "Third post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("third-url")
                ),
                CreatePost(
                    title: "Fourth post",
                    excerpt: "Fourth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("fourth-url")
                ),
                CreatePost(
                    title: "Fifth post",
                    excerpt: "Fifth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("fifth-url")
                ),
                CreatePost(
                    title: "Sixth post",
                    excerpt: "Sixth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("sixth-url")
                ),
                CreatePost(
                    title: "Seventh post",
                    excerpt: "Seventh post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("seventh-url")
                ),
                CreatePost(
                    title: "Eighth post",
                    excerpt: "Eighth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("eighth-url")
                ),
                CreatePost(
                    title: "Ninth post",
                    excerpt: "Ninth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(),
                    postSeoSetting: GetPostSeoSettingsByUrl("ninth-url")
                ),
                CreatePost(
                    title: "Tenth post",
                    excerpt: "Tenth post about university",
                    content: "We are studying at Yaroslavl Demidov State University",
                    publishDate: DateTime.UtcNow,
                    author: GetUserByLogin(UsersAliases.Mokeev1995),
                    isPublished: true,
                    isDeleted: false,
                    postType: GetPostTypeByAlias(PostTypeAliases.News),
                    postSetting: CreateSetting(true),
                    postSeoSetting: GetPostSeoSettingsByUrl("tenth-url")
                )
            };
        }

        private PostType GetPostTypeByAlias(string alias)
        {
            return Context.PostTypes.First(postType => postType.Alias == alias);
        }

        private User GetUserByLogin(string login)
        {
            return Context.Users.First(user => user.Login == login);
        }

        private PostSeoSetting GetPostSeoSettingsByUrl(string url)
        {
            return Context.PostSeoSettings.First(postSeoSettings => postSeoSettings.Url == url);
        }

        private static Post CreatePost(string title, string excerpt, string content, DateTime publishDate, User author,
            bool isPublished, bool isDeleted, PostType postType, PostSetting postSetting, PostSeoSetting postSeoSetting)
        {
            return new Post
            {
                Title = title,
                Excerpt = excerpt,
                Content = content,
                PublishDate = publishDate,
                Author = author,
                Published = isPublished,
                Deleted = isDeleted,
                PostType = postType,
                PostSettings = postSetting,
                PostSeoSetting = postSeoSetting,
                PostCategories = new List<PostCategory>(),
                PostOwners = new List<PostOwner>(),
                UsersAllowed = new List<PostUserAllowed>(),
                PostRatings = new List<PostRating>(),
                Comments = new List<Comment>(),
                PostAttachments = new List<PostAttachment>(),
                GroupsAllowed = new List<PostGroupsAllowed>()
            };
        }

        public static PostSetting CreateSetting(
            bool onMainPage = false, 
            bool canBeRated = false,
            bool commentsAllowed = true,
            File previewImage = null,
            DateTime? eventTime = null,
            string eventLocation= null
        )
        {
            return new PostSetting
            {
                PostOnStartPage = onMainPage,
                CanBeRated = canBeRated,
                IsCommentsAllowed = commentsAllowed,
                PreviewImage = previewImage,
                EventTime = eventTime,
                EventLocation = eventLocation
            };
        }
    }
}