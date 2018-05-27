using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.PostPreview;

namespace MathSite.ViewModels.SharedModels.SecondaryPage
{
    public abstract class SecondaryViewModelBuilder : CommonViewModelBuilder
    {
        protected IPostPreviewViewModelBuilder PostPreviewViewModelBuilder;

        protected SecondaryViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
            : base(siteSettingsFacade)
        {
            PostPreviewViewModelBuilder = postPreviewViewModelBuilder;
            PostsFacade = postsFacade;
        }

        protected IPostsFacade PostsFacade { get; }

        protected async Task<T> BuildSecondaryViewModel<T>()
            where T : SecondaryViewModel, new()
        {
            var model = await BuildCommonViewModelAsync<T>();

            await BuildFeaturedMenuAsync(model);

            return model;
        }

        private async Task BuildFeaturedMenuAsync(SecondaryViewModel model)
        {
            var posts = await PostsFacade.GetPostsAsync(PostTypeAliases.News, 1, 3, RemovedStateRequest.Excluded, PublishStateRequest.Published, FrontPageStateRequest.Visible);

            model.Featured = GetPostsModels(posts);
        }

        private IEnumerable<PostPreviewViewModel> GetPostsModels(IEnumerable<Post> posts)
        {
            return posts.Select(PostPreviewViewModelBuilder.Build);
        }
    }
}