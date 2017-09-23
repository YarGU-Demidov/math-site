using MathSite.Repository.Core;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades
{
    public class BaseFacade
    {
        public BaseFacade(IRepositoryManager logicManager, IMemoryCache memoryCache)
        {
            LogicManager = logicManager;
            MemoryCache = memoryCache;
        }

        public IRepositoryManager LogicManager { get; }
        public IMemoryCache MemoryCache { get; }
    }
}