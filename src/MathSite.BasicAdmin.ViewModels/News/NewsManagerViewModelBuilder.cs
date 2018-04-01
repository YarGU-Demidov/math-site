﻿using System;
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

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class NewsManagerViewModelBuilder : PostViewModelBuilderBase, INewsManagerViewModelBuilder
    {
        public NewsManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IMapper mapper,
            IPostsFacade postsFacade, 
            IUsersFacade usersFacade, 
            ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade
        ) : base(siteSettingsFacade, mapper, postsFacade, usersFacade, categoryFacade, postCategoryFacade)
        {
        }


        public async Task<ListNewsViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.News;
            
            return await BuildIndexViewModel<ListNewsViewModel>(page, perPage, postType, NewsTopMenuName, "List", typeOfList: "новостей");
        }

        public async Task<ListNewsViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.News;
            
            return await BuildRemovedViewModel<ListNewsViewModel>(page, perPage, postType, NewsTopMenuName, "ListRemoved", typeOfList: "новостей");
        }

        public async Task<NewsViewModel> BuildCreateViewModel()
        {
            return await BuildCreateViewModel<NewsViewModel>(NewsTopMenuName, "Create");
        }

        public async Task<NewsViewModel> BuildCreateViewModel(NewsViewModel news)
        {
            const string postType = PostTypeAliases.News;

            return await BuildCreateViewModel(news, postType, NewsTopMenuName, "Create");
        }

        public async Task<NewsViewModel> BuildEditViewModel(Guid id)
        {
            return await BuildEditViewModel<NewsViewModel>(id, NewsTopMenuName, "Edit", "новости");
        }

        public async Task<NewsViewModel> BuildEditViewModel(NewsViewModel news)
        {
            return await BuildEditViewModel(news, NewsTopMenuName, "Edit");
        }

        public async Task<ListNewsViewModel> BuildDeleteViewModel(Guid id)
        {
            return await BuildDeleteViewModel<ListNewsViewModel>(id, NewsTopMenuName, "Delete");
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список новостей", "/manager/news/list", false, "Список новостей", "List"),
                new MenuLink("Список удаленных новостей", "/manager/news/removed", false, "Список удаленных новостей",
                    "ListRemoved"),
                new MenuLink("Создать новость", "/manager/news/create", false, "Создать новость", "Create")
            };
        }
    }
}