using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("manager/pages/edit")]
        public async Task<IActionResult> Create(Guid id)
        {
            return id != Guid.Empty
                ? View("Create", await _modelBuilder.BuildEditViewModel(id))
                : View("Create", await _modelBuilder.BuildCreateViewModel());
        }

        [HttpPost("[area]/[controller]/create")]
        [Route("manager/pages/edit")]
        public async Task<IActionResult> Create(PageViewModel page)
        {
            if (page.Id != Guid.Empty)
            {
                var post = new Post
                {
                    Id = page.Id,
                    Title = page.Title,
                    Excerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content,
                    Content = page.Content,
                    Published = page.Published,
                    Deleted = page.Deleted,
                    PublishDate = page.PublishDate,
                    Author = page.CurrentAuthor,
                    PostType = page.PostType,
                    PostSettings = page.PostSettings,
                    PostSeoSetting = page.PostSeoSetting,
                    PostCategories = page.PostCategories?.ToList()
                };
                await _modelBuilder.BuildEditViewModel(post);
            }
            else
            {
                var postType = new PostType
                {
                    Alias = PostTypeAliases.StaticPage
                };

                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Title = page.Title,
                    Excerpt = page.Content.Length > 50 ? $"{page.Content.Substring(0, 47)}..." : page.Content,
                    Content = page.Content,
                    Author = CurrentUser,
                    Published = false,
                    Deleted = false,
                    PostType = postType,
                    PostSettings = new PostSetting(),
                    PostSeoSetting = new PostSeoSetting(),
                    PostCategories = new List<PostCategory>()
                };

                await _modelBuilder.BuildCreateViewModel(post);
            }

            return RedirectToActionPermanent("Index");
        }

        [HttpDelete("{id}")]
        [Route("manager/pages/delete")]
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