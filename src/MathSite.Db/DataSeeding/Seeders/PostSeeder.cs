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

			var posts = new List<Post>(newsPosts);

			posts.AddRange(staticPages);

			Context.Posts.AddRange(posts);
		}

		private IEnumerable<Post> CreateStaticPages()
		{
			return new[]
			{
				CreatePost(
					"New post",
					"New post about university",
					"We are studying in the best university",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.StaticPage),
					null,
					GetPostSeoSettingsByType("static-page-url")
				)
			};
		}

		private IEnumerable<Post> CreateNewsPosts()
		{
			return new[]
			{
				CreatePost(
					"First post",
					"First post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("first-url")
				),
				CreatePost(
					"Second post",
					"Second post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("second-url")
				),
				CreatePost(
					"Third post",
					"Third post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("third-url")
				),
				CreatePost(
					"Fourth post",
					"Fourth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("fourth-url")
				),
				CreatePost(
					"Fifth post",
					"Fifth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("fifth-url")
				),
				CreatePost(
					"Sixth post",
					"Sixth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("sixth-url")
				),
				CreatePost(
					"Seventh post",
					"Seventh post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("seventh-url")
				),
				CreatePost(
					"Eighth post",
					"Eighth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("eighth-url")
				),
				CreatePost(
					"Ninth post",
					"Ninth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(),
					GetPostSeoSettingsByType("ninth-url")
				),
				CreatePost(
					"Tenth post",
					"Tenth post about university",
					"We are studying at Yaroslavl Demidov State University",
					DateTime.UtcNow,
					GetUserByLogin(UsersAliases.FirstUser),
					true,
					false,
					GetPostTypeByAlias(PostTypeAliases.News),
					CreateSetting(true),
					GetPostSeoSettingsByType("tenth-url")
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

		private PostSeoSetting GetPostSeoSettingsByType(string url)
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

		public static PostSetting CreateSetting(bool onMainPage = false, bool canBeRated = false,
			bool commentsAllowed = true,
			File previewImage = null)
		{
			return new PostSetting
			{
				PostOnStartPage = onMainPage,
				CanBeRated = canBeRated,
				IsCommentsAllowed = commentsAllowed,
				PreviewImage = previewImage
			};
		}
	}
}