using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Models;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
	public class PostSettingsSeeder : AbstractSeeder
	{
		/// <inheritdoc />
		public PostSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
		{
		}

		/// <inheritdoc />
		public override string SeedingObjectName { get; } = "PostSettings";

		/// <inheritdoc />
		protected override bool DbContainsEntities()
		{
			return Context.PostSettings.Any();
		}

		/// <inheritdoc />
		protected override void SeedData()
		{
			var firstPostSettings = CreatePostSettings(
				GetPostTypeByName(PostTypeAliases.News),
				GetPreviewImageByName(FileAliases.FirstFile),
				true,
				true,
				true
			);

			var secondPostSettings = CreatePostSettings(
				GetPostTypeByName(PostTypeAliases.StaticPage),
				GetPreviewImageByName(FileAliases.SecondFile),
				false,
				false,
				false
			);

			var postsSettings = new[]
			{
				firstPostSettings,
				secondPostSettings
			};

			Context.PostSettings.AddRange(postsSettings);
		}

		private PostType GetPostTypeByName(string name)
		{
			return Context.PostTypes.First(postType => postType.TypeName == name);
		}

		private File GetPreviewImageByName(string name)
		{
			return Context.Files.First(file => file.FileName == name);
		}

		private static PostSettings CreatePostSettings(PostType postType, File previewImage,
			bool isCommentsAllowed, bool canBeRated, bool isPostOnStartPage)
		{
			return new PostSettings
			{
				IsCommentsAllowed = isCommentsAllowed,
				CanBeRated = canBeRated,
				PostOnStartPage = isPostOnStartPage,
				PostType = postType,
				PreviewImage = previewImage
			};
		}
	}
}