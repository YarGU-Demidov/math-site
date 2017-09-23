using System;
using System.Collections.Generic;
using MathSite.Entities;
using Microsoft.Extensions.Logging;

namespace MathSite.Db.DataSeeding.Seeders
{
    public class FileSeeder : AbstractSeeder<File>
    {
        /// <inheritdoc />
        public FileSeeder(ILogger logger, MathSiteDbContext context) : base(logger, context)
        {
        }

        /// <inheritdoc />
        public override string SeedingObjectName { get; } = nameof(File);

        /// <inheritdoc />
        protected override void SeedData()
        {
            var firstFile = CreateFile(
                "FirstFile",
                DateTime.Now,
                "first file path",
                "first extension"
            );

            var secondFile = CreateFile(
                "SecondFile",
                DateTime.Now,
                "second file path",
                "second extension"
            );

            var files = new[]
            {
                firstFile,
                secondFile
            };

            Context.Files.AddRange(files);
        }

        private static File CreateFile(string name, DateTime dateAdded, string filePath, string extension)
        {
            return new File
            {
                Name = name,
                DateAdded = dateAdded,
                Path = filePath,
                Extension = extension,
                PostSettings = new List<PostSetting>(),
                PostAttachments = new List<PostAttachment>()
            };
        }
    }
}