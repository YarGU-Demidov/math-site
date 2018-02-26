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
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}