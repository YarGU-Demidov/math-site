using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Facades.Professors;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Professors
{
    public interface IProfessorViewModelBuilder
    {
        Task<ProfessorListViewModel> BuildListViewModel(int page, int perPage);
    }

    public class ProfessorViewModelBuilder : AdminPageWithPagingViewModelBuilder, IProfessorViewModelBuilder
    {
        private readonly IProfessorsFacade _professorsFacade;

        public ProfessorViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IProfessorsFacade professorsFacade
        ) : base(siteSettingsFacade)
        {
            _professorsFacade = professorsFacade;
        }

        public async Task<ProfessorListViewModel> BuildListViewModel(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<ProfessorListViewModel>(
                link => link.Alias == ProfessorsTopMenuName,
                link => false,
                page,
                await _professorsFacade.GetPagesCountAsync(perPage),
                perPage
            );

            model.PageTitle.Title = "Преподаватели";
            model.Professors = await _professorsFacade.GetProfessorsForPage(page, perPage);

            return model;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return Enumerable.Empty<MenuLink>();
        }
    }
}