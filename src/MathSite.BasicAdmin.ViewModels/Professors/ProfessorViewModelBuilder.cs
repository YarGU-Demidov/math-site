using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Entities;
using MathSite.Facades.Persons;
using MathSite.Facades.Professors;
using MathSite.Facades.SiteSettings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MathSite.BasicAdmin.ViewModels.Professors
{
    public interface IProfessorViewModelBuilder
    {
        Task<ProfessorListViewModel> BuildListViewModel(int page, int perPage);
        Task<CreateProfessorViewModel> BuildCreateViewModelAsync();
        Task<EditProfessorViewModel> BuildEditViewModelAsync(Guid id);
        Task CreateProfessorAsync(CreateProfessorViewModel model);
        Task EditProfessorAsync(EditProfessorViewModel model);
        Task DeleteProfessorAsync(Guid id);
    }

    public class ProfessorViewModelBuilder : AdminPageWithPagingViewModelBuilder, IProfessorViewModelBuilder
    {
        private readonly IProfessorsFacade _professorsFacade;
        private readonly IPersonsFacade _personsFacade;

        public ProfessorViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IProfessorsFacade professorsFacade,
            IPersonsFacade personsFacade
        ) : base(siteSettingsFacade)
        {
            _professorsFacade = professorsFacade;
            _personsFacade = personsFacade;
        }

        public async Task<ProfessorListViewModel> BuildListViewModel(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<ProfessorListViewModel>(
                link => link.Alias == ProfessorsTopMenuName,
                link => link.Alias == "List",
                page,
                await _professorsFacade.GetPagesCountAsync(perPage),
                perPage
            );

            model.PageTitle.Title = "Преподаватели";
            model.Professors = await _professorsFacade.GetProfessorsForPage(page, perPage);

            return model;
        }

        public async Task<CreateProfessorViewModel> BuildCreateViewModelAsync()
        {
            var model = await BuildAdminBaseViewModelAsync<CreateProfessorViewModel>(
                link => link.Alias == ProfessorsTopMenuName,
                link => link.Alias == "Create"
            );

            model.PageTitle.Title = "Создать преподавателя";
            
            model.AvailablePersons = await GetAvailablePersonsAsync();

            return model;
        }

        public async Task<EditProfessorViewModel> BuildEditViewModelAsync(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EditProfessorViewModel>(
                link => link.Alias == ProfessorsTopMenuName,
                link => false
            );

            model.PageTitle.Title = "Править преподавателя";

            var professor = await _professorsFacade.GetProfessorAsync(id);

            model.PersonId = professor.PersonId;
            model.BibliographicIndexOfWorks = professor.BibliographicIndexOfWorks;
            model.Department = professor.Department;
            model.Description = professor.Description;
            model.Faculty = professor.Faculty;
            model.Graduated = professor.Graduated;
            model.MathNetLink = professor.MathNetLink;
            model.ScientificTitle = professor.ScientificTitle;
            model.Status = professor.Status;
            model.TermPapers = professor.TermPapers;
            model.Theses = professor.Theses;
            model.Id = professor.Id;

            model.AvailablePersons = await GetAvailablePersonsAsync(professor.PersonId);

            return model;
        }

        public async Task CreateProfessorAsync(CreateProfessorViewModel model)
        {
            var professor = new Professor
            {
                PersonId = model.PersonId,
                BibliographicIndexOfWorks = model.BibliographicIndexOfWorks,
                Department = model.Department,
                Description = model.Description,
                Faculty = model.Faculty,
                Graduated = model.Graduated,
                MathNetLink = model.MathNetLink,
                ScientificTitle = model.ScientificTitle,
                Status = model.Status,
                TermPapers = model.TermPapers,
                Theses = model.Theses
            };

            await _professorsFacade.CreateAsync(professor);
        }

        public async Task EditProfessorAsync(EditProfessorViewModel model)
        {
            var professor = await _professorsFacade.GetProfessorAsync(model.Id);

            professor.PersonId = model.PersonId;
            professor.BibliographicIndexOfWorks = model.BibliographicIndexOfWorks;
            professor.Department = model.Department;
            professor.Description = model.Description;
            professor.Faculty = model.Faculty;
            professor.Graduated = model.Graduated;
            professor.MathNetLink = model.MathNetLink;
            professor.ScientificTitle = model.ScientificTitle;
            professor.Status = model.Status;
            professor.TermPapers = model.TermPapers;
            professor.Theses = model.Theses;

            await _professorsFacade.UpdateAsync(professor);
        }

        public async Task DeleteProfessorAsync(Guid id)
        {
            await _professorsFacade.DeleteAsync(id);
        }

        private async Task<IEnumerable<SelectListItem>> GetAvailablePersonsAsync(Guid? currentId = null)
        {
            string GetText(Person p)
            {
                return $"{p.Surname} {p.Name} {p.MiddleName} ({p.Birthday.ToString("d", CultureInfo.GetCultureInfo("ru-RU"))})";
            }

            var availablePersons = await _personsFacade.GetPersonsWithoutProfessorsAsync();

            var persons = availablePersons.Select(person => new SelectListItem
            {
                Text = GetText(person),
                Value = person.Id.ToString(),
                Selected = false,
                Disabled = false
            }).ToList();

            if (currentId.HasValue)
            {
                var currentPerson = await _personsFacade.GetPersonAsync(currentId.Value);

                persons.Add(new SelectListItem
                {
                    Text = GetText(currentPerson),
                    Disabled = false,
                    Selected = true,
                    Value = currentId.ToString()
                });
            }

            return persons;
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>
            {
                new MenuLink("Список преподавателей", "/manager/professor/list", false, "Список преподавателей", "List"),
                new MenuLink("Создать преподавателя", "/manager/professor/create", false, "Создать преподавателя", "Create")
            };
        }
    }
}