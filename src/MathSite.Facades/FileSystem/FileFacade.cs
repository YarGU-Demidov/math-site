using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Common.FileStorage;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Files;
using Microsoft.Extensions.Caching.Memory;
using File = MathSite.Entities.File;

namespace MathSite.Facades.FileSystem
{
    public interface IFileFacade : IFacade
    {
        /// <summary></summary>
        /// <param name="currentUser"></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="dirPath"></param>
        /// <exception cref="AuthenticationException">You must be authenticated and authorized for this action!</exception>
        /// <returns></returns>
        Task<Guid> SaveFileAsync(User currentUser, string name, Stream data, string dirPath = "/");

        Task<IEnumerable<File>> GetFilesByExtensions(IEnumerable<string> extensions);

        Task<(string FileName, Stream FileStream, string Extension)> GetFileAsync(Guid id);
        Task Remove(Guid id);
    }


    public class FileFacade : BaseMathFacade<IFilesRepository, File>, IFileFacade
    {
        private readonly Lazy<IDirectoryFacade> _directoryFacade;
        private readonly IFileStorage _fileStorage;
        private readonly IUsersFacade _usersFacade;
        private readonly IUserValidationFacade _userValidationFacade;

        public FileFacade(
            IRepositoryManager repositoryManager,
            IMemoryCache memoryCache,
            IFileStorage fileStorage,
            IUsersFacade usersFacade,
            IUserValidationFacade userValidationFacade,
            Lazy<IDirectoryFacade> directoryFacade
        )
            : base(repositoryManager, memoryCache)
        {
            _fileStorage = fileStorage;
            _usersFacade = usersFacade;
            _userValidationFacade = userValidationFacade;
            _directoryFacade = directoryFacade;
        }

        public async Task<IEnumerable<File>> GetFilesByExtensions(IEnumerable<string> extensions)
        {
            var spec = new FileExtensionsSpecification(extensions);
            return await Repository.GetAllListAsync(spec);
        }

        public async Task<(string FileName, Stream FileStream, string Extension)> GetFileAsync(Guid id)
        {
            var file = await Repository.FirstOrDefaultAsync(id);
            return file.IsNull()
                ? (null, null, null)
                : (file.Name, _fileStorage.GetFileStream(file.Path), file.Extension);
        }

        public async Task Remove(Guid id)
        {
            var file = await Repository
                .WithPerson()
                .WithPostAttachments()
                .WithPostSetting()
                .GetAsync(id);

            if (file.Person.IsNotNull() || file.PostAttachments.IsNotNullOrEmpty() || file.PostSettings.IsNotNullOrEmpty())
            {
                throw new FileIsUsedException();
            }

            var deleteFileTask = await RepositoryManager.FilesRepository.CountAsync(f => f.Path == file.Path) <= 1
                ? _fileStorage.Remove(file.Path)
                : Task.CompletedTask;

            var tasks = new[]
            {
                deleteFileTask,
                Repository.DeleteAsync(id)
            };

            await Task.WhenAll(tasks);
        }

        public async Task<Guid> SaveFileAsync(User currentUser, string name, Stream data, string dirPath = "/")
        {
            var hash = GetFileHashString(data);

            var alreadyExistsFile =
                await Repository.LastOrDefaultOrderedByAsync(f => f.Hash == hash, f => f.CreationDate, true);

            using (data)
            {
                if (data.CanSeek)
                    data.Seek(0, SeekOrigin.Begin);

                var pathId = alreadyExistsFile.IsNotNull()
                    ? alreadyExistsFile.Path
                    : await _fileStorage.SaveFileAsync(name, data);

                var user = await _usersFacade.GetUserAsync(currentUser?.Id ?? Guid.Empty);

                var hasRight = await _userValidationFacade.UserHasRightAsync(user, "admin");

                if (!hasRight)
                    throw new AuthenticationException("You must be authenticated and authorized for this action!");

                var dir = await _directoryFacade.Value.TryGetDirectoryWithPathAsync(dirPath);

                var file = new File
                {
                    Hash = hash,
                    Extension = Path.GetExtension(name),
                    Name = GetFileName(name, alreadyExistsFile),
                    Path = pathId,
                    DirectoryId = !dirPath.IsNullOrWhiteSpace() && dir.Id != Guid.Empty
                        ? dir.Id
                        : null as Guid?
                };

                return await Repository.InsertAndGetIdAsync(file);
            }
        }

        private static string GetFileName(string currentName, File file)
        {
            if (file == null)
                return currentName;

            var splitedName = Path.GetFileNameWithoutExtension(file.Name)?.Split(new[] {"_"}, StringSplitOptions.None)
                .ToList();

            if (splitedName?.Count > 1 && long.TryParse(splitedName.LastOrDefault(), out var postfixValue))
                splitedName[splitedName.Count - 1] = $"{++postfixValue}";
            else
                splitedName?.Add("1");

            return splitedName?.Aggregate((f, s) => $"{f}_{s}") + Path.GetExtension(currentName);
        }

        private static string GetFileHashString(Stream data)
        {
            byte[] hash;
            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(data);
            }

            return hash.Select(b => b.ToString("X2")).Aggregate((f, s) => $"{f}{s}");
        }
    }
}