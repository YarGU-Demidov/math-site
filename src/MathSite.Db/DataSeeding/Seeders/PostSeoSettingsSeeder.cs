using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSeoSettingsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostSeoSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostSeoSettings";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostSeoSettings.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostSeoSettings = CreatePostSeoSettings(
				"first url",
				"first title",
				"first description",
				GetPostByPostTypeAlias(PostTypeAliases.News)
			);

			var secondPostSeoSettings = CreatePostSeoSettings(
				"second url",
				"second title",
				"second description",
				GetPostByPostTypeAlias(PostAliases.SecondPost)
			);

			var postSeoSettings = new[]
			{
				firstPostSeoSettings,
				secondPostSeoSettings
			};

			Context.PostSeoSettings.AddRange(postSeoSettings);
		}

		private Post GetPostByPostTypeAlias(string alias)
		{
			return Context.Posts.First(post => post.PostType.TypeName == alias);
		}

		private static PostSeoSettings CreatePostSeoSettings(string url, string title, string description, Post post)
		{
			return new PostSeoSettings
			{
				Url = url,
				Title = title,
				Description = description,
				Post = post,
				PostKeywords = new List<PostKeywords>()
			};
		}
	}
}