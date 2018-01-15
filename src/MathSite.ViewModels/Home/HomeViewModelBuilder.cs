using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.EventPreview;
using MathSite.ViewModels.Home.PostPreview;

namespace MathSite.ViewModels.Home
{
    public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
    {
        private readonly IPostPreviewViewModelBuilder _postPreviewViewModelBuilder;
        private readonly IEventPreviewViewModelBuilder _eventPreviewViewModelBuilder;
        private readonly IPostsFacade _postsFacade;

        public HomeViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder,
            IEventPreviewViewModelBuilder eventPreviewViewModelBuilder
        )
            : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _postPreviewViewModelBuilder = postPreviewViewModelBuilder;
            _eventPreviewViewModelBuilder = eventPreviewViewModelBuilder;
        }
        
        public async Task<HomeIndexViewModel> BuildIndexModel()
        {
            var model = await BuildCommonViewModelAsync<HomeIndexViewModel>();
            await FillPageNameAsync(model);
            await BuildPostsAsync(model);
            return model;
        }

        private async Task BuildPostsAsync(HomeIndexViewModel model)
        {
            const string postType = PostTypeAliases.News;

            var posts = await _postsFacade.GetPostsAsync(
                postTypeAlias: postType, 
                page: 1, 
                perPage: 6, 
                state: RemovedStateRequest.Excluded, 
                publishState: PublishStateRequest.Published, 
                frontPageState: FrontPageStateRequest.Visible, 
                cache: true
            );

            var events = await _postsFacade.GetPostsAsync(
                PostTypeAliases.Event,
                1,
                3,
                RemovedStateRequest.Excluded,
                PublishStateRequest.Published,
                FrontPageStateRequest.AllVisibilityStates,
                true
            );

            model.Posts = GetPostsModels(posts);
            model.Events = GetEventsModels(events);
        }

        private IEnumerable<EventPreviewViewModel> GetEventsModels(IEnumerable<Post> events)
        {
            return events.Select(_eventPreviewViewModelBuilder.Build);
        }

        private IEnumerable<PostPreviewViewModel> GetPostsModels(IEnumerable<Post> posts)
        {
            return posts.Select(_postPreviewViewModelBuilder.Build);
        }

        private async Task FillPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade[SiteSettingsNames.DefaultHomePageTitle];

            model.PageTitle.Title = title ?? "Главная страница";
        }
    }
}