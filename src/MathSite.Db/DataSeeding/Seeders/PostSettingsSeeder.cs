using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class PostSettingsSeeder : AbstractSeeder<PostSetting>
    {
        /// <inheritdoc />
        public PostSettingsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = $"{nameof(PostSetting)} ### Default Posts Settings";

        /// <inheritdoc />
        protected override void SeedData()
        {
            var postsSettings = new[]
            {
                CreatePostSettings(
                    null,
                    GetPreviewImageByName(FileAliases.FirstFile),
                    true,
                    true,
                    true
                ),
                CreatePostSettings(
                    null,
                    GetPreviewImageByName(FileAliases.SecondFile),
                    false,
                    false,
                    false
                )
            };

            Context.PostSettings.AddRange(postsSettings);
        }

        protected File GetPreviewImageByName(string name)
        {
            return Context.Files.First(file => file.Name == name);
        }

        protected static PostSetting CreatePostSettings(PostType postType, File previewImage,
            bool isCommentsAllowed, bool canBeRated, bool isPostOnStartPage)
        {
            return new PostSetting
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