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

            model.Categories = await _categoryFacade.GetAllCategoriesAsync(page, perPage, true);
            model.PageTitle.Title = "Список категорий";
            
            return model;
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