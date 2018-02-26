using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
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
    public class PagesManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IPagesManagerViewModelBuilder
    {
        private readonly IMapper _mapper;
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;
        private readonly ICategoryFacade _categoriesFacade;
        private readonly IPostCategoryFacade _postCategoryFacade;

        public PagesManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper,
            IPostsFacade postsFacade, IUsersFacade usersFacade, ICategoryFacade categoriesFacade,
            IPostCategoryFacade postCategoryFacade) :
            base(siteSettingsFacade)
        {
            _mapper = mapper;
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
            _categoriesFacade = categoriesFacade;
            _postCategoryFacade = postCategoryFacade;
        }

        public async Task<IndexPagesViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.StaticPage;
            const RemovedStateRequest removedState = RemovedStateRequest.Excluded;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexPagesViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "List",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState,
                    cached),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState,
                frontPageState, cached);
            model.PageTitle.Title = "Список статей";

            return model;
        }

        public async Task<IndexPagesViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.StaticPage;
            const RemovedStateRequest removedState = RemovedStateRequest.OnlyRemoved;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexPagesViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "ListRemoved",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState,
                    cached),
                perPage
            );

            model.Posts =
                await _postsFacade.GetPostsAsync(postType, page, 5, removedState, publishState, frontPageState, cached);
            model.PageTitle.Title = "Список удаленных статей";

            return model;
        }

        public async Task<PageViewModel> BuildCreateViewModel()
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Create"
            );

            model.Authors = GetSelectListItems(await _usersFacade.GetUsersAsync());
            model.Categories = GetSelectListItems(await _postsFacade.GetPostCategoriesAsync());

            return model;
        }

        public async Task<PageViewModel> BuildCreateViewModel(PageViewModel page)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Create"
            );
            model.PageTitle.Title = page.Title;

            var postType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.StaticPage);
            var categories = await _categoriesFacade.GetCategoreisByIdAsync(page.SelectedCategories);

            page.Id = Guid.NewGuid();
            page.Excerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content;
            page.PostTypeId = postType.Id;
            page.PostSettings = new PostSetting();
            page.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<PageViewModel, Post>(page);
            post.PostCategories = _postCategoryFacade.CreateRelation(post, categories).Result.ToList();

            await _postsFacade.CreatePostAsync(post);

            return model;
        }

        public async Task<PageViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Edit"
            );

            var post = await _postsFacade.GetPostAsync(id);

            model.Id = post.Id;
            model.Title = post.Title;
            model.Excerpt = post.Excerpt;
            model.Content = post.Content;
            model.Published = post.Published;
            model.Deleted = post.Deleted;
            model.PublishDate = post.PublishDate;
            model.AuthorId = post.AuthorId;
            model.Authors = GetSelectListItems(await _usersFacade.GetUsersAsync());
            model.PostTypeId = post.PostTypeId;
            model.PostSettingsId = post.PostSettingsId;
            model.PostSeoSettingsId = post.PostSeoSettingsId;

            return model;
        }

        public async Task<PageViewModel> BuildEditViewModel(PageViewModel page)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Edit"
            );

            var postType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.StaticPage);
            var postSettings = await _postsFacade.GetPostSettingsAsync(page.PostSettingsId);
            var postSeoSetting = await _postsFacade.GetPostSeoSettingsAsync(page.PostSeoSettingsId);

            model.Id = page.Id;
            model.Title = page.Title;
            model.Excerpt = page.Excerpt;
            model.Content = page.Content;
            model.Published = page.Published;
            model.Deleted = page.Deleted;
            model.PublishDate = page.PublishDate;
            model.AuthorId = page.AuthorId;
            model.PostTypeId = postType.Id;
            model.PostSettingsId = postSettings.Id;
            model.PostSeoSettingsId = postSeoSetting.Id;

            var post = _mapper.Map<PageViewModel, Post>(page);
            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexPagesViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Delete"
            );

            var post = await _postsFacade.GetPostAsync(id);

            post.Deleted = true;

            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список страниц", "/manager/pages/list", false, "Список страниц", "List"),
                new MenuLink("Список удаленных страниц", "/manager/pages/removed", false, "Список удаленных страниц",
                    "ListRemoved"),
                new MenuLink("Создать страницу", "/manager/pages/create", false, "Создать страницу", "CreatePage")
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<User> elements)
        {
            return elements
                .Select(element => new SelectListItem
                {
                    Text = element.Person.Name + " " + element.Person.Surname,
                    Value = element.Id.ToString()
                });
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Category> elements)
        {
            return elements
                .Select(element => new SelectListItem
                {
                    Text = element.Name,
                    Value = element.Id.ToString()
                });
        }
    }
}