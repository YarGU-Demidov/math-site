using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostTypeSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostTypeSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostType";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostTypes.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostType = CreateUserSettings(
				PostTypeAliases.News,
				GetPostSettingsByPost("First post")
			);
			var secondPostType = CreateUserSettings(
				PostTypeAliases.StaticPage,
				GetPostSettingsByPost("Second post")
			);
			var postTypes = new[]
			{
				firstPostType,
				secondPostType
			};

			Context.PostTypes.AddRange(postTypes);
		}

		private PostSettings GetPostSettingsByPost(string title)
		{
			return Context.PostSettings.FirstOrDefault(p => p.Post.Title == title);

		}

		private static PostType CreateUserSettings(string name, PostSettings defaultPostsSettings)
		{
			return new PostType
			{
				TypeName = name,
				DefaultPostsSettings = defaultPostsSettings,
				Posts = new List<Post>()
			};
		}
	}
}