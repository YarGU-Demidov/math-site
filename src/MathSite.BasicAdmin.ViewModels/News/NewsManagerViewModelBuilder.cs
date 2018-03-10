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

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class NewsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IMapper _mapper;
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IPostCategoryFacade _postCategoryFacade;

        public NewsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper,
            IPostsFacade postsFacade, IUsersFacade usersFacade, ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade) :
            base(siteSettingsFacade)
        {
            _mapper = mapper;
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
            _categoryFacade = categoryFacade;
            _postCategoryFacade = postCategoryFacade;
        }

        public async Task<IndexNewsViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.News;
            const RemovedStateRequest removedState = RemovedStateRequest.Excluded;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "List",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState, cached),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState, frontPageState, cached);
            model.PageTitle.Title = "Список новостей";

            return model;
        }

        public async Task<IndexNewsViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.News;
            const RemovedStateRequest removedState = RemovedStateRequest.OnlyRemoved;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "ListRemoved",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState, cached),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, 5, removedState, publishState, frontPageState, cached);
            model.PageTitle.Title = "Список удаленных новостей";

            return model;
        }

        public async Task<NewsViewModel> BuildCreateViewModel()
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Create"
            );

            model.Authors = GetSelectListItems(await _usersFacade.GetUsersAsync());
            model.Categories = await GetSelectListItems(
                await _categoryFacade.GetAllCategoriesAsync(
                    page: 1, 
                    perPage: await _categoryFacade.GetCategoriesCount(true), 
                    cache: true
                )
            );

            return model;
        }

        public async Task<NewsViewModel> BuildCreateViewModel(NewsViewModel news)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Create"
            );
            model.PageTitle.Title = news.Title;

            var postType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.News);
            var categories = await _categoryFacade.GetCategoreisByIdAsync(news.SelectedCategories);

            news.Id = Guid.NewGuid();
            news.Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content;
            news.PostTypeId = postType.Id;
            news.PostSettings = new PostSetting();
            news.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<NewsViewModel, Post>(news);
            post.PostCategories = _postCategoryFacade.CreateRelation(post, categories).ToList();

            await _postsFacade.CreatePostAsync(post);

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
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
            model.Categories = await GetSelectListItems(await _categoryFacade.GetAllCategoriesAsync(
                page: 1, 
                perPage: await _categoryFacade.GetCategoriesCount(true), 
                cache: true
            ));

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(NewsViewModel news)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Edit"
            );

            news.Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content;
            news.PostSettings = new PostSetting();
            news.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<NewsViewModel, Post>(news);
            var selectedCategories = await _categoryFacade.GetCategoreisByIdAsync(news.SelectedCategories);

            await _postCategoryFacade.DeletePostCategoryAsync(news.Id);
            await _postsFacade.UpdatePostAsync(post);

            var postCategories = _postCategoryFacade.CreateRelation(post, selectedCategories);
            foreach (var postCategory in postCategories)
            {
                await _postCategoryFacade.CreatePostCategoryAsync(postCategory);
            }
            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexNewsViewModel>(
                link => link.Alias == "News",
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
                new MenuLink("Список новостей", "/manager/news/list", false, "Список новостей", "List"),
                new MenuLink("Список удаленных новостей", "/manager/news/removed", false, "Список удаленных новостей", "ListRemoved"),
                new MenuLink("Создать новость", "/manager/news/create", false, "Создать новость", "Create")
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<User> users)
        {
            return users
                .Select(user => new SelectListItem
                {
                    Text = user.Person.Name + " " + user.Person.Surname,
                    Value = user.Id.ToString()
                });
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectListItems(IEnumerable<Category> categories,
            string postId = null)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var category in categories)
            {
                var postCategory = postId != null
                    ? await _postCategoryFacade.GetPostCategoryAsync(Guid.Parse(postId), category.Id)
                    : null;
                selectListItems.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = postCategory != null
                });
            }

            return selectListItems;
        }
    }
}