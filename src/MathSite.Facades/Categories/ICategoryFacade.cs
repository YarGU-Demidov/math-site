using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;

namespace MathSite.Facades.Categories
{
    public interface ICategoryFacade
    {
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetCategoreisByIdAsync(IEnumerable<Guid> ids);
        Task<Category> GetCategoryByAliasAsync(string categoryAlias);
        Task<IEnumerable<Category>> GetCategoriesWithPostRelationAsync();
        Task<Guid> CreateCategory(Guid currentUser, string name, string alias, string description = null);
        Task UpdateCategory(Guid currentUser, Guid id, string name, string description = null);
        Task DeleteCategory(Guid currentUser, Guid id);
        Task<int> GetCategoriesPagesCountAsync(int perPage, bool cache = true);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int page, int perPage, bool cache);
    }
}