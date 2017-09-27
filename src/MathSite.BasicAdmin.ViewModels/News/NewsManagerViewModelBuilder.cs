using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel(int page);
        Task<IndexNewsViewModel> BuildRemovedViewModel(int page);
    }

    public class NewsManagerManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;

        public NewsManagerManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade) :
            base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
        }

        public async Task<IndexNewsViewModel> BuildIndexViewModel(int page)
        {
            var model = await BuildAdminPageWithPaging<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "List",
                page,
                await _postsFacade.GetNewsPagesCountAsync()
            );

            model.Posts = await _postsFacade.GetAllNewsAsync(page, 5);
            model.PageTitle.Title = "Список новостей";

            return model;
        }

        public async Task<IndexNewsViewModel> BuildRemovedViewModel(int page)
        {
            var model = await BuildAdminPageWithPaging<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "ListRemoved",
                page,
                await _postsFacade.GetNewsPagesCountAsync()
            );

            model.Posts = await _postsFacade.GetAllNewsAsync(page, 5, false, true);
            model.PageTitle.Title = "Список удаленных новостей";

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список новостей", "/manager/news/list", false, "Список новостей", "List"),
                new MenuLink("Список удаленных новостей", "/manager/news/removed", false, "Список удаленных новостей",
                    "ListRemoved"),
                new MenuLink("Создать новость", "/manager/news/create", false, "Создать новость", "Create")
            };
        }
    }
}