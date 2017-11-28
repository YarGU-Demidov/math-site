using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Users;
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

        [Route("manager/users/")]
        [Route("manager/users/index")]
        [Route("manager/users/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View(await _viewModelBuilder.BuildIndexViewModelAsync(page, perPage));
        }

        public async Task<IActionResult> Create(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}