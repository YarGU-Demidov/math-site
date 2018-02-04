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
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Events
{
    public class EventsViewModelBuilder : SecondaryViewModelBuilder, IEventsViewModelBuilder
    {
        public EventsViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder)
            : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
        {
        }

        public async Task<EventsIndexViewModel> BuildIndexViewModelAsync(int page = 1)
        {
            var model = await BuildSecondaryViewModel<EventsIndexViewModel>();
            await FillIndexPageNameAsync(model);

            await BuildPosts(model, page);

            model.Paginator = await GetPaginator(page);

            return model;
        }

        public async Task<EventItemViewModel> BuildNewsItemViewModelAsync(Guid currentUserId, string query, int page = 1)
        {
            var model = await BuildSecondaryViewModel<EventItemViewModel>();

            var post = await BuildPostData(currentUserId, query, page);

            if (post == null)
                throw new PostNotFoundException();

            model.Content = post.Content;
            model.Title = post.Title;
            model.Location = post.PostSettings.EventLocation;
            model.PreviewImageId = post.PostSettings.PreviewImage?.Id.ToString();
            model.PreviewImage2XId = post.PostSettings.PreviewImage?.Id.ToString();
            model.Date = post.PostSettings.EventTime?.ToString("dd MMMM yyyy");
            model.Time = post.PostSettings.EventTime?.ToString("HH:mm:ss");
            model.PageTitle.Title = post.Title;

            return model;
        }

        private async Task<PaginatorViewModel> GetPaginator(int page)
        {
            var postType = PostTypeAliases.Event;
            return new PaginatorViewModel
            {
                CurrentPage = page,
                PagesCount = await PostsFacade.GetPostPagesCountAsync(postType, RemovedStateRequest.Excluded, PublishStateRequest.Published, FrontPageStateRequest.AllVisibilityStates, true),
                Controller = "Events"
            };
        }


        private async Task FillIndexPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade[SiteSettingsNames.DefaultNewsPageTitle];

            model.PageTitle.Title = title ?? "Новости нашего факультета";
        }

        private async Task BuildPosts(EventsIndexViewModel model, int page)
        {
            var postType = PostTypeAliases.Event;
            var posts = (await PostsFacade.GetPostsAsync(postType, page, true)).ToArray();

            model.Posts = GetPosts(posts);
        }

        private IEnumerable<PostPreviewViewModel> GetPosts(IEnumerable<Post> posts)
        {
            return posts.Select(PostPreviewViewModelBuilder.Build);
        }

        private async Task<Post> BuildPostData(Guid currentUserId, string query, int page = 1)
        {
            var postType = PostTypeAliases.Event;
            return await PostsFacade.GetPostByUrlAndTypeAsync(currentUserId, query, postType, true);
        }
    }
}