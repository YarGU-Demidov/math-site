using System;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.News;
using MathSite.Controllers;
using MathSite.Facades.Posts;
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
        private readonly IPostsFacade _posts;

        public NewsController(IUserValidationFacade userValidationFacade, INewsManagerViewModelBuilder modelBuilder,
            IPostsFacade posts)
            : base(userValidationFacade)
        {
            _modelBuilder = modelBuilder;
            _posts = posts;
        }
        
        [Route("manager/news/")]
        [Route("manager/news/index")]
        [Route("manager/news/list")]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            return View(await _modelBuilder.BuildIndexViewModel(page));
        }

        public async Task<IActionResult> Removed([FromQuery] int page = 1)
        {
            return View("Index", await _modelBuilder.BuildRemovedViewModel(page));
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