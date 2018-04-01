using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.Common;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.PostCategories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.SharedModels.Posts
{
    public abstract class PostViewModelBuilderBase : AdminPageWithPagingViewModelBuilder
    {
        private readonly IMapper _mapper;
        protected readonly IPostsFacade PostsFacade;
        protected readonly IUsersFacade UsersFacade;
        protected readonly ICategoryFacade CategoryFacade;
        protected readonly IPostCategoryFacade PostCategoryFacade;

        public PostViewModelBuilderBase(
            ISiteSettingsFacade siteSettingsFacade, 
            IMapper mapper,
            IPostsFacade postsFacade, 
            IUsersFacade usersFacade, 
            ICategoryFacade categoryFacade,
            IPostCategoryFacade postCategoryFacade
        ) : base(siteSettingsFacade)
        {
            _mapper = mapper;
            PostsFacade = postsFacade;
            UsersFacade = usersFacade;
            CategoryFacade = categoryFacade;
            PostCategoryFacade = postCategoryFacade;
        }

        protected async Task<TModel> BuildIndexViewModel<TModel>(int page, int perPage, string postType, string activeTop, string activeLeft, string typeOfList)
            where TModel : ListPostViewModel, new()
        {
            const RemovedStateRequest removedState = RemovedStateRequest.Excluded;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft,
                page,
                await PostsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState, cached),
                perPage
            );

            model.Posts = await PostsFacade.GetPostsAsync(postType, page, perPage, removedState, publishState, frontPageState, cached);
            model.PageTitle.Title = $"Список {typeOfList}";

            return model;
        }

        protected async Task<TModel> BuildRemovedViewModel<TModel>(int page, int perPage, string postType, string activeTop, string activeLeft, string typeOfList)
            where TModel : ListPostViewModel, new()
        {
            const RemovedStateRequest removedState = RemovedStateRequest.OnlyRemoved;
            const PublishStateRequest publishState = PublishStateRequest.AllPublishStates;
            const FrontPageStateRequest frontPageState = FrontPageStateRequest.AllVisibilityStates;
            const bool cached = false;

            var model = await BuildAdminPageWithPaging<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft,
                page,
                await PostsFacade.GetPostPagesCountAsync(postType, perPage, removedState, publishState, frontPageState, cached),
                perPage
            );

            model.Posts = await PostsFacade.GetPostsAsync(postType, page, 5, removedState, publishState, frontPageState, cached);
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

            var categories = await CategoryFacade.GetAllCategoriesAsync(
                page: 1,
                perPage: await CategoryFacade.GetCategoriesCount(true),
                cache: true
            );

            model.Authors = GetSelectListItems(await UsersFacade.GetUsersAsync());
            model.Categories = await GetSelectListItems(categories);

            return model;
        }

        protected async Task<TModel> BuildCreateViewModel<TModel>(TModel news, string postTypeAlias, string activeTop, string activeLeft)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            model.PageTitle.Title = news.Title;

            var postType = await PostsFacade.GetPostTypeAsync(postTypeAlias);
            var categories = await CategoryFacade.GetCategoreisByIdAsync(news.SelectedCategories);

            news.Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content;
            news.PublishDate = news.Published ? DateTime.UtcNow : DateTime.MinValue;
            news.PostTypeId = postType.Id;

            news.PostSettings = new PostSetting();
            news.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<TModel, Post>(news);
            post.PostCategories = PostCategoryFacade.CreateRelation(post, categories).ToList();

            await PostsFacade.CreatePostAsync(post);

            return model;
        }

        protected async Task<TModel> BuildEditViewModel<TModel>(Guid id, string activeTop, string activeLeft, string nameForEditTitle)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var post = await PostsFacade.GetPostAsync(id);
            var categories = await CategoryFacade.GetAllCategoriesAsync(
                page: 1,
                perPage: await CategoryFacade.GetCategoriesCount(true),
                cache: true
            );

            model.Id = post.Id;
            model.Title = post.Title;
            model.Excerpt = post.Excerpt;
            model.Content = post.Content;
            model.Published = post.Published;
            model.Deleted = post.Deleted;
            model.PublishDate = post.PublishDate;
            model.AuthorId = post.AuthorId;
            model.Authors = GetSelectListItems(await UsersFacade.GetUsersAsync());
            model.PostTypeId = post.PostTypeId;
            model.PostSeoSetting = post.PostSeoSetting;
            model.PostSettings = post.PostSettings;
            model.Categories = await GetSelectListItems(categories);

            model.PageTitle.Title = $"Правка {nameForEditTitle}";

            return model;
        }

        protected async Task<TModel> BuildEditViewModel<TModel>(TModel news, string activeTop, string activeLeft)
            where TModel: PostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            news.Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content;
            news.PublishDate = news.Published ? DateTime.UtcNow : DateTime.MinValue;

            news.PostSettings = new PostSetting();
            news.PostSeoSetting = new PostSeoSetting();

            var post = _mapper.Map<TModel, Post>(news);
            var selectedCategories = await CategoryFacade.GetCategoreisByIdAsync(news.SelectedCategories);

            await PostCategoryFacade.DeletePostCategoryAsync(news.Id);
            await PostsFacade.UpdatePostAsync(post);

            var postCategories = PostCategoryFacade.CreateRelation(post, selectedCategories);
            foreach (var postCategory in postCategories)
                await PostCategoryFacade.CreatePostCategoryAsync(postCategory);

            await PostsFacade.UpdatePostAsync(post);

            return model;
        }

        protected async Task<TModel> BuildDeleteViewModel<TModel>(Guid id, string activeTop, string activeLeft)
            where TModel: ListPostViewModel, new()
        {
            var model = await BuildAdminBaseViewModelAsync<TModel>(
                link => link.Alias == activeTop,
                link => link.Alias == activeLeft
            );

            var post = await PostsFacade.GetPostAsync(id);

            post.Deleted = true;

            await PostsFacade.UpdatePostAsync(post);

            return model;
        }

        protected IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<User> users)
        {
            return users
                .Select(user => new SelectListItem
                {
                    Text = user.Person.Name + " " + user.Person.Surname,
                    Value = user.Id.ToString()
                });
        }

        protected async Task<IEnumerable<SelectListItem>> GetSelectListItems(IEnumerable<Category> categories, string postId = null)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var category in categories)
            {
                var postCategory = postId != null
                    ? await PostCategoryFacade.GetPostCategoryAsync(Guid.Parse(postId), category.Id)
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