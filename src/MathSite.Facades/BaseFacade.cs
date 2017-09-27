using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades
{
    public class BaseFacade
    {
        public BaseFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache)
        {
            RepositoryManager = repositoryManager;
            MemoryCache = memoryCache;
        }

        public IRepositoryManager RepositoryManager { get; }
        public IMemoryCache MemoryCache { get; }
    }
}