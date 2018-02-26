using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Dtos;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.Controllers;
using MathSite.Entities;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Area("manager")]
    [Authorize("admin")]
    public class NewsController : BaseController
    {
        private readonly INewsManagerViewModelBuilder _modelBuilder;

        public NewsController(IUserValidationFacade userValidationFacade, INewsManagerViewModelBuilder modelBuilder,
            IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
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

        public async Task<IActionResult> Removed([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page, perPage));
        }

        [HttpGet]
        [Route("[area]/[controller]/create")]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost]
        [Route("[area]/[controller]/create")]
        public async Task<IActionResult> Create(NewsViewModel page)
        {
            page.AuthorId = CurrentUser.Id;

            await _modelBuilder.BuildCreateViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        [Route("[area]/[controller]/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return View("Edit", await _modelBuilder.BuildEditViewModel(id));
        }

        [HttpPost]
        [Route("[area]/[controller]/edit")]
        public async Task<IActionResult> Edit(NewsViewModel page)
        {
            page.Excerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content;

            await _modelBuilder.BuildEditViewModel(page);

            return RedirectToActionPermanent("Index");
        }

        [HttpDelete("{id}")]
        [Route("manager/news/delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _modelBuilder.BuildDeleteViewModel(id);

            return RedirectToActionPermanent("Index");
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}