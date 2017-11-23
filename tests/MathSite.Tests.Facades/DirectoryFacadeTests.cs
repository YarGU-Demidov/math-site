using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Db.DataSeeding.Seeders;
using MathSite.Facades.FileSystem;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class DirectoryFacadeTests : FacadesTestsBase
    {
        private void SeedDirData(ILogger logger, MathSiteDbContext context, List<ISeeder> seeders = null)
        {
            var last = new ISeeder[]
            {
                new DirectorySeeder(logger, context),
                new FileSeeder(logger, context)
            };

            if (seeders == null)
            {
                SeedData(last);
                return;
            }

            seeders.AddRange(last);
        }

        [Fact]
        public async Task FolderNotFoundTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedDirData(logger, context);

                var facade = new DirectoryFacade(manager, MemoryCache);

                await Assert.ThrowsAsync<DirectoryNotFoundException>(async () =>
                {
                    await facade.GetDirectoryWithPathAsync("/news/previews/does-not-exists");
                });
            });
        }

        [Fact]
        public async Task GetDeepFilesAndDirectoriesTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedDirData(logger, context);

                var facade = new DirectoryFacade(manager, MemoryCache);

                var prevDir = await facade.GetDirectoryWithPathAsync("/news");
                var dir = await facade.GetDirectoryWithPathAsync("/news/previews");

                Assert.Equal(prevDir.Id, dir.RootDirectoryId);
                Assert.Equal(0, dir.Directories.Count);
                Assert.Equal(1, dir.Files.Count);
            });
        }

        [Fact]
        public async Task GetNotRootFilesAndDirectoriesTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedDirData(logger, context);

                var facade = new DirectoryFacade(manager, MemoryCache);

                var dir = await facade.GetDirectoryWithPathAsync("/news");

                Assert.Null(dir.RootDirectoryId);
                Assert.Equal(1, dir.Directories.Count);
                Assert.Equal(1, dir.Files.Count);
            });
        }

        [Fact]
        public async Task GetRootFilesAndDirectoriesTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedDirData(logger, context);

                var facade = new DirectoryFacade(manager, MemoryCache);

                var dir = await facade.GetDirectoryWithPathAsync("/");

                Assert.Equal(Guid.Empty, dir.Id);
                Assert.Null(dir.RootDirectoryId);
                Assert.Equal(2, dir.Directories.Count);
                Assert.Equal(1, dir.Files.Count);
            });
        }

        [Fact]
        public async Task PreviousFolderNotFoundTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                SeedDirData(logger, context);

                var facade = new DirectoryFacade(manager, MemoryCache);

                await Assert.ThrowsAsync<DirectoryNotFoundException>(async () =>
                {
                    await facade.GetDirectoryWithPathAsync(
                        "/news/previews/does-not-exists/and-this-doesnot-exists-too");
                });
            });
        }
    }
}