using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Entities;
using MathSite.Facades.Persons;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Persons
{
    public interface IPersonsManagerViewModelBuilder
    {
        Task<IndexPersonsViewModel> BuildIndexViewModelAsync(int page, int perPage);
        Task<CreatePersonsViewModel> BuildCreateViewModelAsync();
        Task CreatePersonAsync(PersonEditViewModel model);
        Task EditPersonAsync(Guid id, PersonEditViewModel model);
        Task<EditPersonsViewModel> BuildEditViewModelAsync(Guid id);
        Task DeletePersonAsync(Guid id);
    }

    public class PersonsManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IPersonsManagerViewModelBuilder
    {
        private readonly IPersonsFacade _personsFacade;

        public PersonsManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IPersonsFacade personsFacade
        ) : base(siteSettingsFacade)
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

        public async Task<CreatePersonsViewModel> BuildCreateViewModelAsync()
        {
            var model = await BuildAdminBaseViewModelAsync<CreatePersonsViewModel>(
                link => link.Alias == "Persons",
                link => link.Alias == "Create"
            );

            return model;
        }

        public async Task CreatePersonAsync(PersonEditViewModel model)
        {
            var person = FillFromModel(model, new Person());
            await _personsFacade.CreatePersonAsync(person);
        }

        public async Task EditPersonAsync(Guid id, PersonEditViewModel model)
        {
            var person = await _personsFacade.GetPersonAsync(id);
            var personEntity = FillFromModel(model, person);
            await _personsFacade.UpdatePersonAsync(personEntity);
        }

        public async Task<EditPersonsViewModel> BuildEditViewModelAsync(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EditPersonsViewModel>(
                link => link.Alias == "Persons",
                link => link.Alias == "Create"
            );

            var person = await _personsFacade.GetPersonAsync(id);
            
            model.Id = person.Id;
            model.FirstName = person.Name;
            model.SecondName= person.Surname;
            model.MiddleName = person.MiddleName;
            model.BirthDate = person.Birthday;
            model.Phone = person.Phone;

            return model;
        }

        public async Task DeletePersonAsync(Guid id)
        {
            await _personsFacade.DeletePersonAsync(await _personsFacade.GetPersonAsync(id));
        }

        private Person FillFromModel(PersonEditViewModel model, Person person)
        {
            person.Name = model.FirstName;
            person.Surname = model.SecondName;
            person.MiddleName = model.MiddleName;
            person.Birthday = model.BirthDate ?? DateTime.Today;
            person.Phone = model.Phone;

            return person;
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