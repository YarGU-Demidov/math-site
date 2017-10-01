using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Exceptions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.News
{
    public class NewsViewModelBuilder : SecondaryViewModelBuilder, INewsViewModelBuilder
    {
        public NewsViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
            : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
        {
        }

        public async Task<NewsIndexViewModel> BuildIndexViewModelAsync(int page = 1)
        {
            var model = await BuildSecondaryViewModel<NewsIndexViewModel>();
            await FillIndexPageNameAsync(model);

            await BuildPosts(model, page);

            model.Paginator = await GetPaginator(page);

            return model;
        }

        public async Task<NewsItemViewModel> BuildNewsItemViewModelAsync(Guid currentUserId, string query, int page = 1)
        {
            var model = await BuildSecondaryViewModel<NewsItemViewModel>();

            var post = await BuildPostData(currentUserId, query, page);

            if (post == null)
                throw new PostNotFoundException();

            model.Content = post.Content;
            model.PageTitle.Title = post.Title;

            return model;
        }

        private async Task<PaginatorViewModel> GetPaginator(int page)
        {
            var newsPostType = PostTypeAliases.News;
            return new PaginatorViewModel
            {
                CurrentPage = page,
                PagesCount = await PostsFacade.GetPostPagesCountAsync(newsPostType, RemovedStateRequest.Excluded, PublishStateRequest.Published, FrontPageStateRequest.AllVisibilityStates, true)
            };
        }


        private async Task FillIndexPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade[SiteSettingsNames.DefaultNewsPageTitle];

            model.PageTitle.Title = title ?? "Новости нашего факультета";
        }

        private async Task BuildPosts(NewsIndexViewModel model, int page)
        {
            var postType = PostTypeAliases.News;
            var posts = (await PostsFacade.GetPostsAsync(postType, page, true)).ToArray();

            model.Posts = GetPosts(posts);
        }

        private IEnumerable<PostPreviewViewModel> GetPosts(IEnumerable<Post> posts)
        {
            return posts.Select(PostPreviewViewModelBuilder.Build);
        }

        private async Task<Post> BuildPostData(Guid currentUserId, string query, int page = 1)
        {
            var postType = PostTypeAliases.News;
            return await PostsFacade.GetPostByUrlAndTypeAsync(currentUserId, query, postType, true);
        }
    }
}