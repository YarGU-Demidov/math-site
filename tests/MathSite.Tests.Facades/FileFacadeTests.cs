using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Common.Exceptions;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.FileSystem;
using MathSite.Facades.Persons;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository.Core;
using MathSite.Tests.CoreThings;
using MathSite.Tests.Facades.TestStuff;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MathSite.Tests.Facades
{
    public class FileFacadeTests : FacadesTestsBase
    {
        private (IUsersFacade usersFacade, Lazy<IDirectoryFacade> directoryFacade, IUserValidationFacade userValidationFacade) GetRequiredFacades(
            IRepositoryManager repositoryManager
        )
        {
            var passwordsManager = new DoubleSha512HashPasswordsManager();
            var testKeyManager = new TestKeyManager();
            var validationFacade = new UserValidationFacade(repositoryManager, MemoryCache, passwordsManager,testKeyManager);
            var usersFacade = new UsersFacade(repositoryManager, MemoryCache, validationFacade, passwordsManager);
            var directoryFacade = new Lazy<IDirectoryFacade>(() => new DirectoryFacade(repositoryManager, MemoryCache));

            return (usersFacade, directoryFacade, validationFacade);
        }

        private static async Task<User> GetUserByLoginAsync(MathSiteDbContext context, string login)
        {
            return await context.Users.FirstAsync(user => user.Login == login);
        }

        private FileFacade GetFacade(IRepositoryManager repositoryManager, string pathId, byte[] data)
        {
            var fileStorage = new TestFileStorage(pathId, data);

            var (usersFacade, directoryFacade, userValidationFacade) = GetRequiredFacades(repositoryManager);

            return new FileFacade(repositoryManager, MemoryCache, fileStorage, usersFacade, userValidationFacade, directoryFacade);
        }

        private FileFacade GetFacade(IRepositoryManager repositoryManager, string pathId)
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

                var facade = GetFacade(manager, fileName);

                var id = await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995),
                    fileName,
                    new MemoryStream(data)
                );

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

                var facade = GetFacade(manager, fileName);

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

                var facade = GetFacade(manager, fileName);

                await Assert.ThrowsAsync<AuthenticationException>(async () =>
                {
                    await facade.SaveFileAsync(
                        await GetUserByLoginAsync(
                            context,
                            UsersAliases.TestUser
                        ),
                        fileName,
                        new MemoryStream(data)
                    );
                });
            });
        }

        [Fact]
        public async Task SaveFile_TheSameFile()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                const string fileName = "test-filename";

                var facade = GetFacade(manager, fileName);

                await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995), 
                    fileName,
                    new MemoryStream(data.ToArray())
                );

                var id = await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995), 
                    fileName,
                    new MemoryStream(data.ToArray())
                );

                Assert.NotEqual(default, id);

                var downloaded = await facade.GetFileAsync(id);

                Assert.Equal($"{fileName}_1", downloaded.FileName);
            });
        }
        [Fact]
        public async Task SaveFile_TheSameFileThirdTime()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                const string fileName = "test-filename";

                var facade = GetFacade(manager, fileName);

                await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995), 
                    fileName,
                    new MemoryStream(data.ToArray())
                );

                await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995), 
                    fileName,
                    new MemoryStream(data.ToArray())
                );

                var id = await facade.SaveFileAsync(
                    await GetUserByLoginAsync(context, UsersAliases.Mokeev1995), 
                    fileName,
                    new MemoryStream(data.ToArray())
                );

                Assert.NotEqual(default, id);

                var downloaded = await facade.GetFileAsync(id);

                Assert.Equal($"{fileName}_2", downloaded.FileName);
            });
        }

        [Fact]
        public async Task RemoveFileUsedWithPersonPhoto()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                const string filename = "test-file-for-remove-but-which-is-used-by-person";
                var facade = GetFacade(manager, filename);
                
                var user = await GetUserByLoginAsync(context, UsersAliases.Mokeev1995);
                var fileId = await facade.SaveFileAsync(user, filename, new MemoryStream(new byte[] {0, 1, 2, 3, 4}));
                var file = await manager.FilesRepository.GetAsync(fileId);
                
                file.Person = user.Person;
                user.Person.Photo = file;
                user.Person.PhotoId = file.Id;
                manager.UsersRepository.Update(user);
                manager.FilesRepository.Update(file);
                context.SaveChanges();

                await Assert.ThrowsAsync<FileIsUsedException>(async () =>
                {
                    await facade.Remove(fileId);
                });

                file = await manager.FilesRepository.FirstOrDefaultAsync(fileId);
                user = await GetUserByLoginAsync(context, UsersAliases.Mokeev1995);

                Assert.NotNull(file);
                Assert.Equal(file.Name, filename);

                Assert.NotNull(user);
                Assert.Equal(user.Person.PhotoId, fileId);
            });
        }

        [Fact]
        public async Task RemoveFileUsedWithPostSettings()
        {
            await WithRepositoryAsync(async (manager, context, logger) =>
            {
                const string filename = "test-file-for-remove-but-which-is-used-by-post-settings";
                var facade = GetFacade(manager, filename);
                
                var user = await GetUserByLoginAsync(context, UsersAliases.Mokeev1995);
                var fileId = await facade.SaveFileAsync(user, filename, new MemoryStream(new byte[] {0, 1, 2, 3, 4}));
                var file = await manager.FilesRepository.GetAsync(fileId);


                var postSetting = new PostSetting {PreviewImage = file, PreviewImageId = fileId};
                file.PostSettings = new List<PostSetting>
                {
                    postSetting
                };
                var setting = manager.PostSettingRepository.Insert(postSetting);
                manager.FilesRepository.Update(file);
                context.SaveChanges();

                await Assert.ThrowsAsync<FileIsUsedException>(async () =>
                {
                    await facade.Remove(fileId);
                });

                file = await manager.FilesRepository.FirstOrDefaultAsync(fileId);
                postSetting = await manager.PostSettingRepository.FirstOrDefaultAsync(setting.Id);

                Assert.NotNull(file);
                Assert.Equal(file.Name, filename);

                Assert.NotNull(postSetting);
                Assert.Equal(postSetting.PreviewImageId, fileId);
            });
        }
    }
}