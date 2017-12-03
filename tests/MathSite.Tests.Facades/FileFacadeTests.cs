using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding;
using MathSite.Db.DataSeeding.Seeders;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Tests.Facades.TestStuff;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class FileFacadeTests : FacadesTestsBase
    {
        private (IUsersFacade usersFacade, Lazy<IDirectoryFacade> directoryFacade, IUserValidationFacade userValidationFacade) GetRequiredFacades(
            IRepositoryManager repositoryManager
        )
        {
            var usersFacade = new UsersFacade(repositoryManager, MemoryCache);
            var validationFacade = new UserValidationFacade(repositoryManager, MemoryCache, new DoubleSha512HashPasswordsManager());
            var directoryFacade = new Lazy<IDirectoryFacade>(() => new DirectoryFacade(repositoryManager, MemoryCache));

            return (usersFacade, directoryFacade, validationFacade);
        }

        private static async Task<User> GetUserByLogin(MathSiteDbContext context, string login)
        {
            return await context.Users.FirstAsync(user => user.Login == login);
        }

        private FileFacade GetFacadeWithRead(IRepositoryManager repositoryManager, string pathId, byte[] data)
        {
            var fileStorage = new TestFileStorage(pathId, data);

            var (usersFacade, directoryFacade, userValidationFacade) = GetRequiredFacades(repositoryManager);

            return new FileFacade(repositoryManager, MemoryCache, fileStorage, usersFacade, userValidationFacade, directoryFacade);
        }

        private FileFacade GetFacadeWithWrite(IRepositoryManager repositoryManager, string pathId)
        {
            var fileStorage = new TestFileStorage(pathId);

            var (usersFacade, directoryFacade, userValidationFacade) = GetRequiredFacades(repositoryManager);

            return new FileFacade(repositoryManager, MemoryCache, fileStorage, usersFacade, userValidationFacade, directoryFacade);
        }

        [Fact]
        public async Task SaveFile_WithAdminRights_Success()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var data = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
                const string fileName = "test-filename";

                var facade = GetFacadeWithWrite(manager, fileName);

                var id = await facade.SaveFileAsync(await GetUserByLogin(context, UsersAliases.Mokeev1995), fileName,
                    new MemoryStream(data));

                Assert.NotEqual(default, id);

                var downloaded = await facade.GetFileAsync(id);
                var dataLength = (int) downloaded.FileStream.Length;
                var downloadedData = new byte[dataLength];

                await downloaded.FileStream.ReadAsync(downloadedData, 0, dataLength);

                Assert.Equal(fileName, downloaded.FileName);
                Assert.Equal(data, downloadedData);
            });
        }

        [Fact]
        public async Task SaveFile_WithoutAuthentication_Fail()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var data = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
                const string fileName = "test-filename";

                var facade = GetFacadeWithWrite(manager, fileName);

                await Assert.ThrowsAsync<AuthenticationException>(async () =>
                {
                    await facade.SaveFileAsync(
                        null,
                        fileName,
                        new MemoryStream(data)
                    );
                });
            });
        }

        [Fact]
        public async Task SaveFile_WithoutRights_Fail()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var data = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
                const string fileName = "test-filename";

                var facade = GetFacadeWithWrite(manager, fileName);

                await Assert.ThrowsAsync<AuthenticationException>(async () =>
                {
                    await facade.SaveFileAsync(
                        await GetUserByLogin(
                            context,
                            UsersAliases.TestUser
                        ),
                        fileName,
                        new MemoryStream(data)
                    );
                });
            });
        }
    }
}