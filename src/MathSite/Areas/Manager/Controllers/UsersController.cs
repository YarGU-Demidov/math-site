using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Users;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{

    [Area("manager")]
    [Authorize("admin")]
    public class UsersController : BaseController
    {
        private readonly IUsersManagerViewModelBuilder _viewModelBuilder;

        public UsersController(IUserValidationFacade userValidationFacade, IUsersManagerViewModelBuilder viewModelBuilder, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("[area]/[controller]/")]
        [Route("[area]/[controller]/index")]
        [Route("[area]/[controller]/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View(await _viewModelBuilder.BuildIndexViewModelAsync(page, perPage));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _viewModelBuilder.BuildCreateUserViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUsersViewModel model)
        {
            if (!TryValidateModel(model))
                return View("Create", model);

            await _viewModelBuilder.CreateUser(CurrentUser.Id, model);
            
            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _viewModelBuilder.BuildEditUserViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditUsersViewModel model)
        {
            var passwordsAreNotEqual = model.Password != model.PasswordConfimation;
            var passEmpty = model.Password.IsNullOrWhiteSpace();

            if (model.ResetPassword && (passwordsAreNotEqual || passEmpty))
                return View("Edit", model);

            await _viewModelBuilder.UpdateUser(CurrentUser.Id, id, model);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _viewModelBuilder.RemoveUser(CurrentUser.Id, id);
            return RedirectToActionPermanent("Index");
        }
    }
}