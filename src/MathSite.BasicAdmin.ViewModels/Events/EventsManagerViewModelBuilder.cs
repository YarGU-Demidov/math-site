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

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public class EventsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IEventsManagerViewModelBuilder
    {
        private readonly IMapper _mapper;
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IPostCategoryFacade _postCategoryFacade;

        public EventsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper,
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

        public async Task<IndexEventsViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.Event;
            const RemovedStateRequest removedState = RemovedStateRequest.Excluded;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexEventsViewModel>(
                link => link.Alias == "Events",
                link => link.Alias == "List",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState,
                    frontPageState, cached),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState,
                frontPageState, cached);
            model.PageTitle.Title = "Список событий";

            return model;
        }

        public async Task<IndexEventsViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.Event;
            const RemovedStateRequest removedState = RemovedStateRequest.OnlyRemoved;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<IndexEventsViewModel>(
                link => link.Alias == "Events",
                link => link.Alias == "ListRemoved",
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState,
                    frontPageState, cached),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, 5, removedState, publishState,
                frontPageState, cached);
            model.PageTitle.Title = "Список удаленных событий";

            return model;
        }

        public async Task<EventViewModel> BuildCreateViewModel()
        {
            var model = await BuildAdminBaseViewModelAsync<EventViewModel>(
                link => link.Alias == "Events",
                link => link.Alias == "Create"
            );

            model.Authors = GetSelectListItems(await _usersFacade.GetUsersAsync());
            model.Categories = await GetSelectListItems(await _categoryFacade.GetCategoriesWithPostRelationAsync());

            return model;
        }

        public async Task<EventViewModel> BuildCreateViewModel(EventViewModel eventViewModel)
        {
            var model = await BuildAdminBaseViewModelAsync<EventViewModel>(
                link => link.Alias == "Events",
                link => link.Alias == "Create"
            );
            model.PageTitle.Title = eventViewModel.Title;

            var postType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.Event);
            var categories = await _categoryFacade.GetCategoreisByIdAsync(eventViewModel.SelectedCategories);

            eventViewModel.Id = Guid.NewGuid();
            eventViewModel.Excerpt = eventViewModel.Content.Length > 50
                ? $"{eventViewModel.Content.Substring(0, 47)}..."
                : eventViewModel.Content;
            eventViewModel.PublishDate = eventViewModel.Published ? DateTime.UtcNow : DateTime.MinValue;
            eventViewModel.PostTypeId = postType.Id;
            eventViewModel.PostSettings = new PostSetting();
            eventViewModel.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<EventViewModel, Post>(eventViewModel);
            post.PostCategories = _postCategoryFacade.CreateRelation(post, categories).ToList();

            await _postsFacade.CreatePostAsync(post);

            return model;
        }

        public async Task<EventViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EventViewModel>(
                link => link.Alias == "Events",
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
            model.Categories = await GetSelectListItems(await _categoryFacade.GetCategoriesWithPostRelationAsync(),
                id.ToString());

            return model;
        }

        public async Task<EventViewModel> BuildEditViewModel(EventViewModel eventViewModel)
        {
            var model = await BuildAdminBaseViewModelAsync<EventViewModel>(
                link => link.Alias == "Events",
                link => link.Alias == "Edit"
            );

            eventViewModel.Excerpt = eventViewModel.Content.Length > 50
                ? $"{eventViewModel.Content.Substring(0, 47)}..."
                : eventViewModel.Content;
            eventViewModel.PublishDate = eventViewModel.Published ? DateTime.UtcNow : DateTime.MinValue;
            eventViewModel.PostSettings = new PostSetting();
            eventViewModel.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<EventViewModel, Post>(eventViewModel);
            var selectedCategories = await _categoryFacade.GetCategoreisByIdAsync(eventViewModel.SelectedCategories);

            await _postCategoryFacade.DeletePostCategoryAsync(eventViewModel.Id);
            await _postsFacade.UpdatePostAsync(post);

            var postCategories = _postCategoryFacade.CreateRelation(post, selectedCategories);
            foreach (var postCategory in postCategories)
            {
                await _postCategoryFacade.CreatePostCategoryAsync(postCategory);
            }
            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexEventsViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexEventsViewModel>(
                link => link.Alias == "Events",
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
                new MenuLink("Список событий", "/manager/events/list", false, "Список событий", "List"),
                new MenuLink("Список удаленных событий", "/manager/events/removed", false, "Список удаленных событий", "ListRemoved"),
                new MenuLink("Создать событие", "/manager/events/create", false, "Создать событие", "CreateEvent")
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