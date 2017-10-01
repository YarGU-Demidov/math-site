using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel(int page, int perPage);
        Task<IndexNewsViewModel> BuildRemovedViewModel(int page, int perPage);
    }

    public class NewsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;

        public NewsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade) :
            base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
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