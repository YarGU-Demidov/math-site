using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostKeywordsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostKeywordsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostKeywords";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostKeywords.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstKeyword = CreatePostKeywords(
				GetKeywordByName(KeywordsAliases.FirstKeyword),
				GetPostSeoSettingsByTitle("first title"));

			var secondKeyword = CreatePostKeywords(
				GetKeywordByName(KeywordsAliases.SecondKeyword),
				GetPostSeoSettingsByTitle("second title"));

			var keywords = new[]
			{
				firstKeyword,
				secondKeyword
			};

			Context.PostKeywords.AddRange(keywords);
		}

		private Keywords GetKeywordByName(string name)
		{
			return Context.Keywords.First(keyword => keyword.Name == name);
		}

		private PostSeoSettings GetPostSeoSettingsByTitle(string title)
		{
			return Context.PostSeoSettings.First(postSeoSettings => postSeoSettings.Title == title);
		}

		private static PostKeywords CreatePostKeywords(Keywords keyword, PostSeoSettings postSeoSettings)
		{
			return new PostKeywords
			{
				Keyword = keyword,
				PostSeoSettings = postSeoSettings
			};
		}
	}
}