using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Rights
{
	public interface IRightsLogic
	{
		Task CreateRightAsync(string alias, string name, string description);
		Task UpdateRightAsync(string alias, string name, string description);
		Task DeleteRightAsync(string alias);

		Task<Right> TryGetByAliasAsync(string alias);
	}
}