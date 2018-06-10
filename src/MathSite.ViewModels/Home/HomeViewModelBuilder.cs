using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Categories;
using MathSite.Facades.Posts;
using MathSite.Facades.Professors;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.EventPreview;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.Home.StudentActivityPreview;
using SimpleMvcSitemap;
using SimpleMvcSitemap.Images;
using SimpleMvcSitemap.News;

namespace MathSite.ViewModels.Home
{
    public class HomeViewModelBuilder : CommonViewModelBuilder, IHomeViewModelBuilder
    {
        private readonly IEventPreviewViewModelBuilder _eventPreviewViewModelBuilder;
        private readonly IStudentActivityPreviewViewModelBuilder _activityPreviewViewModelBuilder;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IProfessorsFacade _professorsFacade;
        private readonly IPostPreviewViewModelBuilder _postPreviewViewModelBuilder;
        private readonly IPostsFacade _postsFacade;

        public HomeViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IPostsFacade postsFacade,
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder,
            IEventPreviewViewModelBuilder eventPreviewViewModelBuilder,
            IStudentActivityPreviewViewModelBuilder activityPreviewViewModelBuilder,
            ICategoryFacade categoryFacade,
            IProfessorsFacade professorsFacade
        ) : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
            _postPreviewViewModelBuilder = postPreviewViewModelBuilder;
            _eventPreviewViewModelBuilder = eventPreviewViewModelBuilder;
            _activityPreviewViewModelBuilder = activityPreviewViewModelBuilder;
            _categoryFacade = categoryFacade;
            _professorsFacade = professorsFacade;
        }

        public async Task<HomeIndexViewModel> BuildIndexModel()
        {
            var model = await BuildCommonViewModelAsync<HomeIndexViewModel>();
            await FillPageNameAsync(model);
            await BuildPostsAsync(model);
            return model;
        }

        public async Task<SitemapModel> GenerateSiteMap()
        {
            // ну предположим, что постов у нас не больше 50 000 (а больше и нельзя в sitemap).

            const string newsTypeAlias = PostTypeAliases.News;
            const string pagesTypeAlias = PostTypeAliases.StaticPage;
            const string eventsTypeAlias = PostTypeAliases.Event;

            Task<IEnumerable<Post>> GetData(string postTypeAlias)
            {
                return _postsFacade.GetPostsAsync(
                    postTypeAlias: postTypeAlias,
                    page: 1,
                    perPage: 50_000,
                    state: RemovedStateRequest.Excluded,
                    publishState: PublishStateRequest.Published,
                    frontPageState: FrontPageStateRequest.AllVisibilityStates
                );
            }

            var pages = (await GetData(pagesTypeAlias)).ToArray();
            var news = (await GetData(newsTypeAlias)).ToArray();
            var events = (await GetData(eventsTypeAlias)).ToArray();

            IEnumerable<SitemapNode> GetSitemapNodes(IEnumerable<Post> posts) => posts.Select(post =>
            {
                var postType = post.PostType.Alias;
                var previewModel = _postPreviewViewModelBuilder.Build(post);

                return new SitemapNode(previewModel.Url)
                {
                    ChangeFrequency = postType == PostTypeAliases.StaticPage ? ChangeFrequency.Weekly : ChangeFrequency.Always,
                    Images = GetImages(previewModel.PreviewImageId, previewModel.PreviewImageId2X),
                    LastModificationDate = post.PublishDate.ToUniversalTime(),
                    News = GetNews(post),
                    Priority = 0.5M
                };
            });

            var allNodes = new List<SitemapNode>(pages.Length + news.Length + events.Length + 3)
            {
                new SitemapNode("/")
                {
                    ChangeFrequency = ChangeFrequency.Always,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.7M
                },
                new SitemapNode("/news")
                {
                    ChangeFrequency = ChangeFrequency.Always,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M
                },
                new SitemapNode("/events")
                {
                    ChangeFrequency = ChangeFrequency.Always,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M
                }
            };

            allNodes.AddRange(GetSitemapNodes(events));
            allNodes.AddRange(GetSitemapNodes(news));
            allNodes.AddRange(GetSitemapNodes(pages));

            var professors = await _professorsFacade.GetProfessorsForPage(1, 50_000);

            allNodes.AddRange(professors.Select(professor => new SitemapNode($"/professors/show/{professor.Id}")
            {
                ChangeFrequency = ChangeFrequency.Monthly,
                Images = GetImages(professor.Person?.PhotoId?.ToString()),
                Priority = 0.5M
            }));

            return new SitemapModel(allNodes);
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
                excludedCategories: category.IsNotNull() ? new[] {category} : new Category[0]
            );

            var events = await _postsFacade.GetPostsAsync(
                postTypeAlias: PostTypeAliases.Event,
                page: 1,
                perPage: 3,
                state: RemovedStateRequest.Excluded,
                publishState: PublishStateRequest.Published,
                frontPageState: FrontPageStateRequest.Visible
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
                    frontPageState: FrontPageStateRequest.Visible
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

        private static List<SitemapImage> GetImages(params string[] images)
        {
            return images
                .Where(imgId => imgId.IsNotNullOrWhiteSpace())
                .Distinct()
                .Select(imgId => new SitemapImage($"/file/get/{imgId}"))
                .ToList();
        }

        private SitemapNews GetNews(Post post)
        {
            return new SitemapNews(new NewsPublication(post.Title, "ru"), post.PublishDate.ToUniversalTime(), post.Title);
        }

        private async Task FillPageNameAsync(CommonViewModel model)
        {
            var title = await SiteSettingsFacade.GetDefaultHomePageTitle();

            model.PageTitle.Title = title ?? "Главная страница";
        }
    }
}