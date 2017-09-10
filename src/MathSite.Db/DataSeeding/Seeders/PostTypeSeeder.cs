using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostTypeSeeder : AbstractSeeder<PostType>
	{
		/// <inheritdoc />
		public PostTypeSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostType);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostType = CreateUserSettings(
				PostTypeAliases.News,
				"Новости",
				GetPostSetting(1)
			);

			var secondPostType = CreateUserSettings(
				PostTypeAliases.StaticPage,
				"Статическая страница",
				GetPostSetting(2)
			);
			var postTypes = new[]
			{
				firstPostType,
				secondPostType
			};

			Context.PostTypes.AddRange(postTypes);
		}

		private static PostType CreateUserSettings(string alias, string name, PostSetting postSetting)
		{
			return new PostType
			{
				Alias = alias,
				TypeName = name,
				DefaultPostsSettings = postSetting,
				Posts = new List<Post>()
			};
		}

		private PostSetting GetPostSetting(int at)
		{
			return Context.PostSettings.Skip(at - 1).First();
		}
	}
}