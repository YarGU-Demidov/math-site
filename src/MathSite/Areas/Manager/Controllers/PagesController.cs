using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Controllers;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    public class PagesController : BaseController
    {
        private readonly IPagesManagerViewModelBuilder _modelBuilder;

        public PagesController(IUserValidationFacade userValidationFacade, IPagesManagerViewModelBuilder modelBuilder) : base(userValidationFacade)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("manager/pages/")]
        [Route("manager/pages/index")]
        [Route("manager/pages/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 5)
        {
            return View(await _modelBuilder.BuildIndexViewModel(page, perPage));
        }

        public async Task<IActionResult> Removed([FromQuery] int page = 1, [FromQuery] int perPage = 5)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page, perPage));
        }

        public async Task<IActionResult> Create(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}