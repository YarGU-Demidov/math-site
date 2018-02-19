using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Entities;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Categories;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Categories
{
    public class CategoryFacade: BaseFacade<ICategoryRepository, Category>, ICategoryFacade
    {
        public CategoryFacade(IRepositoryManager repositoryManager, IMemoryCache memoryCache) 
            : base(repositoryManager, memoryCache)
        {
        }

        public Task<Category> GetByAliasAsync(string categoryAlias)
        {
            var spec = new CategoryAliasSpecification(categoryAlias);

            return Repository.FirstOrDefaultAsync(spec);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await RepositoryManager.CategoryRepository.GetAllListAsync(c => c.PostCategories != null);
        }
    }
}