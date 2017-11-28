using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MathSite.Common.Extensions;
using MathSite.Common.FileStorage;
using MathSite.Facades.Users;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using File = MathSite.Entities.File;

namespace MathSite.Facades.FileSystem
{
    public interface IFileFacade
    {
        Task<Guid> SaveFileAsync(string name, Stream data);
        Task<(string FileName, Stream FileStream)> GetFileAsync(Guid id);
    }


    public class FileFacade : BaseFacade<IFilesRepository, File>, IFileFacade
    {
        private readonly IFileStorage _fileStorage;
        private readonly IUsersFacade _usersFacade;

        public FileFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache, IFileStorage fileStorage,
            IUsersFacade usersFacade)
            : base(repositoryManager, memoryCache)
        {
            _fileStorage = fileStorage;
            _usersFacade = usersFacade;
        }

        public async Task<(string FileName, Stream FileStream)> GetFileAsync(Guid id)
        {
            var file = await Repository.FirstOrDefaultAsync(id);
            return file.IsNull()
                ? (null, null)
                : (file.Name, _fileStorage.GetFileStream(file.Path));
        }

        public async Task<Guid> SaveFileAsync(string name, Stream data)
        {
            var hash = GetFileHashString(data);

            var alreadyExistsFile =
                await Repository.FirstOrDefaultOrderedByAsync(f => f.Hash == hash, f => f.DateAdded, false);

            using (data)
            {
                if (data.CanSeek)
                    data.Seek(0, SeekOrigin.Begin);

                var pathId = alreadyExistsFile.IsNotNull()
                    ? alreadyExistsFile.Path
                    : await _fileStorage.SaveFileAsync(name, data);

                var file = new File(GetFileName(name, alreadyExistsFile), pathId, Path.GetExtension(name), hash);

                return await Repository.InsertAndGetIdAsync(file);
            }
        }

        private static string GetFileName(string currentName, File file)
        {
            if (file == null)
                return currentName;

            var splitedName = Path.GetFileNameWithoutExtension(file.Name).Split(new[] {"_"}, StringSplitOptions.None)
                .ToList();
            if (splitedName.Count > 1 && long.TryParse(splitedName.LastOrDefault(), out var postfixValue))
                splitedName[splitedName.Count - 1] = $"{++postfixValue}";
            else
                splitedName.Add("1");

            return splitedName.Aggregate((f, s) => $"{f}_{s}") + Path.GetExtension(currentName);
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