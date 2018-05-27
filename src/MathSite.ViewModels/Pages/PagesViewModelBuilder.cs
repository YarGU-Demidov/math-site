using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Pages
{
    public class PagesViewModelBuilder : SecondaryViewModelBuilder, IPagesViewModelBuilder

    {
        public PagesViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
            : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
        {
        }

        public async Task<PageItemViewModel> BuildPageItemViewModelAsync(Guid currentUserId, string query)
        {
            var model = await BuildSecondaryViewModel<PageItemViewModel>();

            var post = await BuildPostData(currentUserId, query);
            if (post == null)
                throw new PostNotFoundException(query);

            model.PageTitle.Title = post.Title;
            model.Title = post.Title;
            model.Content = post.Content;

            return model;
        }

        private async Task<Post> BuildPostData(Guid currentUserId, string query)
        {
            return await PostsFacade.GetPostByUrlAndTypeAsync(currentUserId, query, PostTypeAliases.StaticPage);
        }
    }
}