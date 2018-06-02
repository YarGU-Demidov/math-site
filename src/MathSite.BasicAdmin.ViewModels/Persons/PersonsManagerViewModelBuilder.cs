using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common.Extensions;
using MathSite.Entities;
using MathSite.Facades.Persons;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Persons
{
    public interface IPersonsManagerViewModelBuilder
    {
        Task<IndexPersonsViewModel> BuildIndexViewModelAsync(int page, int perPage);
        Task<CreatePersonsViewModel> BuildCreateViewModelAsync();
        Task CreatePersonAsync(BasePersonEditViewModel model);
        Task EditPersonAsync(Guid id, BasePersonEditViewModel model);
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

        public async Task CreatePersonAsync(BasePersonEditViewModel model)
        {
            var p = FillFromModel(model, new Person());
            await _personsFacade.CreatePersonAsync(p.Surname, p.Name, p.MiddleName, p.Birthday, p.Phone, p.AdditionalPhone, p.PhotoId);
        }

        public async Task EditPersonAsync(Guid id, BasePersonEditViewModel model)
        {
            var p = FillFromModel(model, new Person());
            await _personsFacade.UpdatePersonAsync(id, p.Surname, p.Name, p.MiddleName, p.Phone, p.AdditionalPhone, p.PhotoId, p.Birthday);
        }

        public async Task<EditPersonsViewModel> BuildEditViewModelAsync(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EditPersonsViewModel>(
                link => link.Alias == "Persons"
            );

            var person = await _personsFacade.GetPersonAsync(id);
            
            model.Id = person.Id;
            model.FirstName = person.Name;
            model.SecondName= person.Surname;
            model.MiddleName = person.MiddleName;
            model.BirthDate = person.Birthday;
            model.Phone = person.Phone;
            model.PhotoId = person.PhotoId?.ToString();

            return model;
        }

        public async Task DeletePersonAsync(Guid id)
        {
            await _personsFacade.DeletePersonAsync(id);
        }

        private Person FillFromModel(BasePersonEditViewModel model, Person person)
        {
            person.Name = model.FirstName;
            person.Surname = model.SecondName;
            person.MiddleName = model.MiddleName;
            person.Birthday = model.BirthDate ?? DateTime.Today;
            person.Phone = model.Phone;
            
            person.PhotoId = model.PhotoId.IsNotNullOrWhiteSpace() 
                ? Guid.Parse(model.PhotoId) 
                : default(Guid?);

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