using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.News
{
    public class NewsViewModelBuilder : SecondaryViewModelBuilder, INewsViewModelBuilder
    {
        private readonly ICategoryFacade _categoryFacade;

        public NewsViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder,
            ICategoryFacade categoryFacade
        )
            : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
        {
            _categoryFacade = categoryFacade;
        }

        public async Task<NewsIndexViewModel> BuildIndexViewModelAsync(int page = 1)
        {
            var model = await BuildSecondaryViewModel<NewsIndexViewModel>();
            await FillIndexPageNameAsync(model);

            await BuildPosts(model, page);

            model.Paginator = await GetPaginator(page);

            return model;
        }

        public async Task<NewsByCategoryViewModel> BuildByCategoryViewModelAsync(string categoryQuery, int page)
        {
            var model = await BuildSecondaryViewModel<NewsByCategoryViewModel>();
            await FillIndexPageNameAsync(model);

            var category = await GetCategoryAsync(categoryQuery);

            await BuildPosts(model, page, category, true);

            model.Paginator = await GetPaginator(page, category);

            model.CategoryAlias = category?.Alias;
            model.CategoryName = category?.Name;

            return model;
        }

        public async Task<NewsItemViewModel> BuildNewsItemViewModelAsync(Guid currentUserId, string query, int page = 1)
        {
            var model = await BuildSecondaryViewModel<NewsItemViewModel>();

            var post = await BuildPostData(currentUserId, query, page);

            if (post == null)
                throw new PostNotFoundException();

            model.Content = post.Content;
            model.Title = post.Title;
            model.PreviewImageId = post.PostSettings.PreviewImage?.Id.ToString();
            model.PreviewImage2XId = post.PostSettings.PreviewImage?.Id.ToString();
            model.PageTitle.Title = post.Title;

            return model;
        }

        private async Task<PaginatorViewModel> GetPaginator(int page, Category category = null)
        {
            var newsPostType = PostTypeAliases.News;

            var postCount = await PostsFacade.GetPostPagesCountAsync(
                category?.Id,
                newsPostType,
                await SiteSettingsFacade.GetPerPageCountAsync(),
                RemovedStateRequest.Excluded,
                PublishStateRequest.Published, 
                FrontPageStateRequest.AllVisibilityStates, 
                cache: true
            );

            return new PaginatorViewModel
            {
                CurrentPage = page,
                PagesCount = postCount,
                Controller = "News"
            };
        }


        private async Task FillIndexPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade.GetDefaultNewsPageTitle();

            model.PageTitle.Title = title ?? "Новости нашего факультета";
        }

        private async Task BuildPosts(NewsIndexViewModel model, int page, Category category = null, bool forceCategory = false)
        {
            const string postType = PostTypeAliases.News;
            const bool cache = true;

            

            if (category.IsNull() && forceCategory)
            {
                throw new CategoryDoesNotExists();
            }

            var posts = await PostsFacade.GetPostsAsync(
                category?.Id,
                postType,
                page,
                await SiteSettingsFacade.GetPerPageCountAsync(cache),
                RemovedStateRequest.Excluded,
                PublishStateRequest.Published,
                FrontPageStateRequest.AllVisibilityStates,
                cache: cache
            );

            model.Posts = GetPosts(posts);
        }

        private async Task<Category> GetCategoryAsync(string categoryAlias)
        {
            return await _categoryFacade.GetByAliasAsync(categoryAlias);
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