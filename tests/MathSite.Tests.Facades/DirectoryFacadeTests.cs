using System;
using System.IO;
using System.Threading.Tasks;
using MathSite.Facades.FileSystem;
using MathSite.Repository.Core;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class DirectoryFacadeTests : FacadesTestsBase
    {

        private DirectoryFacade GetFacade(IRepositoryManager manager)
        {
            return new DirectoryFacade(manager, MemoryCache);
        }

        [Fact]
        public async Task FolderNotFoundTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var facade = GetFacade(manager);

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
                var facade = GetFacade(manager);

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
                var facade = GetFacade(manager);

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
                var facade = GetFacade(manager);

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
                var facade = GetFacade(manager);

                await Assert.ThrowsAsync<DirectoryNotFoundException>(async () =>
                {
                    await facade.GetDirectoryWithPathAsync(
                        "/news/previews/does-not-exists/and-this-doesnot-exists-too");
                });
            });
        }

        [Fact]
        public async Task SendEmptyPathTest()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var facade = GetFacade(manager);

                await Assert.ThrowsAsync<ArgumentException>(async () => await facade.GetDirectoryWithPathAsync(null));
                await Assert.ThrowsAsync<ArgumentException>(async () => await facade.GetDirectoryWithPathAsync(""));
            });
        }
    }
}