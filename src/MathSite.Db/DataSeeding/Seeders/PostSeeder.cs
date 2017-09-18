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

			var secondPost = CreatePost(
			  "New post",
			  "New post about university",
			  "We are studying in the best university",
			  DateTime.UtcNow,
			  GetUserByLogin(UsersAliases.FirstUser),
			  false,
			  true,
			  GetPostTypeByAlias(PostTypeAliases.StaticPage),
			  null,
			  GetPostSeoSettingsByType("static-page-url")
			);

			var posts = new List<Post>(newsPosts)
	  {
		secondPost
	  };

			Context.Posts.AddRange(posts);
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
		),
		CreatePost(
		  "For entrants",
		  "For entrants post",
		  "For entrants content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("for-entrants")
		),
		CreatePost(
		  "For students",
		  "For students post",
		  "For students content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("for-students")
		),
		CreatePost(
		  "For scholars",
		  "For scholars post",
		  "For scholars content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("for-scholars")
		),
		CreatePost(
		  "Contacts",
		  "Contacts post",
		  "Contacts content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("contacts")
		),
		CreatePost(
		  "Departments",
		  "Departments post",
		  "Departments content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments")
		),
		CreatePost(
		  "General-math",
		  "General-math post",
		  "General-math content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/general-math")
		),
		CreatePost(
		  "Calculus",
		  "Calculus post",
		  "Calculus content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/calculus")
		),
		CreatePost(
		  "Computer-security",
		  "Computer-security post",
		  "Computer-security content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/computer-security")
		),
		CreatePost(
		  "Algebra",
		  "Algebra post",
		  "Algebra content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/algebra")
		),
		CreatePost(
		  "Mathmod",
		  "Mathmod post",
		  "Mathmod content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/mathmod")
		),
		CreatePost(
		  "Differential equations",
		  "Differential equations post",
		  "Differential equations content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("departments/differential-equations")
		),
		CreatePost(
		  "How to enter",
		  "How to enter post",
		  "How to enter content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("how-to-enter")
		),
		CreatePost(
		  "How to learn",
		  "How to learn post",
		  "How to learn content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("how-to-learn")
		),
		CreatePost(
		  "Where to work",
		  "Where to work post",
		  "Where to work content",
		  DateTime.UtcNow,
		  GetUserByLogin(UsersAliases.SecondUser),
		  true,
		  false,
		  GetPostTypeByAlias(PostTypeAliases.StaticPage),
		  CreateSetting(true),
		  GetPostSeoSettingsByType("where-to-work")
		),
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