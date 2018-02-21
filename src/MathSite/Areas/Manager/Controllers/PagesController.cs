using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Entities;
using MathSite.Entities.Dtos;
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

        public PagesController(IUserValidationFacade userValidationFacade, IPagesManagerViewModelBuilder modelBuilder,
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
        public async Task<IActionResult> Create(PageViewModel page)
        {
            var postExcerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content;
            var post = new PostDto
            {
                Id = Guid.NewGuid(),
                Title = page.Title,
                Excerpt = postExcerpt,
                Content = page.Content,
                AuthorId = CurrentUser.Id,
                Published = false,
                Deleted = false,
                PostSettings = new PostSetting(),
                PostSeoSetting = new PostSeoSetting(),
                PostCategories = new List<PostCategory>()
            };

            await _modelBuilder.BuildCreateViewModel(post);

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
        public async Task<IActionResult> Edit(PageViewModel page)
        {
            var post = new PostDto
            {
                Id = page.Id,
                Title = page.Title,
                Excerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content,
                Content = page.Content,
                Published = page.Published,
                Deleted = page.Deleted,
                PublishDate = page.PublishDate,
                AuthorId = page.AuthorId,
                PostTypeId = page.PostTypeId,
                PostSettingsId = page.PostSettingsId,
                PostSeoSettingsId = page.PostSeoSettingsId,
                PostCategories = page.PostCategories
            };

            await _modelBuilder.BuildEditViewModel(post);

            return RedirectToActionPermanent("Index");
        }

        [HttpDelete("{id}")]
        [Route("[area]/[controller]/delete")]
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