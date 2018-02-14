using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.Pages;
using MathSite.Controllers;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades.Users;
using MathSite.Entities;
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

        [HttpGet("[area]/[controller]/create")]
        public async Task<IActionResult> Create()
        {
            return View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost("[area]/[controller]/create")]
        public async Task<IActionResult> Create(CreatePageViewModel page)
        {
            var postType = new PostType
            {
                Alias = PostTypeAliases.StaticPage
            };
            
            var postExcerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content;

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = page.Title,
                Excerpt = postExcerpt,
                Content = page.Content,
                Author = CurrentUser,
                Published = false,
                Deleted = false,
                PostType = postType,
                PostSettings = new PostSetting(),
                PostSeoSetting = new PostSeoSetting()
            };

            await _modelBuilder.BuildCreateViewModel(post);

            return RedirectToActionPermanent("Index");
        }

        [HttpDelete("{id}")]
        [Route("manager/pages/delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _modelBuilder.BuildDeleteViewModel(id);

            return RedirectToActionPermanent("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Recover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}