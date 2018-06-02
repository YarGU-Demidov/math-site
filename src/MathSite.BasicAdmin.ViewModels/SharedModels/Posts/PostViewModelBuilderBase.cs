using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.PostCategories;
using MathSite.Facades.Posts;
using MathSite.Facades.PostSeoSettings;
using MathSite.Facades.PostSettings;
using MathSite.Facades.PostTypes;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.SecondaryPage;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Posts
{
    public abstract class PostViewModelBuilderBase : AdminPageWithPagingViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;
        private readonly IUsersFacade _usersFacade;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IPostCategoryFacade _postCategoryFacade;
        private readonly IPostSettingsFacade _postSettingsFacade;
        private readonly IPostSeoSettingsFacade _postSeoSettingsFacade;
        private readonly IPostTypeFacade _postTypeFacade;

        public PostViewModelBuilderBase(
            ISiteSettingsFacade siteSettingsFacade,
            IPostsFacade postsFacade, 
            IUsersFacade usersFacade, 
            ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade,
            IPostSettingsFacade postSettingsFacade,
            IPostSeoSettingsFacade postSeoSettingsFacade,
            IPostTypeFacade postTypeFacade
        ) : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _usersFacade = usersFacade;
            _categoryFacade = categoryFacade;
            _postCategoryFacade = postCategoryFacade;
            _postSettingsFacade = postSettingsFacade;
            _postSeoSettingsFacade = postSeoSettingsFacade;
            _postTypeFacade = postTypeFacade;
        }

        protected async Task<TModel> BuildIndexViewModel<TModel>(int page, int perPage, string postType, string activeTop, string activeLeft, string typeOfList)
            where TModel : ListPostViewModel, new()
        {
            const RemovedStateRequest removedState = RemovedStateRequest.Excluded;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;

            var model = await BuildAdminPageWithPaging<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft,
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState, frontPageState, false);
            model.PageTitle.Title = $"Список {typeOfList}";

            return model;
        }

        protected async Task<TModel> BuildRemovedViewModel<TModel>(int page, int perPage, string postType, string activeTop, string activeLeft, string typeOfList)
            where TModel : ListPostViewModel, new()
        {
            const RemovedStateRequest removedState = RemovedStateRequest.OnlyRemoved;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;

            var model = await BuildAdminPageWithPaging<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft,
                page,
                await _postsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState),
                perPage
            );

            model.Posts = await _postsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState, frontPageState, false);
            model.PageTitle.Title = $"Список удаленных {typeOfList}";

            return model;
        }

        protected async Task<TModel> BuildCreateViewModel<TModel>(string activeTop, string activeLeft)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var categories = await _categoryFacade.GetAllCategoriesAsync(
                page: 1,
                perPage: await _categoryFacade.GetCategoriesCount()
            );

            model.Authors = GetSelectListItems(await _usersFacade.GetAllUsersAsync());
            model.Categories = await GetSelectListItems(categories);

            return model;
        }

        protected async Task<TModel> BuildCreateViewModel<TModel>(TModel postModel, string postTypeAlias, string activeTop, string activeLeft)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            model.PageTitle.Title = postModel.Title;

            var postType = await _postsFacade.GetPostTypeAsync(postTypeAlias);

            postModel.Excerpt = postModel.Excerpt.IsNullOrWhiteSpace() || postModel.Excerpt.Length > 50 
                ? $"{postModel.Content.Substring(0, 47)}..." 
                : postModel.Content;
            
            postModel.PostTypeId = postType.Id;

            var postSeoSettings = await _postSeoSettingsFacade.CreateAsync(postModel.Url, postModel.SeoTitle, postModel.SeoDescription);
            var postSettings = await _postSettingsFacade.CreateAsync(postModel.IsCommentsAllowed, postModel.CanBeRated, postModel.PostOnStartPage, postModel.PreviewImageId);
            
            var post = new Post
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Excerpt = postModel.Excerpt,
                Content = postModel.Content,
                Published = postModel.Published,
                Deleted = postModel.Deleted,
                PublishDate = postModel.PublishDate,
                AuthorId = postModel.AuthorId,
                PostTypeId = postModel.PostTypeId,

                PostSeoSettingsId = postSeoSettings,
                PostSettingsId = postSettings
            };

            model.Id = await _postsFacade.CreatePostAsync(post);

            if (postModel.SelectedCategories.IsNullOrEmpty()) 
                return model;

            var categories = await _categoryFacade.GetCategoreisByIdAsync(postModel.SelectedCategories);
            post.PostCategories = (await _postCategoryFacade.CreateRelation(post, categories)).ToList();

            return model;
        }

        protected async Task<TModel> BuildEditViewModel<TModel>(Guid id, string activeTop, string activeLeft, string nameForEditTitle)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var post = await _postsFacade.GetPostAsync(id);
            var defaultSettings = (await _postTypeFacade.GetFromPostAsync(post)).DefaultPostsSettings;

            var categories = await _categoryFacade.GetAllCategoriesAsync(
                page: 1,
                perPage: await _categoryFacade.GetCategoriesCount()
            );

            var seoSettings = post.PostSeoSetting;
            var settings = post.PostSettings;

            model.Id = post.Id;
            model.Title = post.Title;
            model.Excerpt = post.Excerpt;
            model.Content = post.Content;
            model.Published = post.Published;
            model.Deleted = post.Deleted;
            model.PublishDate = post.PublishDate;
            model.AuthorId = post.AuthorId;
            model.Authors = GetSelectListItems(await _usersFacade.GetAllUsersAsync());
            model.PostTypeId = post.PostTypeId;
            
            model.SeoTitle = seoSettings?.Title;
            model.SeoDescription = seoSettings?.Description;
            model.Url = seoSettings?.Url;

            model.IsCommentsAllowed = settings?.IsCommentsAllowed ?? defaultSettings.IsCommentsAllowed;
            model.CanBeRated = settings?.CanBeRated ?? defaultSettings.CanBeRated;
            model.PostOnStartPage = settings?.PostOnStartPage ?? defaultSettings.PostOnStartPage;
            model.PreviewImageId = settings?.PreviewImageId ?? defaultSettings.PreviewImageId;

            model.Categories = await GetSelectListItems(categories, post.Id);

            model.PageTitle.Title = $"Правка {nameForEditTitle}";

            return model;
        }

        protected async Task<TModel> BuildEditViewModel<TModel>(TModel postModel, string activeTop, string activeLeft)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var post = await _postsFacade.GetPostAsync(postModel.Id);

            post.Title = postModel.Title;
            post.Excerpt = postModel.Excerpt;
            post.Content = postModel.Content;
            post.Published = postModel.Published;
            post.Deleted = postModel.Deleted;
            post.PublishDate = postModel.PublishDate;
            post.AuthorId = postModel.AuthorId;
            post.PostTypeId = postModel.PostTypeId;

            await _postSettingsFacade.UpdateForPostAsync(
                post,
                postModel.IsCommentsAllowed,
                postModel.CanBeRated,
                postModel.PostOnStartPage,
                postModel.PreviewImageId
            );

            await _postSeoSettingsFacade.UpdateForPost(
                post,
                postModel.Url,
                postModel.SeoTitle,
                postModel.SeoDescription
            );

            await _postsFacade.UpdatePostAsync(post);

            if (postModel.SelectedCategories.IsNotNullOrEmpty())
            {
                var selectedCategories = await _categoryFacade.GetCategoreisByIdAsync(postModel.SelectedCategories);
                await _postCategoryFacade.UpdateRelations(post, selectedCategories);
            }
            else
            {
                await _postCategoryFacade.DeleteAllRelations(post);
            }

            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        protected async Task<TModel> BuildDeleteViewModel<TModel>(Guid id, string activeTop, string activeLeft)
            where TModel: ListPostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var post = await _postsFacade.GetPostAsync(id);

            post.Deleted = true;

            await _postsFacade.UpdatePostAsync(post);

            return model;
        }

        public async Task BuildForceDeleteViewModel(Guid postId)
        {
            await _postsFacade.DeletePostAsync(postId);
            await _postSettingsFacade.RemoveForPostAsync(postId);
            await _postSeoSettingsFacade.RemoveForPostAsync(postId);
        }

        public async Task BuildRecoverViewModel(Guid postId)
        {
            var post = await _postsFacade.GetPostAsync(postId);

            post.Deleted = false;

            await _postsFacade.UpdatePostAsync(post);
        }

        public void FillPostItemViewModel(SecondaryViewModel model)
        {
            model.Featured = new List<PostPreviewViewModel>();
            model.Description = "PREVIEW";
            model.Keywords = "PREVIEW";
            model.MainMenuLinks = new List<IEnumerable<MenuItemViewModel>>();
            model.PageTitle = new MathSite.ViewModels.SharedModels.PageTitleViewModel(null, " | ", "PREVIEW PAGE");
            model.TopMenuLinks = new List<MenuItemViewModel>();
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<User> users)
        {
            return users
                .Select(user => new SelectListItem
                {
                    Text = user.Person.Name + " " + user.Person.Surname,
                    Value = user.Id.ToString()
                });
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectListItems(IEnumerable<Category> categories, Guid? postId = null)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var category in categories)
            {
                var postCategory = postId.HasValue
                    ? await _postCategoryFacade.GetPostCategoryAsync(postId.Value, category.Id)
                    : null;

                selectListItems.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = postCategory != null
                });
            }

            return selectListItems;
        }
    }
}