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
using MathSite.Facades.Users;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class NewsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;

        public NewsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade, IUsersFacade usersFacade) :
            base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
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

        public async Task<NewsViewModel> BuildCreateViewModel(Post post = null)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Create"
            );

            if (post != null)
            {
                model.PageTitle.Title = post.Title;

                await _postsFacade.CreatePostAsync(post);
            }

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
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
            model.CurrentAuthor = post.Author;
            model.SelectedAuthor = string.Empty;
            model.Authors = _usersFacade.GetUsers();
            model.PostType = post.PostType;
            model.PostSettings = post.PostSettings;
            model.PostSeoSetting = post.PostSeoSetting;
            model.PostCategories = post.PostCategories;

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(Post post)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Edit"
            );

            model.Id = post.Id;
            model.Title = post.Title;
            model.Excerpt = post.Excerpt;
            model.Content = post.Content;
            model.Published = post.Published;
            model.Deleted = post.Deleted;
            model.PublishDate = post.PublishDate;
            model.CurrentAuthor = post.Author;
            model.Authors = _usersFacade.GetUsers();
            model.PostType = post.PostType;
            model.PostSettings = post.PostSettings;
            model.PostSeoSetting = post.PostSeoSetting;
            model.PostCategories = post.PostCategories;

            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Delete"
            );

            await _postsFacade.DeletePostAsync(id);

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список новостей", "/manager/news/list", false, "Список новостей", "List"),
                new MenuLink("Список удаленных новостей", "/manager/news/removed", false, "Список удаленных новостей", "ListRemoved"),
                new MenuLink("Создать новость", "/manager/news/create", false, "Создать новость", "Create")
            };
        }
    }
}