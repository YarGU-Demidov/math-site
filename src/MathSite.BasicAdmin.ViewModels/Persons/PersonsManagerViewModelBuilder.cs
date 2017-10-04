using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.Persons;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Persons
{
    public interface IPersonsManagerViewModelBuilder
    {
        Task<IndexPersonsViewModel> BuildIndexViewModelAsync(int page, int perPage);
    }

    public class PersonsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IPersonsManagerViewModelBuilder
    {
        private readonly IPersonsFacade _personsFacade;

        public PersonsManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IPersonsFacade personsFacade) : base(siteSettingsFacade)
        {
            _personsFacade = personsFacade;
        }

        public async Task<IndexPersonsViewModel> BuildIndexViewModelAsync(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<IndexPersonsViewModel>(
                link => link.Alias == "Persons",
                link => link.Alias == "List",
                page,
                await _personsFacade.GetPersonsCountAsync(perPage, false),
                perPage
            );

            model.Persons = await _personsFacade.GetPersonsAsync(page, perPage, false);

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список лиц", "/manager/persons/list", false, "Список лиц", "List"),
                new MenuLink("Создать лицо", "/manager/persons/create", false, "Создать лицо", "Create")
            };
        }
    }
}