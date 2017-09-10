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
			var postSeoSettings = new[]
			{
				CreatePostSeoSettings(
					"first-url",
					"first title",
					"first description"
				),
				CreatePostSeoSettings(
					"second-url",
					"second title",
					"second description"
				),
				CreatePostSeoSettings(
					"third-url",
					"third title",
					"third description"
				),
				CreatePostSeoSettings(
					"fourth-url",
					"fourth title",
					"fourth description"
				),
				CreatePostSeoSettings(
					"fifth-url",
					"fifth title",
					"fifth description"
				),
				CreatePostSeoSettings(
					"sixth-url",
					"sixth title",
					"sixth description"
				),
				CreatePostSeoSettings(
					"seventh-url",
					"seventh title",
					"seventh description"
				),
				CreatePostSeoSettings(
					"eighth-url",
					"eighth title",
					"eighth description"
				),
				CreatePostSeoSettings(
					"ninth-url",
					"ninth title",
					"ninth description"
				),
				CreatePostSeoSettings(
					"tenth-url",
					"tenth title",
					"tenth description"
				),
				CreatePostSeoSettings(
					"static-page-url",
					"static page title",
					"static page description"
				),
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