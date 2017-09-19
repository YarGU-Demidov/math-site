using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class PostKeywordsSeeder : AbstractSeeder<PostKeyword>
    {
        /// <inheritdoc />
        public PostKeywordsSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(PostKeyword);

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

        private Keyword GetKeywordByName(string name)
        {
            return Context.Keywords.First(keyword => keyword.Name == name);
        }

        private PostSeoSetting GetPostSeoSettingsByTitle(string title)
        {
            return Context.PostSeoSettings.First(postSeoSettings => postSeoSettings.Title == title);
        }

        private static PostKeyword CreatePostKeywords(Keyword keyword, PostSeoSetting postSeoSettings)
        {
            return new PostKeyword
            {
                Keyword = keyword,
                PostSeoSettings = postSeoSettings
            };
        }
    }
}