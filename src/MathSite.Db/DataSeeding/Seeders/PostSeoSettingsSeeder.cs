using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSeoSettingsSeeder : AbstractSeeder<PostSeoSettings>
	{
		/// <inheritdoc />
		public PostSeoSettingsSeeder(ILogger logger, IMathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostSeoSettings);

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostSeoSettings = CreatePostSeoSettings(
				"first url",
				"first title",
				"first description"
			);

			var secondPostSeoSettings = CreatePostSeoSettings(
				"second url",
				"second title",
				"second description"
			);

			var postSeoSettings = new[]
			{
				firstPostSeoSettings,
				secondPostSeoSettings
			};

			Context.PostSeoSettings.AddRange(postSeoSettings);
		}

		private static PostSeoSettings CreatePostSeoSettings(string url, string title, string description)
		{
			return new PostSeoSettings
			{
				Url = url,
				Title = title,
				Description = description,
				PostKeywords = new List<PostKeywords>()
			};
		}
	}
}