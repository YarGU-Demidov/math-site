using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize(RightAliases.AdminAccess)]
    public class PagesController : BaseController
    {
        private readonly IPagesManagerViewModelBuilder _modelBuilder;

        public PagesController(
            IUserValidationFacade userValidationFacade, 
            IPagesManagerViewModelBuilder modelBuilder,
            IUsersFacade usersFacade
        ) : base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("[area]/[controller]/")]
        [Route("[area]/[controller]/index")]
        [Route("[area]/[controller]/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildIndexViewModel(page, perPage));
        }

        [HttpGet("[area]/[controller]/removed")]
        public async Task<IActionResult> Removed([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page, perPage));
        }

        [HttpGet("[area]/[controller]/create")]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost("[area]/[controller]/create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel page)
        {
            page.AuthorId = CurrentUser.Id;

            await _modelBuilder.BuildCreateViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet("[area]/[controller]/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _modelBuilder.BuildEditViewModel(id));
        }

        [HttpPost("[area]/[controller]/edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PageViewModel page)
        {
            await _modelBuilder.BuildEditViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost("[area]/[controller]/delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _modelBuilder.BuildDeleteViewModel(id);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost("[area]/[controller]/recover/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Recover(Guid id)
        {
            await _modelBuilder.BuildRecoverViewModel(id);
            return RedirectToActionPermanent("Removed");
        }


        [HttpPost("[area]/[controller]/forece-delete/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForceDelete(Guid id)
        {
            await _modelBuilder.BuildForceDeleteViewModel(id);
            return RedirectToActionPermanent("Removed");
        }
    }
}