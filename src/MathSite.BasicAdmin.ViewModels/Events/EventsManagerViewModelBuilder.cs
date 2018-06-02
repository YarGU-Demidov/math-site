using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.BasicAdmin.ViewModels.SharedModels.Posts;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Categories;
using MathSite.Facades.PostCategories;
using MathSite.Facades.Posts;
using MathSite.Facades.PostSeoSettings;
using MathSite.Facades.PostSettings;
using MathSite.Facades.PostTypes;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;

namespace MathSite.BasicAdmin.ViewModels.Events
{
    public class EventsManagerViewModelBuilder : PostViewModelBuilderBase, IEventsManagerViewModelBuilder
    {
        private readonly IPostSettingsFacade _postSettingsFacade;

        public EventsManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IPostsFacade postsFacade,
            IUsersFacade usersFacade,
            ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade,
            IPostSettingsFacade postSettingsFacade,
            IPostSeoSettingsFacade postSeoSettingsFacade,
            IPostTypeFacade postTypeFacade
        ) : base(
            siteSettingsFacade, 
            postsFacade, 
            usersFacade, 
            categoryFacade, 
            postCategoryFacade, 
            postSettingsFacade, 
            postSeoSettingsFacade,
            postTypeFacade
        )
        {
            _postSettingsFacade = postSettingsFacade;
        }

        public async Task<ListEventsViewModel> BuildIndexViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.Event;

            return await BuildIndexViewModel<ListEventsViewModel>(page, perPage, postType, EventsTopMenuName, "List",
                typeOfList: "событий");
        }

        public async Task<ListEventsViewModel> BuildRemovedViewModel(int page, int perPage)
        {
            const string postType = PostTypeAliases.Event;

            return await BuildRemovedViewModel<ListEventsViewModel>(page, perPage, postType, EventsTopMenuName,
                "ListRemoved", typeOfList: "событий");
        }

        public async Task<EventViewModel> BuildCreateViewModel()
        {
            return await BuildCreateViewModel<EventViewModel>(EventsTopMenuName, "CreateEvent");
        }

        public async Task<EventViewModel> BuildCreateViewModel(EventViewModel eventViewModel)
        {
            const string postType = PostTypeAliases.Event;

            var model = await BuildCreateViewModel(eventViewModel, postType, EventsTopMenuName, "CreateEvent");

            await _postSettingsFacade.UpdateForPostAsync(
                model.Id,
                eventViewModel.EventTime ?? DateTime.UtcNow,
                eventViewModel.EventLocation
            );

            return model;
        }

        public async Task<EventViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildEditViewModel<EventViewModel>(id, EventsTopMenuName, "Edit", "события");

            var settings = await _postSettingsFacade.GetForPostAsync(id);

            model.EventLocation = settings.EventLocation;
            model.EventTime = settings.EventTime;

            return model;
        }

        public async Task<EventViewModel> BuildEditViewModel(EventViewModel eventViewModel)
        {
            var model = await BuildEditViewModel(eventViewModel, EventsTopMenuName, "Edit");
            
            await _postSettingsFacade.UpdateForPostAsync(
                eventViewModel.Id, 
                eventViewModel.EventTime ?? DateTime.UtcNow, 
                eventViewModel.EventLocation
            );

            model.EventLocation = eventViewModel.EventLocation;
            model.EventTime = eventViewModel.EventTime;

            return model;
        }

        public async Task<ListEventsViewModel> BuildDeleteViewModel(Guid id)
        {
            return await BuildDeleteViewModel<ListEventsViewModel>(id, EventsTopMenuName, "Delete");
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
    }
}