using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common.Specifications;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications.Categories;
using Microsoft.Extensions.Caching.Memory;

namespace MathSite.Facades.Categories
{
    public class CategoryFacade: BaseFacade<ICategoryRepository, Category>, ICategoryFacade
    {
        private readonly IUserValidationFacade _userValidationFacade;

        public CategoryFacade(
            IRepositoryManager repositoryManager, 
            IMemoryCache memoryCache,
            IUserValidationFacade userValidationFacade
        ) : base(repositoryManager, memoryCache)
        {
            _userValidationFacade = userValidationFacade;
        }

        private TimeSpan CacheMinutes { get; } = TimeSpan.FromMinutes(5);

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(id);
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

        public async Task<IEnumerable<Category>> GetCategoriesWithPostRelationAsync()
        {
            return await RepositoryManager.CategoryRepository.GetAllListAsync(c => c.PostCategories != null);
        }

        public async Task<Guid> CreateCategory(Guid currentUser, string name, string alias, string description = null)
        {
            if (!await _userValidationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess))
                throw new AccessViolationException();

            return await Repository.InsertAndGetIdAsync(new Category
            {
                Alias = alias,
                Name = name,
                Description = description
            });
        }

        public async Task UpdateCategory(Guid currentUser, Guid id, string name, string description = null)
        {
            if (!await _userValidationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess))
                throw new AccessViolationException();

            await Repository.UpdateAsync(id, async category =>
            {
                category.Name = name;
                category.Description = description;

                await Repository.UpdateAsync(category);
            });
        }

        public async Task DeleteCategory(Guid currentUser, Guid id)
        {
            if (!await _userValidationFacade.UserHasRightAsync(currentUser, RightAliases.AdminAccess))
                throw new AccessViolationException();
            await Repository.DeleteAsync(id);
        }

        public async Task<int> GetCategoriesPagesCountAsync(int perPage, bool cache = true)
        {
            var spec = new AnySpecification<Category>();
            
            var cacheKey = $"Categories:Count;PerPage={perPage}";

            async Task<int> GetCount()
            {
                return await GetCountAsync(spec, cache, CacheMinutes, $"{cacheKey}");
            }

            var count =  cache
                ? await MemoryCache.GetOrCreateAsync($"{cacheKey};PagesCount", async entry =>
                {
                    entry.SetSlidingExpiration(CacheMinutes);
                    entry.SetPriority(CacheItemPriority.Low);

                    return await GetCount();
                })
                : await GetCount();

            return (int) Math.Ceiling(count / (float) perPage);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int page, int perPage, bool cache)
        {
            page = page >= 1 ? page : 1;
            perPage = perPage > 0 ? perPage : 1;

            const CacheItemPriority cachePriority = CacheItemPriority.Normal;

            var toSkip = perPage * (page - 1);

            var requirements = new AnySpecification<Category>();

            var cacheKey = $"Page={page}:PerPage={perPage}:AllCategories";
            
            async Task<IEnumerable<Category>> GetPosts(Specification<Category> specification, int perPageCount, int toSkipCount)
            {
                return await Repository.GetAllPagedAsync(specification, perPageCount, toSkipCount);
            }

            return cache
                ? await MemoryCache.GetOrCreateAsync(
                    cacheKey,
                    async entry =>
                    {
                        entry.SetPriority(cachePriority);
                        entry.SetSlidingExpiration(CacheMinutes);

                        return await GetPosts(requirements, perPage, toSkip);
                    })
                : await GetPosts(requirements, perPage, toSkip);
        }
    }
}