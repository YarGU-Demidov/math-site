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
            var newsPostType = CreateUserSettings(
                PostTypeAliases.News,
                "Новости",
                GetPostSetting(1)
            );

            var staticPagePostType = CreateUserSettings(
                PostTypeAliases.StaticPage,
                "Статическая страница",
                GetPostSetting(2)
            );

            var eventPostType = CreateUserSettings(
                PostTypeAliases.Event,
                "События",
                GetPostSetting(3)
            );

            var postTypes = new[]
            {
                newsPostType,
                staticPagePostType,
                eventPostType
            };

            Context.PostTypes.AddRange(postTypes);
        }

        private static PostType CreateUserSettings(string alias, string name, PostSetting postSetting)
        {
            return new PostType
            {
                Alias = alias,
                Name = name,
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