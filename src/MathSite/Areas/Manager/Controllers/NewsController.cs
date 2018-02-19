using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.Controllers;
using MathSite.Entities;
using MathSite.Entities.Dtos;
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
        [Route("manager/news/create")]
        [Route("manager/news/edit")]
        public async Task<IActionResult> Create(Guid id)
        {
            return id == Guid.Empty
                ? View("Create", await _modelBuilder.BuildCreateViewModel())
                : View("Create", await _modelBuilder.BuildEditViewModel(id));
        }

        [HttpPost]
        [Route("[area]/[controller]/create")]
        [Route("[area]/[controller]/edit")]
        public async Task<IActionResult> Create(NewsViewModel news)
        {
            if (news.Id == Guid.Empty)
            {
                var post = new PostDto
                {
                    Id = Guid.NewGuid(),
                    Title = news.Title,
                    Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content,
                    Content = news.Content,
                    AuthorId = CurrentUser.Id,
                    Published = false,
                    Deleted = false,
                    PostSettings = new PostSetting(),
                    PostSeoSetting = new PostSeoSetting(),
                    PostCategories = new List<PostCategory>()
                };

                await _modelBuilder.BuildCreateViewModel(post);
            }
            else
            {
                var post = new PostDto
                {
                    Id = news.Id,
                    Title = news.Title,
                    Excerpt = news.Content.Length > 50 ? $"{news.Content.Substring(0, 47)}..." : news.Content,
                    Content = news.Content,
                    Published = news.Published,
                    Deleted = news.Deleted,
                    PublishDate = news.PublishDate,
                    AuthorId = news.AuthorId,
                    PostTypeId = news.PostTypeId,
                    PostSettingsId = news.PostSettingsId,
                    PostSeoSettingsId = news.PostSeoSettingsId,
                    PostCategories = news.PostCategories
                };

                await _modelBuilder.BuildEditViewModel(post);
            }

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