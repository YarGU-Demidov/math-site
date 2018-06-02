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
    public interface IDirectoryFacade: IFacade
    {
        Task<Directory> GetDirectoryWithPathAsync(string path);
        Task<Directory> TryGetDirectoryWithPathAsync(string path);
    }


    public class DirectoryFacade : BaseMathFacade<IDirectoriesRepository, Directory>, IDirectoryFacade
    {
        public DirectoryFacade(IRepositoryManager repositoryManager) 
            : base(repositoryManager)
        {
        }

        public async Task<Directory> GetDirectoryWithPathAsync(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("You need to specify path to directory!", nameof(path));

            if (path == "/" || path == "\\")
            {
                return new Directory
                {
                    CreationDate = DateTime.UtcNow,
                    RootDirectoryId = null,
                    Directories = await GetRootDirectoriesAsync(),
                    Files = await GetRootFilesAsync(),
                    Id = Guid.Empty,
                    Name = new string(new [] {Path.DirectorySeparatorChar}),
                    RootDirectory = null
                };
            }
            
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
                if(dir == null) 
                    throw new DirectoryNotFoundException(path);
                
                tempName = paths.Dequeue();

                var nestedDirectorySpec = new NestedDirectoryNameSpecification(dir.Id, tempName);
                dir = await Repository
                    .WithDirectories()
                    .WithFiles()
                    .FirstOrDefaultAsync(nestedDirectorySpec);
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

        private async Task<ICollection<Directory>> GetRootDirectoriesAsync()
        {
            return await Repository.GetAllListAsync(d => d.RootDirectoryId == null);
        }

        private async Task<ICollection<File>> GetRootFilesAsync()
        {
            return await RepositoryManager.FilesRepository.GetAllListAsync(f => f.DirectoryId == null);
        }
    }
}