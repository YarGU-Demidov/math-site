using System.Collections.Generic;
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
				"Новости"
			);

			var secondPostType = CreateUserSettings(
				PostTypeAliases.StaticPage,
				"Статическая страница"
			);
			var postTypes = new[]
			{
				firstPostType,
				secondPostType
			};

			Context.PostTypes.AddRange(postTypes);
		}

		private static PostType CreateUserSettings(string alias, string name)
		{
			return new PostType
			{
				Alias = alias,
				TypeName = name,
				Posts = new List<Post>()
			};
		}
	}
}