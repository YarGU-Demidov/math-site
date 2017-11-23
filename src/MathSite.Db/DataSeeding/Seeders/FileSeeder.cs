using System;
using System.Collections.Generic;
using System.Linq;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
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
            var files = new[]
            {
                CreateFile(
                    "FirstFile",
                    DateTime.UtcNow,
                    "uploads/new-file.jpg",
                    "jpg",
                    GetDirectoryByPath("/")
                ),
                CreateFile(
                    "SecondFile",
                    DateTime.UtcNow,
                    "uploads/new-file-1.png",
                    "png",
                    GetDirectoryByPath("/news")
                ),
                CreateFile(
                    "File in path",
                    DateTime.UtcNow,
                    "uploads/new-file-2.docx",
                    "docx",
                    GetDirectoryByPath("/news/previews")
                )
            };

            Context.Files.AddRange(files);

            Context.SaveChanges();
        }

        private Directory GetDirectoryByPath(string name)
        {
            if (name == "/")
                return null;
            
            var names = new Queue<string>(name.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries));

            var tempName = names.Dequeue();
            var dir = Context.Directories.First(d => d.Name == tempName);
            
            while (names.Count > 0)
            {
                tempName = names.Dequeue();
                dir = dir.Directories.First(d => d.Name == tempName);
            }

            return dir;
        }

        private static File CreateFile(string name, DateTime dateAdded, string filePath, string extension, Directory dir)
        {
            return new File
            {
                Name = name,
                DateAdded = dateAdded,
                Path = filePath,
                Extension = extension,
                PostSettings = new List<PostSetting>(),
                PostAttachments = new List<PostAttachment>(),
                DirectoryId = dir?.Id
            };
        }
    }
}