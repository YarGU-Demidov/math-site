using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class PagesManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IPagesManagerViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;

        public PagesManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade) :
            base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
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

        public async Task<CreatePageViewModel> BuildCreateViewModel(Post post = null)
        {
            var model = await BuildAdminBaseViewModelAsync<CreatePageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Create"
            );

            if (post != null)
            {
                model.PageTitle.Title = post.Title;

                await _postsFacade.CreatePostAsync(post);
            }

            return model;
        }

        public async Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexPagesViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Delete"
            );

            await _postsFacade.DeletePostAsync(id);

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
    }
}