using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPageWithPaging;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common.Extensions;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Groups;
using MathSite.Facades.Persons;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;

namespace MathSite.BasicAdmin.ViewModels.Users
{
    public interface IUsersManagerViewModelBuilder
    {
        Task<IndexUsersViewModel> BuildIndexViewModelAsync(int page, int perPage);
        Task<CreateUsersViewModel> BuildCreateUserViewModelAsync();
        Task<EditUsersViewModel> BuildEditUserViewModelAsync(Guid id);

        Task UpdateUser(Guid currentUser, Guid id, EditUsersViewModel model);
        Task CreateUser(Guid currentUser, CreateUsersViewModel model);
        Task RemoveUser(Guid currentUser, Guid id);
    }

    public class UsersManagerViewModelBuilder : AdminPageWithPagingViewModelBuilder, IUsersManagerViewModelBuilder
    {
        private readonly IGroupsFacade _groupsFacade;
        private readonly IPersonsFacade _personsFacade;
        private readonly IUsersFacade _usersFacade;

        public UsersManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade,
            IUsersFacade usersFacade,
            IGroupsFacade groupsFacade,
            IPersonsFacade personsFacade
        ) : base(siteSettingsFacade)
        {
            _usersFacade = usersFacade;
            _groupsFacade = groupsFacade;
            _personsFacade = personsFacade;
        }

        public async Task<IndexUsersViewModel> BuildIndexViewModelAsync(int page, int perPage)
        {
            var model = await BuildAdminPageWithPaging<IndexUsersViewModel>(
                link => link.Alias == "Users",
                link => link.Alias == "List",
                page,
                await _usersFacade.GetUsersPagesCountAsync(perPage, false),
                perPage
            );

            model.PageTitle.Title = "Пользователи";
            model.Users = await _usersFacade.GetUsersAsync(page, perPage, false);

            return model;
        }

        public async Task<CreateUsersViewModel> BuildCreateUserViewModelAsync()
        {
            var model = await BuildAdminBaseViewModelAsync<CreateUsersViewModel>(
                link => link.Alias == "Users",
                link => link.Alias == "Create"
            );

            model.PageTitle.Title = "Создать пользователя сайта";

            await FillUserGroupsAsync(model);
            await FillPossiblePersonsAsync(model);

            return model;
        }

        public async Task<EditUsersViewModel> BuildEditUserViewModelAsync(Guid id)
        {
            var model = await BuildAdminBaseViewModelAsync<EditUsersViewModel>(
                link => link.Alias == "Users"
            );

            var user = await _usersFacade.GetUserAsync(id);

            model.PageTitle.Title = "Создать пользователя сайта";

            model.GroupId = user.GroupId.ToString();
            model.Login = user.Login;
            model.Password = "";
            model.PasswordConfimation = "";
            model.ResetPassword = false;
            model.Id = user.Id.ToString();
            model.PersonId = user.PersonId.ToString();

            await FillUserGroupsAsync(model);
            await FillPossiblePersonsAsync(model);

            return model;
        }

        public async Task UpdateUser(Guid currentUser, Guid id, EditUsersViewModel model)
        {
            if (model.ResetPassword)
                await _usersFacade.UpdateUserAsync(
                    currentUser: currentUser, 
                    id: id, 
                    personId: Guid.Parse(model.PersonId), 
                    groupId: Guid.Parse(model.GroupId),
                    newPassword: model.Password
                );
            else
                await _usersFacade.UpdateUserAsync(
                    currentUser: currentUser, 
                    id: id, 
                    personId: Guid.Parse(model.PersonId), 
                    groupId: Guid.Parse(model.GroupId)
                );
        }

        public async Task CreateUser(Guid currentUser, CreateUsersViewModel model)
        {
            await _usersFacade.CreateUserAsync(
                currentUser: currentUser, 
                personId: Guid.Parse(model.PersonId),
                login: model.Login, 
                password: model.Password,
                groupId: Guid.Parse(model.GroupId)
            );
        }

        public async Task RemoveUser(Guid currentUser, Guid id)
        {
            await _usersFacade.RemoveUser(currentUser, id);
        }

        private async Task FillUserGroupsAsync(BaseUserEditViewModel model)
        {
            var groups = await GetUserGroups();

            model.Groups = groups.Select(group => (group.Id.ToString(), group.Name));
        }

        private async Task FillPossiblePersonsAsync(BaseUserEditViewModel model)
        {
            var persons = (await _personsFacade.GetAvailablePersonsAsync()).ToList();
            
            if (model.PersonId.IsNotNullOrWhiteSpace())
            {
                var currentPerson = await _personsFacade.GetPersonAsync(Guid.Parse(model.PersonId));
                persons.Insert(0, currentPerson);
            }

            model.Persons = persons.Select(person => (person.Id.ToString(), $"{person.Surname} {person.Name} {person.MiddleName} ({person.Birthday.Date:d})"));
        }

        private async Task<IEnumerable<Group>> GetUserGroups()
        {
            return await _groupsFacade.GetGroupsByTypeAsync(GroupTypeAliases.User);
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