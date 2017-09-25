using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.BasicAdmin.ViewModels.SharedModels.Common;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.News
{
    public interface INewsManagerViewModelBuilder
    {
        Task<IndexNewsViewModel> BuildIndexViewModel();
    }
    
    public class NewsManagerManagerViewModelBuilder : AdminPageBaseViewModelBuilder, INewsManagerViewModelBuilder
    {
        private readonly IPostsFacade _postsFacade;

        public NewsManagerManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPostsFacade postsFacade) : base(siteSettingsFacade)
        {
            _postsFacade = postsFacade;
        }


        public async Task<IndexNewsViewModel> BuildIndexViewModel()
        {
            var model = await BuildAdminBaseViewModelAsync<IndexNewsViewModel>(
                link => link.Alias == "News",
                link => link.Alias == "List"
            );

            await SetPostsAsync(model);
            SetTitle(model);

            return model;
        }

        private async Task SetPostsAsync(IndexNewsViewModel model)
        {
            model.Posts = await _postsFacade.GetAllNewsAsync(100, 0, false, false);
        }

        private void SetTitle(CommonAdminPageViewModel model)
        {
            model.PageTitle.Title = "Список новостей";
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список новостей", "/manager/news/index", false, "Список новостей", "List"),
                new MenuLink("Создать новость", "/manager/news/create", false, "Создать новость", "Create")
            };
        }
    }
}