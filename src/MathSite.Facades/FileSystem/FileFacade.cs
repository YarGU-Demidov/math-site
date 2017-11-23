using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.FileSystem
{
    public interface IFileFacade
    {
    }


    public class FileFacade : BaseFacade<IFilesRepository, File>, IFileFacade
    {
        public FileFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
            : base(repositoryManager, memoryCache)
        {
        }
    }
}