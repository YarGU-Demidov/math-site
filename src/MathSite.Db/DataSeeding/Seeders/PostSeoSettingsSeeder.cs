using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSeoSettingsSeeder : AbstractSeeder<PostSeoSetting>
	{
		/// <inheritdoc />
		public PostSeoSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = nameof(PostSeoSetting);

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

		private static PostSeoSetting CreatePostSeoSettings(string url, string title, string description)
		{
			return new PostSeoSetting
			{
				Url = url,
				Title = title,
				Description = description,
				PostKeywords = new List<PostKeyword>()
			};
		}
	}
}