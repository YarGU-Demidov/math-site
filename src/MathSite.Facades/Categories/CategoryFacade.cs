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

namespace MathSite.Facades.Categories
{
    public class CategoryFacade: BaseMathFacade<ICategoryRepository, Category>, ICategoryFacade
    {
        private readonly IUserValidationFacade _userValidationFacade;

        public CategoryFacade(
            IRepositoryManager repositoryManager,
            IUserValidationFacade userValidationFacade
        ) : base(repositoryManager)
        {
            _userValidationFacade = userValidationFacade;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await Repository.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoreisByIdAsync(IEnumerable<Guid> ids)
        {
            // TODO: переписать, тут много запросов кидаться может (и будет)
            // TODO: надо вхерачить тут конкатенацию через AND спецификаций по ID категории в GetAllListAsync
            var categoriesIds = new List<Category>();
            foreach (var id in ids)
            {
                categoriesIds.Add(await Repository.GetAsync(id));
            }

            return categoriesIds;
        }

        public async Task<Category> GetCategoryByAliasAsync(string categoryAlias)
        {
            var spec = new CategoryAliasSpecification(categoryAlias);

            return await Repository.FirstOrDefaultAsync(spec);
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
            var count = await GetCategoriesCount();

            return (int) Math.Ceiling(count / (float) perPage);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int page, int perPage)
        {
            return await GetItemsForPageAsync(page, perPage);
        }

        public async Task<int> GetCategoriesCount()
        {
            var spec = new AnySpecification<Category>();
            return await GetCountAsync(spec);
        }
    }
}