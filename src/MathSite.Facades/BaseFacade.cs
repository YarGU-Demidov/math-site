using MathSite.Domain.Common;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades
{
	public class BaseFacade
	{
		public BaseFacade(IBusinessLogicManager logicManager, IMemoryCache memoryCache)
		{
			LogicManager = logicManager;
			MemoryCache = memoryCache;
		}

		public IBusinessLogicManager LogicManager { get; }
		public IMemoryCache MemoryCache { get; }
	}
}