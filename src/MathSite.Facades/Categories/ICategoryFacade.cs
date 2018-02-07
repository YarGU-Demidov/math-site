using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Categories
{
    public interface ICategoryFacade
    {
        Task<Category> GetByAliasAsync(string categoryAlias);
    }
}