using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.Dtos;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Categories;
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
        private readonly ICategoryFacade _categoriesFacade;

        public NewsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper,
            IPostsFacade postsFacade, IUsersFacade usersFacade, ICategoryFacade categoriesFacade) :
            base(siteSettingsFacade)
        {
            _mapper = mapper;
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
            _categoriesFacade = categoriesFacade;
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
            model.Categories = GetSelectListItems(await _categoriesFacade.GetCategoriesAsync());

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

            news.Id = Guid.NewGuid();
            news.Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content;
            news.PostTypeId = postType.Id;
            news.PostSettings = new PostSetting();
            news.PostSeoSetting = new PostSeoSetting();
            news.PostCategories = new List<PostCategory>();

            var post = _mapper.Map<NewsViewModel, Post>(news);

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

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(NewsViewModel news)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Edit"
            );

            var postType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.News);
            var postSettings = await _postsFacade.GetPostSettingsAsync(news.PostSettingsId);
            var postSeoSetting = await _postsFacade.GetPostSeoSettingsAsync(news.PostSeoSettingsId);

            model.Id = news.Id;
            model.Title = news.Title;
            model.Excerpt = news.Excerpt;
            model.Content = news.Content;
            model.Published = news.Published;
            model.Deleted = news.Deleted;
            model.PublishDate = news.PublishDate;
            model.AuthorId = news.AuthorId;
            model.PostTypeId = postType.Id;
            model.PostSettingsId = postSettings.Id;
            model.PostSeoSettingsId = postSeoSetting.Id;

            var post = _mapper.Map<NewsViewModel, Post>(news);
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