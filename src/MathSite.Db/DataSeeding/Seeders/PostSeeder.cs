using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "Post";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.Posts.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPost = CreatePost(
				"First post",
				"First post about university",
				"We are studying at Yaroslavl Demidov State University",
				DateTime.Now,
				true,
				false,
				GetPostTypeByName(PostTypeAliases.News),
				GetPostSettingsByPostType(PostTypeAliases.News),
				GetPostSeoSettingsByTitle(PostSeoSettingsAliases.FirstPostSeoSettings)
			);

			var secondPost = CreatePost(
				"Second post",
				"Second post about university",
				"We are studying in the best university",
				DateTime.Now,
				false,
				true,
				GetPostTypeByName(PostTypeAliases.StaticPage),
				GetPostSettingsByPostType(PostTypeAliases.StaticPage),
				GetPostSeoSettingsByTitle(PostSeoSettingsAliases.SecondPostSeoSettings)
			);

			var posts = new[]
			{
				firstPost,
				secondPost
			};

			Context.Posts.AddRange(posts);
		}

		private PostType GetPostTypeByName(string name)
		{
			return Context.PostTypes.First(postType => postType.TypeName == name);
		}

		private PostSettings GetPostSettingsByPostType(string name)
		{
			return Context.PostSettings.First(postSettings => postSettings.PostType.TypeName == name);
		}

		private PostSeoSettings GetPostSeoSettingsByTitle(string title)
		{
			return Context.PostSeoSettings.First(postSeoSettings => postSeoSettings.Title == title);
		}

		private static Post CreatePost(string title, string excerpt, string content, DateTime publishDate,
			bool isPublished, bool isDeleted, PostType postType, PostSettings postSetting, PostSeoSettings postSeoSetting)
		{
			return new Post
			{
				Title = title,
				Excerpt = excerpt,
				Content = content,
				PublishDate = publishDate,
				Published = isPublished,
				Deleted = isDeleted,
				PostType = postType,
				PostSettings = postSetting,
				PostSeoSettings = postSeoSetting,
				PostCategories = new List<PostCategory>(),
				PostOwners = new List<PostOwner>(),
				UsersAllowed = new List<PostUserAllowed>(),
				PostRatings = new List<PostRating>(),
				Comments = new List<Comment>(),
				PostAttachments = new List<PostAttachment>(),
				GroupsAllowed = new List<PostGroupsAllowed>()
			};
		}
	}
}