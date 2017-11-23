using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using Directory = MathSite.Entities.Directory;
using File = MathSite.Entities.File;

namespace MathSite.Facades.FileSystem
{
    public interface IDirectoryFacade
    {
        Task<Directory> GetDirectoryWithPathAsync(string path);
        Task<Directory> TryGetDirectoryWithPathAsync(string path);
    }


    public class DirectoryFacade : BaseFacade<IDirectoriesRepository, Directory>, IDirectoryFacade
    {
        public DirectoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        public async Task<Directory> GetDirectoryWithPathAsync(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("You need to specify path to directory!", nameof(path));

            if (path == "/")
            {
                return new Directory
                {
                    CreationDate = DateTime.UtcNow,
                    RootDirectoryId = null,
                    Directories = await GetRootDirectories(),
                    Files = await GetRootFiles(),
                    Id = Guid.Empty,
                    Name = "/",
                    RootDirectory = null
                };
            }
            
            var paths = new Queue<string>(path.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries));

            var tempName = paths.Dequeue();
            var dir = await Repository.FirstOrDefaultAsync(d => d.Name == tempName);

            while (paths.Count > 0)
            {
                if(dir == null) 
                    throw new DirectoryNotFoundException(path);
                
                tempName = paths.Dequeue();
                dir = dir.Directories.FirstOrDefault(d => d.Name == tempName);
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

        private async Task<ICollection<Directory>> GetRootDirectories()
        {
            return await Repository.GetAllListAsync(d => d.RootDirectoryId == null);
        }

        private async Task<ICollection<File>> GetRootFiles()
        {
            return await RepositoryManager.FilesRepository.GetAllListAsync(f => f.DirectoryId == null);
        }
    }
}