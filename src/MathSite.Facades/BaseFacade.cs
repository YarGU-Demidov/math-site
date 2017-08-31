using MathSite.Domain.Common;

namespace MathSite.Facades
{
	public class BaseFacade
	{
		public BaseFacade(IBusinessLogicManger logicManger)
		{
			LogicManger = logicManger;
		}

		public IBusinessLogicManger LogicManger { get; }
	}
}