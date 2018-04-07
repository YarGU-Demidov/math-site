using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.Categories;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Categories
{
    public interface ICategoriesViewModelBuilder
    {
        Task<IndexCategoriesViewModel> BuildIndexViewModelAsync(int page, int perPage);
        Task<CreateCategoriesViewModel> BuildCreateViewModelAsync();
        Task<EditCategoriesViewModel> BuildEditViewModelAsync(Guid id);
        Task CreateCategoryAsync(Guid currentUserId, CreateCategoriesViewModel model);
        Task EditCategoryAsync(Guid currentUserId, EditCategoriesViewModel model);
        Task DeleteCategoryAsync(Guid currentUserId, Guid id);
    }
    
    public class CategoriesViewModelBuilder: AdminPageWithPagingViewModelBuilder, ICategoriesViewModelBuilder
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoriesViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            ICategoryFacade categoryFacade
        ) : base(siteSettingsFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public async Task<IndexCategoriesViewModel> BuildIndexViewModelAsync(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<IndexCategoriesViewModel>(
                link => link.Alias == "Categories",
                link => link.Alias == "List",
                page,
                await _categoryFacade.GetCategoriesPagesCountAsync(perPage, false),
                perPage
            );

            model.Categories = await _categoryFacade.GetAllCategoriesAsync(page, perPage, false);
            model.PageTitle.Title = "Список категорий";
            
            return model;
        }

        public async Task<CreateCategoriesViewModel> BuildCreateViewModelAsync()
        {
            var model = await BuildAdminBaseViewModelAsync<CreateCategoriesViewModel>(
                link => link.Alias == "Categories",
                link => link.Alias == "Create"
            );

            model.PageTitle.Title = "Создать категорию";

            model.DisableAliasInput = false;

            return model;
        }

        public async Task<EditCategoriesViewModel> BuildEditViewModelAsync(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EditCategoriesViewModel>(
                link => link.Alias == "Categories"
            );
            
            model.PageTitle.Title = "Изменить категорию";
            
            var category = await _categoryFacade.GetCategoryByIdAsync(id);

            model.Name = category.Name;
            model.Alias = category.Alias;
            model.Description = category.Description;
            model.Id = category.Id.ToString();
            model.DisableAliasInput = true;

            return model;
        }

        public async Task CreateCategoryAsync(Guid currentUserId, CreateCategoriesViewModel model)
        {
            await _categoryFacade.CreateCategory(currentUserId, model.Name, model.Alias, model.Description);
        }

        public async Task EditCategoryAsync(Guid currentUserId, EditCategoriesViewModel model)
        {
            await _categoryFacade.UpdateCategory(currentUserId, Guid.Parse(model.Id), model.Name, model.Description);
        }

        public async Task DeleteCategoryAsync(Guid currentUserId, Guid id)
        {
            await _categoryFacade.DeleteCategory(currentUserId, id);
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список Категорий", "/manager/categories/list", false, "Список Категорий", "List"),
                new MenuLink("Создать Категорию", "/manager/categories/create", false, "Создать Категорию", "Create")
            };
        }
    }
}