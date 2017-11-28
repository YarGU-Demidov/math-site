using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            var files = new[]
            {
                CreateFile(
                    "FirstFile",
                    DateTime.UtcNow,
                    "new-file.jpg",
                    "jpg",
                    GetFileHash(new byte[] {1, 2, 3, 4, 5, 6}),
                    GetDirectoryByPath("/")
                ),
                CreateFile(
                    "SecondFile",
                    DateTime.UtcNow,
                    "new-file-1.png",
                    "png",
                    GetFileHash(new byte[] {7, 8, 9, 10, 11, 12, 13, 15}),
                    GetDirectoryByPath("/news")
                ),
                CreateFile(
                    "File in path",
                    DateTime.UtcNow,
                    "new-file-2.docx",
                    "docx",
                    GetFileHash(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}),
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

        private static string GetFileHash(byte[] data)
        {
            byte[] hash;
            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(data);
            }

            return hash.Select(b => b.ToString("X2")).Aggregate((f, s) => $"{f}{s}");
        }

        private static File CreateFile(string name, DateTime dateAdded, string filePath, string extension, string hash, Directory dir)
        {
            return new File
            {
                Name = name,
                DateAdded = dateAdded,
                Path = filePath,
                Extension = extension,
                PostSettings = new List<PostSetting>(),
                PostAttachments = new List<PostAttachment>(),
                DirectoryId = dir?.Id,
                Hash = hash
            };
        }
    }
}