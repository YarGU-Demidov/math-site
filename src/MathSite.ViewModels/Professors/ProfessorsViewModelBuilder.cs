using System;
using System.Threading.Tasks;
using MathSite.Common.Exceptions;
using MathSite.Common.Extensions;
using MathSite.Facades.Posts;
using MathSite.Facades.Professors;
using MathSite.Facades.SiteSettings;
using MathSite.ViewModels.Home.PostPreview;
using MathSite.ViewModels.SharedModels.SecondaryPage;

namespace MathSite.ViewModels.Professors
{
    public interface IProfessorsViewModelBuilder
    {
        Task<ProfessorViewModel> BuildShowViewModelAsync(Guid id);
    }

    public class ProfessorsViewModelBuilder : SecondaryViewModelBuilder, IProfessorsViewModelBuilder
    {
        private readonly IProfessorsFacade _professorsFacade;

        public ProfessorsViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IPostsFacade postsFacade, 
            IPostPreviewViewModelBuilder postPreviewViewModelBuilder,
            IProfessorsFacade professorsFacade
        ) : base(siteSettingsFacade, postsFacade, postPreviewViewModelBuilder)
        {
            _professorsFacade = professorsFacade;
        }


        public async Task<ProfessorViewModel> BuildShowViewModelAsync(Guid id)
        {
            var professor = await _professorsFacade.GetProfessorAsync(id);

            if(professor.IsNull())
                throw new EntityNotFoundException();

            var model = await BuildSecondaryViewModel<ProfessorViewModel>();

            model.PageTitle.Title = $"{professor.Person.Surname} {professor.Person.Name} {professor.Person.MiddleName}";
            model.Professor = professor;

            return model;
        }
    }
}