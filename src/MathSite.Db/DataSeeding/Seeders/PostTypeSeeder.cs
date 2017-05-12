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
				PostTypeAliases.News
			);
			var secondPostType = CreateUserSettings(
				PostTypeAliases.StaticPage
			);
			var postTypes = new[]
			{
				firstPostType,
				secondPostType
			};

			Context.PostTypes.AddRange(postTypes);
		}

		private static PostType CreateUserSettings(string name)
		{
			return new PostType
			{
				TypeName = name,
				Posts = new List<Post>()
			};
		}
	}
}