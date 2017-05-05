using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;

namespace MathSite.Domain.Common
{
	public interface IBusinessLogicManger
	{
		IGroupsLogic GroupsLogic { get; }
		IPersonsLogic PersonsLogic { get; }
	}
}