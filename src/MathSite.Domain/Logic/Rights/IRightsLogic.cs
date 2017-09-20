using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.Rights
{
    public interface IRightsLogic
    {
        Task CreateAsync(string alias, string name, string description);
        Task UpdateAsync(string alias, string name, string description);
        Task DeleteAsync(string alias);

        Task<Right> TryGetByAliasAsync(string alias);
    }
}