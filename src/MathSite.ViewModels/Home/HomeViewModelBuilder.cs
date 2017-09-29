using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels.PostPreview;

namespace MathSite.ViewModels.Home
{
    public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
    {
        private readonly IPostPreviewViewModelBuilder _postPreviewViewModelBuilder;
        private readonly IPostsFacade _postsFacade;

        public HomeViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
            : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _postPreviewViewModelBuilder = postPreviewViewModelBuilder;
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

            var posts = await _postsFacade.GetPostsAsync(postType, 1, 6, RemovedStateRequest.Excluded, PublishStateRequest.Published, FrontPageStateRequest.Visible, true);
            model.Posts = GetPostsModels(posts);
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