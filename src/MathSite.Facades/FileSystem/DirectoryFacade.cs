using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Directories;
using Directory = MathSite.Entities.Directory;
using File = MathSite.Entities.File;

namespace MathSite.Facades.FileSystem
{
    public interface IDirectoryFacade : IFacade
    {
        Task<Directory> GetDirectoryWithPathAsync(string path);
        Task<Directory> TryGetDirectoryWithPathAsync(string path);
        Task CreateDirectoryAsync(string name, Guid? parentId = null);
        Task<Directory> GetDirectoryByIdAsync(Guid? id);
        Task MoveAllAsync(Directory from, Directory to);
        Task DeleteAsync(Guid id);
    }


    public class DirectoryFacade : BaseMathFacade<IDirectoriesRepository, Directory>, IDirectoryFacade
    {
        public DirectoryFacade(IRepositoryManager repositoryManager)
            : base(repositoryManager)
        {
        }

        public async Task<Directory> GetDirectoryWithPathAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("You need to specify path to directory!", nameof(path));

            if (path == "/" || path == "\\") return await GetRootDirAsync();

            var paths = new Queue<string>(path.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries));

            var tempName = paths.Dequeue();

            var spec = new DirectoryNameSpecification(tempName) as Specification<Directory>;

            var dir = await Repository
                .WithDirectories()
                .WithFiles()
                .FirstOrDefaultAsync(spec);

            // можно оптимизировать, если правильно собрать спецификации .And-ами и .Or-ами и т.п. и сделать 1 запрос вместо n
            while (paths.Count > 0)
            {
                if (dir == null)
                    throw new DirectoryNotFoundException(path);

                tempName = paths.Dequeue();

                var nestedDirectorySpec = new NestedDirectoryNameSpecification(dir.Id, tempName);
                var dirs = await Repository
                    .WithDirectories()
                    .WithFiles()
                    .GetAllListAsync(nestedDirectorySpec);

                if (dirs.Count > 1)
                    throw new ArgumentException($"Too many folders ({dirs.Count}) was found", nameof(path));

                dir = dirs.FirstOrDefault();
            }

            return dir ?? throw new DirectoryNotFoundException(path);
        }

        public async Task<Directory> TryGetDirectoryWithPathAsync(string path)
        {
            try
            {
                return await GetDirectoryWithPathAsync(path);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
        }

        public async Task CreateDirectoryAsync(string name, Guid? parentId = null)
        {
            var dir = new Directory
            {
                RootDirectoryId = parentId,
                Name = name
            };

            await Repository.InsertAsync(dir);
        }

        public async Task<Directory> GetDirectoryByIdAsync(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
                return await GetRootDirAsync();

            var spec = new DirectoryIdSpecification(id.Value);
            return await Repository.WithDirectories().WithFiles().WithRoot().FirstOrDefaultAsync(spec);
        }

        public async Task MoveAllAsync(Directory from, Directory to)
        {
            await Repository.ChangeRootAsync(from.Id, to?.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }

        private async Task<ICollection<Directory>> GetRootDirectoriesAsync()
        {
            return await Repository.GetAllListAsync(d => d.RootDirectoryId == null);
        }

        private async Task<ICollection<File>> GetRootFilesAsync()
        {
            return await RepositoryManager.FilesRepository.GetAllListAsync(f => f.DirectoryId == null);
        }

        private async Task<Directory> GetRootDirAsync()
        {
            return new Directory
            {
                CreationDate = DateTime.UtcNow,
                RootDirectoryId = null,
                Directories = await GetRootDirectoriesAsync(),
                Files = await GetRootFilesAsync(),
                Id = Guid.Empty,
                Name = new string(new[] {Path.DirectorySeparatorChar}),
                RootDirectory = null
            };
        }
    }
}