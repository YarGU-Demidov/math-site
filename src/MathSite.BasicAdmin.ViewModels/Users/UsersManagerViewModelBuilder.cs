using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;

namespace MathSite.BasicAdmin.ViewModels.Users
{
    public interface IUsersManagerViewModelBuilder
    {
        Task<IndexUsersViewModel> BuildIndexViewModelAsync(int page, int perPage);
    }

    public class UsersManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IUsersManagerViewModelBuilder
    {
        private readonly IUsersFacade _usersFacade;

        public UsersManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IUsersFacade usersFacade) : base(
            siteSettingsFacade)
        {
            _usersFacade = usersFacade;
        }

        public async Task<IndexUsersViewModel> BuildIndexViewModelAsync(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<IndexUsersViewModel>(
                link => link.Alias == "Users",
                link => link.Alias == "List",
                page,
                await _usersFacade.GetUsersCountAsync(perPage, false),
                perPage
            );

            model.Users = await _usersFacade.GetUsersAsync(page, perPage, false);

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список пользователей", "/manager/users/list", false, "Список пользователей", "List"),
                new MenuLink("Создать пользователя", "/manager/users/create", false, "Создать пользователя", "Create")
            };
        }
    }
}