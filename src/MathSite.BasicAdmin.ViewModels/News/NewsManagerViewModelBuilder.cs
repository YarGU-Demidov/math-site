using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Entities.Dtos;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public class NewsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IMapper _mapper;
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;

        public NewsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper, IPostsFacade postsFacade, IUsersFacade usersFacade) :
            base(siteSettingsFacade)
        {
            _mapper = mapper;
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

        public async Task<NewsViewModel> BuildCreateViewModel(PostDto postDto = null)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Create"
            );

            if (postDto != null)
            {
                model.PageTitle.Title = postDto.Title;

                postDto.PostType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.News);

                var post = _mapper.Map<PostDto, Post>(postDto);
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
            model.AuthorId = post.AuthorId;
            model.SelectedAuthor = string.Empty;
            model.Authors = _usersFacade.GetUsers();
            model.PostTypeId = post.PostTypeId;
            model.PostSettingsId = post.PostSettingsId;
            model.PostSeoSettingsId = post.PostSeoSettingsId;

            return model;
        }

        public async Task<NewsViewModel> BuildEditViewModel(PostDto postDto)
        {
            var model = await BuildAdminBaseViewModelAsync<NewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Edit"
            );

            postDto.PostType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.News);
            postDto.PostSettings = await _postsFacade.GetPostSettingsAsync(postDto.PostSettingsId);
            postDto.PostSeoSetting = await _postsFacade.GetPostSeoSettingsAsync(postDto.PostSeoSettingsId);

            model.Id = postDto.Id;
            model.Title = postDto.Title;
            model.Excerpt = postDto.Excerpt;
            model.Content = postDto.Content;
            model.Published = postDto.Published;
            model.Deleted = postDto.Deleted;
            model.PublishDate = postDto.PublishDate;
            model.AuthorId = postDto.AuthorId;
            model.Authors = _usersFacade.GetUsers();
            model.PostTypeId = postDto.PostTypeId;
            model.PostSettingsId = postDto.PostSettingsId;
            model.PostSeoSettingsId = postDto.PostSeoSettingsId;

            var post = _mapper.Map<PostDto, Post>(postDto);
            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexNewsViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "Delete"
            );

            var post = await _postsFacade.GetPostAsync(id);

            post.Deleted = true;

            await _postsFacade.UpdatePostAsync(post);

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