using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class KeywordSeeder : AbstractSeeder<Keyword>
    {
        /// <inheritdoc />
        public KeywordSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(Keyword);

        /// <inheritdoc />
        protected override void SeedData()
        {
            var firstKeyword = CreateKeyword("Student", "StudentAlias");
            var secondKeyword = CreateKeyword("Employee", "EmployeeAlias");

            var keywords = new[]
            {
                firstKeyword,
                secondKeyword
            };

            Context.Keywords.AddRange(keywords);
        }

        private static Keyword CreateKeyword(string name, string alias)
        {
            return new Keyword
            {
                Name = name,
                Alias = alias,
                Posts = new List<PostKeyword>()
            };
        }
    }
}