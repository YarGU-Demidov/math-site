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

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoreisByIdAsync(IEnumerable<Guid> ids)
        {
            var idsList = new List<Category>();
            foreach (var id in ids)
            {
                idsList.Add(await Repository.GetAsync(id));
            }

            return idsList;
        }

        public async Task<Category> GetCategoryByAliasAsync(string categoryAlias)
        {
            var spec = new CategoryAliasSpecification(categoryAlias);

            return await Repository.FirstOrDefaultAsync(spec);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await RepositoryManager.CategoryRepository.GetAllListAsync(c => c.PostCategories != null);
        }
    }
}