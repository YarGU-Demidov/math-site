using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.EventPreview;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.Home.StudentActivityPreview;

namespace MathSite.ViewModels.Home
{
    public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
    {
        private readonly IEventPreviewViewModelBuilder _eventPreviewViewModelBuilder;
        private readonly IStudentActivityPreviewViewModelBuilder _activityPreviewViewModelBuilder;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IPostPreviewViewModelBuilder _postPreviewViewModelBuilder;
        private readonly IPostsFacade _postsFacade;

        public HomeViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder,
            IEventPreviewViewModelBuilder eventPreviewViewModelBuilder,
            IStudentActivityPreviewViewModelBuilder activityPreviewViewModelBuilder,
            ICategoryFacade categoryFacade
        )
            : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _postPreviewViewModelBuilder = postPreviewViewModelBuilder;
            _eventPreviewViewModelBuilder = eventPreviewViewModelBuilder;
            _activityPreviewViewModelBuilder = activityPreviewViewModelBuilder;
            _categoryFacade = categoryFacade;
        }

        public async Task<HomeIndexViewModel> BuildIndexModel()
        {
            var model = await BuildCommonViewModelAsync<HomeIndexViewModel>();
            await FillPageNameAsync(model);
            await BuildPostsAsync(model);
            return model;
        }

        public string GenerateSiteMap()
        {
            // TODO: Write sitemap generator
            throw new System.NotImplementedException();
        }

        private async Task BuildPostsAsync(HomeIndexViewModel model)
        {
            var category = await _categoryFacade.GetCategoryByAliasAsync("students-activities");

            var posts = await _postsFacade.GetPostsAsync(
                postTypeAlias: PostTypeAliases.News,
                page: 1,
                perPage: 6,
                state: RemovedStateRequest.Excluded,
                publishState: PublishStateRequest.Published,
                frontPageState: FrontPageStateRequest.Visible,
                excludedCategories: new[] {category},
                cache: true
            );

            var events = await _postsFacade.GetPostsAsync(
                postTypeAlias: PostTypeAliases.Event,
                page: 1,
                perPage: 3,
                state: RemovedStateRequest.Excluded,
                publishState: PublishStateRequest.Published,
                frontPageState: FrontPageStateRequest.Visible,
                cache: true
            );

            IEnumerable<Post> studentActivities = new List<Post>();
            
            if (category.IsNotNull())
            {
                studentActivities = await _postsFacade.GetPostsAsync(
                    categoryId: category.Id,
                    postTypeAlias: PostTypeAliases.News,
                    page: 1,
                    perPage: 4,
                    state: RemovedStateRequest.Excluded,
                    publishState: PublishStateRequest.Published,
                    frontPageState: FrontPageStateRequest.Visible,
                    cache: true
                );
            }

            model.Posts = GetPostsModels(posts);
            model.Events = GetEventsModels(events);
            model.StudentsActivities = GetStudentActivityModels(studentActivities);
        }

        private IEnumerable<EventPreviewViewModel> GetEventsModels(IEnumerable<Post> events)
        {
            return events.Select(_eventPreviewViewModelBuilder.Build);
        }

        private IEnumerable<PostPreviewViewModel> GetPostsModels(IEnumerable<Post> posts)
        {
            return posts.Select(_postPreviewViewModelBuilder.Build);
        }
        private IEnumerable<StudentActivityViewModel> GetStudentActivityModels(IEnumerable<Post> posts)
        {
            return posts.Select(_activityPreviewViewModelBuilder.Build);
        }

        private async Task FillPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade.GetDefaultHomePageTitle();

            model.PageTitle.Title = title ?? "Главная страница";
        }
    }
}