using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Entities.Dtos;
using MathSite.Facades.Categories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.Pages
{
    public class PagesManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IPagesManagerViewModelBuilder
    {
        private readonly IMapper _mapper;
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;
        private readonly ICategoryFacade _categoriesFacade;

        public PagesManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IMapper mapper,
            IPostsFacade postsFacade, IUsersFacade usersFacade, ICategoryFacade categoriesFacade) :
            base(siteSettingsFacade)
        {
            _mapper = mapper;
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
            _categoriesFacade = categoriesFacade;
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

        public async Task<PageViewModel> BuildCreateViewModel(PostDto postDto = null)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Create"
            );

            model.Authors = GetSelectListItems(_usersFacade.GetUsers());

            if (postDto != null)
            {
                model.PageTitle.Title = postDto.Title;

                postDto.PostType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.StaticPage);

                var post = _mapper.Map<PostDto, Post>(postDto);
                await _postsFacade.CreatePostAsync(post);
            }

            return model;
        }

        public async Task<PageViewModel> BuildEditViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
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
            model.Authors = GetSelectListItems(_usersFacade.GetUsers());
            model.PostTypeId = post.PostTypeId;
            model.PostSettingsId = post.PostSettingsId;
            model.PostSeoSettingsId = post.PostSeoSettingsId;

            return model;
        }

        public async Task<PageViewModel> BuildEditViewModel(PostDto postDto)
        {
            var model = await BuildAdminBaseViewModelAsync<PageViewModel>(
                link => link.Alias == "Articles",
                link => link.Alias == "Edit"
            );

            postDto.PostType = await _postsFacade.GetPostTypeAsync(PostTypeAliases.StaticPage);
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
            model.Authors = GetSelectListItems(_usersFacade.GetUsers());
            model.PostTypeId = postDto.PostTypeId;
            model.PostSettingsId = postDto.PostSettingsId;
            model.PostSeoSettingsId = postDto.PostSeoSettingsId;

            var post = _mapper.Map<PostDto, Post>(postDto);
            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task<IndexPagesViewModel> BuildDeleteViewModel(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<IndexPagesViewModel>(
                link => link.Alias == "Articles",
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
                new MenuLink("Список страниц", "/manager/pages/list", false, "Список страниц", "List"),
                new MenuLink("Список удаленных страниц", "/manager/pages/removed", false, "Список удаленных страниц",
                    "ListRemoved"),
                new MenuLink("Создать страницу", "/manager/pages/create", false, "Создать страницу", "CreatePage")
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<User> elements)
        {
            return elements
                .Where(element => element.Person != null)
                .Select(element => new SelectListItem
                {
                    Text = element.Person?.Name + " " + element.Person?.Surname,
                    Value = element.Id.ToString()
                })
                .ToList();
        }
    }
}