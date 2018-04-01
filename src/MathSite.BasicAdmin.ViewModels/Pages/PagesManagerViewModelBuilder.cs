using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.SharedModels;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.BasicAdmin.ViewModels.SharedModels.Posts;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.PostCategories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class PagesManagerViewModelBuilder : PostViewModelBuilderBase, IPagesManagerViewModelBuilder
    {
        public PagesManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IMapper mapper,
            IPostsFacade postsFacade, 
            IUsersFacade usersFacade, 
            ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade
        ) : base(siteSettingsFacade, mapper, postsFacade, usersFacade, categoryFacade, postCategoryFacade)
        {
        }

        public async Task<ListPagesViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.StaticPage;
            
            return await BuildIndexViewModel<ListPagesViewModel>(page, perPage, postType, ArticlesTopMenuName, "List", typeOfList: "статей");
        }

        public async Task<ListPagesViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.StaticPage;

            return await BuildRemovedViewModel<ListPagesViewModel>(page, perPage, postType, ArticlesTopMenuName, "ListRemoved", typeOfList: "статей");
        }

        public async Task<PageViewModel> BuildCreateViewModel()
        {
            return await BuildCreateViewModel<PageViewModel>(ArticlesTopMenuName, "CreatePage");
        }

        public async Task<PageViewModel> BuildCreateViewModel(PageViewModel page)
        {
            const string postType = PostTypeAliases.Event;

            return await BuildCreateViewModel(page, postType, ArticlesTopMenuName, "CreatePage");
        }

        public async Task<PageViewModel> BuildEditViewModel(Guid id)
        {
            return await BuildEditViewModel<PageViewModel>(id, ArticlesTopMenuName, "Edit", "статьи");
        }

        public async Task<PageViewModel> BuildEditViewModel(PageViewModel page)
        {
            return await BuildEditViewModel(page, ArticlesTopMenuName, "Edit");
        }

        public async Task<ListPagesViewModel> BuildDeleteViewModel(Guid id)
        {
            return await BuildDeleteViewModel<ListPagesViewModel>(id, ArticlesTopMenuName, "Delete");
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список страниц", "/manager/pages/list", false, "Список страниц", "List"),
                new MenuLink("Список удаленных страниц", "/manager/pages/removed", false, "Список удаленных страниц", "ListRemoved"),
                new MenuLink("Создать страницу", "/manager/pages/create", false, "Создать страницу", "CreatePage")
            };
        }
    }
}