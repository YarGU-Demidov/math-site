﻿using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Common.Extensions;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize(RightAliases.AdminAccess)]
    [Route("[area]/[controller]")]
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

        [Route("")]
        [Route("index")]
        [Route("list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildIndexViewModel(page, perPage));
        }

        [HttpGet("removed")]
        public async Task<IActionResult> Removed([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page, perPage));
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost("create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageViewModel page)
        {
            page.AuthorId = CurrentUser.Id;

            await _modelBuilder.BuildCreateViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _modelBuilder.BuildEditViewModel(id));
        }

        [HttpPost("edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PageViewModel page)
        {
            await _modelBuilder.BuildEditViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost("delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromQuery] Guid id, int page = 1, int perPage = 10)
        {
            await _modelBuilder.BuildDeleteViewModel(id);

            return RedirectToAction("Index", new { page, perPage });
        }

        [HttpPost("recover/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Recover(Guid id, int page = 1, int perPage = 10)
        {
            await _modelBuilder.BuildRecoverViewModel(id);
            return RedirectToAction("Removed", new { page, perPage });
        }

        [HttpPost("force-delete/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForceDelete(Guid id, int page = 1, int perPage = 10)
        {
            await _modelBuilder.BuildForceDeleteViewModel(id);
            return RedirectToAction("Removed", new { page, perPage });
        }

        [HttpPost("preview")]
        public IActionResult Preview(PageItemViewModel model)
        {
            if (model.IsNull())
                return BadRequest();

            _modelBuilder.FillPostItemViewModel(model);
            model.PageTitle.Title = model.Title;

            return View("Index", "Pages", model);
        }
    }
}