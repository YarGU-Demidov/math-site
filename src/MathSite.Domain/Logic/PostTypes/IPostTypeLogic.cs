using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Domain.Logic.PostTypes
{
    public interface IPostTypeLogic
    {
        Task<PostType> TryGetByAliasAsync(string alias);
    }
}