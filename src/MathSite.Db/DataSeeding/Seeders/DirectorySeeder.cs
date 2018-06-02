using System;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class DirectorySeeder : AbstractSeeder<Directory>
    {
        public DirectorySeeder(ILogger logger, MathSiteDbContext context)
            : base(logger, context)
        {
        }

        public override string SeedingObjectName { get; } = nameof(Directory);

        protected override void SeedData()
        {
            var news = CreateDirectory(Guid.NewGuid(), "news");
            var newsPreviews = CreateDirectory(Guid.NewGuid(), "previews", news);
            var staticPages = CreateDirectory(Guid.NewGuid(), "static-pages");
            var staticPagesPreviews = CreateDirectory(Guid.NewGuid(), "previews", staticPages);


            var directories = new[]
            {
                news,
                newsPreviews,
                staticPages,
                staticPagesPreviews
            };

            Context.Directories.AddRange(directories);

            Context.SaveChanges();
        }

        private static Directory CreateDirectory(Guid id, string name, Directory root = null)
        {
            return new Directory {Id = id, Name = name, RootDirectoryId = root?.Id};
        }
    }
}