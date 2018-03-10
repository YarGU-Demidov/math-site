﻿using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Categories;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Area("manager")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesViewModelBuilder _modelBuilder;

        public CategoriesController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade,
            ICategoriesViewModelBuilder modelBuilder
        ) : base(userValidationFacade, usersFacade)
        {
            _modelBuilder = modelBuilder;
        }

        [Route("[area]/[controller]/")]
        [Route("[area]/[controller]/index")]
        [Route("[area]/[controller]/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildIndexViewModelAsync(page, perPage));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModelAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoriesViewModel model)
        {
            if (!CurrentUserId.HasValue)
                return Forbid(CookieAuthenticationDefaults.AuthenticationScheme);

            await _modelBuilder.CreateCategoryAsync(CurrentUserId.Value, model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _modelBuilder.BuildEditViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoriesViewModel model)
        {
            if (!CurrentUserId.HasValue)
                return Forbid(CookieAuthenticationDefaults.AuthenticationScheme);

            await _modelBuilder.EditCategoryAsync(CurrentUserId.Value, model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!CurrentUserId.HasValue)
                return Forbid(CookieAuthenticationDefaults.AuthenticationScheme);

            await _modelBuilder.DeleteCategoryAsync(CurrentUserId.Value, id);

            return RedirectToAction("Index");
        }
    }
}